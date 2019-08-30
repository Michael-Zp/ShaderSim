
#include "cuda_runtime.h"
#include "device_launch_parameters.h"

#include <stdio.h>
#include <stdlib.h>

cudaError_t addWithCuda(int *c, const int *a, const int *b, unsigned int size);
cudaError_t barycentricCuda(const float3 *v0, const float3 *v1, const float3 *v2, const float *da, const float *db, const float *dc, float *dOut, int2 framebufferSize);

__device__ __inline__ float dot(const float2 a, const float2 b)
{
	return (a.x * b.x) + (a.y * b.y);
}

__device__ float2 calculatePosition(int x, int y, float width, float height)
{
	float2 fragSize = make_float2(2 / width, 2 / height);

	return make_float2(fragSize.x * x + fragSize.y / 2 - 1, (fragSize.y * y + fragSize.y / 2 - 1) * -1);
}

__global__ void baryKernel(const float3 *v0, const float3 *v1, const float3 *v2, const float *da, const float *db, const float *dc, float *dOut, int *width, int *height)
{
	unsigned int x = blockIdx.x * blockDim.x + threadIdx.x;
	unsigned int y = blockIdx.y * blockDim.y + threadIdx.y;
	if (x < *width && y < *height)
	{
		float2 pos = calculatePosition(x, y, *width, *height);
		float2 t0 = make_float2(v2->x, v2->y);
		float2 t1 = make_float2(v0->x, v0->y);
		float2 t2 = make_float2(v1->x, v1->y);

		float2 v0 = make_float2(t1.x - t0.x, t1.y - t0.y);
		float2 v1 = make_float2(t2.x - t0.x, t2.y - t0.y);
		float2 v2 = make_float2(pos.x - t0.x, pos.y - t0.y);

		float d00 = dot(v0, v0);
		float d01 = dot(v0, v1);
		float d11 = dot(v1, v1);
		float d20 = dot(v2, v0);
		float d21 = dot(v2, v1);
		float denom = d00 * d11 - d01 * d01;

		float baryX = (d11 * d20 - d01 * d21) / denom;
		float baryY = (d00 * d21 - d01 * d20) / denom;
		float baryZ = 1 - baryX - baryY;

		if (baryX > 0 && baryY > 0 && baryZ > 0)
		{
			dOut[y * *width + x] = *da * baryX + *db * baryY + *dc * baryZ;
		}
		else
		{
			dOut[y * *width + x] = 0;
		}
	}


}


int main()
{
	printf("\n\n\nBarycentric:\n");

	int2 framebufferSize = make_int2(50, 50);
	float3 bary_v0 = make_float3(0, 1, 0);
	float3 bary_v1 = make_float3(1, -1, 0);
	float3 bary_v2 = make_float3(-1, -1, 0);
	float bary_da = 3;
	float bary_db = 2;
	float bary_dc = 1;
	float *bary_dOut = (float*)malloc(framebufferSize.x * framebufferSize.y * sizeof(float*));

	// Barycentric in parallel.
	cudaError_t cudaStatus = barycentricCuda(&bary_v0, &bary_v1, &bary_v2, &bary_da, &bary_db, &bary_dc, bary_dOut, framebufferSize);
	if (cudaStatus != cudaSuccess) {
		fprintf(stderr, "barycentricCuda failed!");
		return 1;
	}

	printf("{\n");
	for (int y = 0; y < framebufferSize.y; y++)
	{
		printf("  {");
		for (int x = 0; x < framebufferSize.x; x++)
		{
			printf("%.1f|", bary_dOut[x + y * framebufferSize.y]);
		}
		printf("}\n");
	}
	printf("}\n");

    // cudaDeviceReset must be called before exiting in order for profiling and
    // tracing tools such as Nsight and Visual Profiler to show complete traces.
    cudaStatus = cudaDeviceReset();
    if (cudaStatus != cudaSuccess) {
        fprintf(stderr, "cudaDeviceReset failed!");
        return 1;
    }

    return 0;
}

#define myMalloc(VAR, SIZE, TYPE) cudaStatus = cudaMalloc((void**)&VAR, SIZE * sizeof(TYPE)); \
if (cudaStatus != cudaSuccess) {\
		fprintf(stderr, "cudaMalloc failed!"); \
		goto Error; \
}

#define myVarOnGPU(VAR, SOURCEVAR, SIZE, TYPE) cudaStatus = cudaMalloc((void**)&VAR, SIZE * sizeof(TYPE));\
	if (cudaStatus != cudaSuccess) {\
		fprintf(stderr, "cudaMalloc failed!");\
		goto Error;\
	}\
	cudaStatus = cudaMemcpy(VAR, SOURCEVAR, SIZE * sizeof(TYPE), cudaMemcpyHostToDevice);\
	if (cudaStatus != cudaSuccess) {\
		fprintf(stderr, "cudaMemcpy failed!");\
		goto Error;\
	}


// Helper function for using CUDA to add vectors in parallel.
cudaError_t barycentricCuda(const float3 *v0, const float3 *v1, const float3 *v2, const float *da, const float *db, const float *dc, float *dOut, int2 framebufferSize)
{
	int length = framebufferSize.x * framebufferSize.y;
	int bytes = length * sizeof(float);

	const dim3 windowSize(framebufferSize.x, framebufferSize.y);
	const dim3 blockSize(16, 16, 1);
	const dim3 gridSize(windowSize.x / blockSize.x + 1, windowSize.y / blockSize.y + 1);

	

	float3 *dev_v0 = 0;
	float3 *dev_v1 = 0;
	float3 *dev_v2 = 0;
	float *dev_da = 0;
	float *dev_db = 0;
	float *dev_dc = 0;
	struct cudaPitchedPtr dstGPU;
	int *dev_width = 0;
	int *dev_height = 0;
	cudaError_t cudaStatus;

	// Choose which GPU to run on, change this on a multi-GPU system.
	cudaStatus = cudaSetDevice(0);
	if (cudaStatus != cudaSuccess) {
		fprintf(stderr, "cudaSetDevice failed!  Do you have a CUDA-capable GPU installed?");
		goto Error;
	}

	// Allocate and populate GPU buffers for vectors and data.
	myVarOnGPU(dev_v0, v0, 1, float3);
	myVarOnGPU(dev_v1, v1, 1, float3);
	myVarOnGPU(dev_v2, v2, 1, float3);
	myVarOnGPU(dev_da, da, 1, float);
	myVarOnGPU(dev_db, db, 1, float);
	myVarOnGPU(dev_dc, dc, 1, float);
	myVarOnGPU(dev_width, &framebufferSize.x, 1, int);
	myVarOnGPU(dev_height, &framebufferSize.y, 1, int);
	cudaStatus = cudaMalloc3D(&dstGPU, make_cudaExtent(framebufferSize.x * sizeof(float), framebufferSize.y, 1));


	// Launch a kernel on the GPU with one thread for each element.
	baryKernel <<<gridSize, blockSize>>> (dev_v0, dev_v1, dev_v2, dev_da, dev_db, dev_dc, (float *)dstGPU.ptr, dev_width, dev_height);

	// Check for any errors launching the kernel
	cudaStatus = cudaGetLastError();
	if (cudaStatus != cudaSuccess) {
		fprintf(stderr, "barycentricCuda launch failed: %s\n", cudaGetErrorString(cudaStatus));
		goto Error;
	}

	// cudaDeviceSynchronize waits for the kernel to finish, and returns
	// any errors encountered during the launch.
	cudaStatus = cudaDeviceSynchronize();
	if (cudaStatus != cudaSuccess) {
		fprintf(stderr, "cudaDeviceSynchronize returned error code %d after launching barycentricCuda!\n", cudaStatus);
		goto Error;
	}

	// Copy output vector from GPU buffer to host memory.
	cudaStatus = cudaMemcpy(dOut, dstGPU.ptr, bytes, cudaMemcpyDeviceToHost);
	if (cudaStatus != cudaSuccess) {
		fprintf(stderr, "cudaMemcpy failed!");
		goto Error;
	}

Error:
	cudaFree(dstGPU.ptr);
	cudaFree(dev_dc);
	cudaFree(dev_db);
	cudaFree(dev_da);
	cudaFree(dev_v2);
	cudaFree(dev_v0);
	cudaFree(dev_v1);

	return cudaStatus;
}

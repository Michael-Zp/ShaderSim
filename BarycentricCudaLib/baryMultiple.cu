extern "C" {
	//Device code
	__device__ __inline__ float dot(const float2 a, const float2 b)
	{
		return (a.x * b.x) + (a.y * b.y);
	}

	__device__ float2 calculatePosition(int x, int y, float width, float height)
	{
		float2 fragSize = make_float2(2 / width, 2 / height);

		return make_float2(fragSize.x * x + fragSize.y / 2 - 1, fragSize.y * y + fragSize.y / 2 - 1);
	}

	__global__ void baryKernel(const float2 *v0, \
		const float2 *v1, \
		const float2 *v2, \
		const unsigned int dCount, \
		const unsigned int primitivesCount, \
		const float *da, \
		const float *db, \
		const float *dc, \
		float *dOut, \
		int *dOut_valid_frament, \
		int *dOut_valid_pixel, \
		const int width, \
		const int height)
	{
		unsigned int x = blockIdx.x * blockDim.x + threadIdx.x;
		unsigned int y = blockIdx.y * blockDim.y + threadIdx.y;
		unsigned int z = blockIdx.z * blockDim.z + threadIdx.z;

		if (x < width && y < height && z < primitivesCount)
		{
			float2 pos = calculatePosition(x, y, width, height);
			float2 t0 = make_float2(v2[z].x, v2[z].y);
			float2 t1 = make_float2(v0[z].x, v0[z].y);
			float2 t2 = make_float2(v1[z].x, v1[z].y);

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

			int rowSize = width;
			int gridSize = rowSize * height;
			int triangleBlockSize = gridSize * dCount;
			
			int outDataBaseIndex = x + y * rowSize + z * triangleBlockSize;
			int validIndex = x + y * rowSize + z * gridSize;

			if (baryX > 0 && baryY > 0 && baryZ > 0)
			{
				int inDataBaseIndex = z * dCount;
				for (int i = 0; i < dCount; i++)
				{
					int idx = inDataBaseIndex + i;
					dOut[outDataBaseIndex + i * gridSize] = da[idx] * baryX + db[idx] * baryY + dc[idx] * baryZ;
				}
				dOut_valid_frament[validIndex] = 1;
				dOut_valid_pixel[x + y * rowSize] += 1;
			}
			else
			{
				for (int i = 0; i < dCount; i++)
				{
					dOut[outDataBaseIndex + i * gridSize] = 0;
				}
				dOut_valid_frament[validIndex] = 0;
			}
		}

	}
}

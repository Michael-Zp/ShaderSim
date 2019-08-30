using ManagedCuda;
using ManagedCuda.VectorTypes;
using System;
using System.IO;
using System.Reflection;
using ShaderSimulator;
using System.Collections.Generic;
using System.Collections;
using ShaderUtils;
using ShaderUtils.Mathematics;
using System.Diagnostics;

namespace BarycentricCudaLib
{
    public struct BarycentricReturn
    {
        public readonly int X;
        public readonly int Y;
        public readonly float Depth;
        public readonly List<object> FragmentData;
        public readonly bool Valid;

        public BarycentricReturn(int x, int y, float depth, List<object> fragmentData, bool valid)
        {
            X = x;
            Y = y;
            Depth = depth;
            FragmentData = fragmentData;
            Valid = valid;
        }
    }

    public class BarycentricCuda
    {
        private CudaContext ctx;
        private CudaKernel baryKernel;
        private int Height;
        private int Width;

        //static bool noprompt;
        // Variables
        private float2 h_v0 = new float2(0, 0);
        private float2 h_v1 = new float2(0, 0);
        private float2 h_v2 = new float2(0, 0);
        

        public BarycentricCuda(int width, int height)
        {
            Width = width;
            Height = height;

            //Init Cuda context
            ctx = new CudaContext(CudaContext.GetMaxGflopsDeviceId());

            //Load Kernel image from resources
            string resName = "bary.ptx";

            string resNamespace = "BarycentricCudaLib";
            string resource = resNamespace + "." + resName;
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);
            if (stream == null) throw new ArgumentException("Kernel not found in resources.");

            baryKernel = ctx.LoadKernelPTX(stream, "baryKernel");
        }
        
        public List<List<BarycentricReturn>> ExecuteMultiple(List<Triangle> triangles, out double runtime)
        {
            runtime = 0;
            
            List<List<BarycentricReturn>> interpolated = new List<List<BarycentricReturn>>();

            foreach (var triangle in triangles)
            {
                interpolated.Add(Execute(triangle, out double tempKernelTime));
                runtime += tempKernelTime;
            }

            return interpolated;
        }

        public List<BarycentricReturn> Execute(Triangle triangle, out double runtime)
        {
            
            h_v0 = new float2(((Vector4)triangle[0][VertexShader.PositionName]).X, ((Vector4)triangle[0][VertexShader.PositionName]).Y);
            h_v1 = new float2(((Vector4)triangle[1][VertexShader.PositionName]).X, ((Vector4)triangle[1][VertexShader.PositionName]).Y);
            h_v2 = new float2(((Vector4)triangle[2][VertexShader.PositionName]).X, ((Vector4)triangle[2][VertexShader.PositionName]).Y);
            int dataByteSize = 1;
            foreach(var key in triangle[0].Keys)
            {
                if(key == VertexShader.PositionName)
                {
                    continue;
                }

                switch (triangle[0][key])
                {
                    case float _:
                        {
                            dataByteSize += 1;
                            break;
                        }
                    case Vector2 _:
                        {
                            dataByteSize += 2;
                            break;
                        }
                    case Vector3 _:
                        {
                            dataByteSize += 3;
                            break;
                        }
                    case Vector4 _:
                        {
                            dataByteSize += 4;
                            break;
                        }
                }
            }

            float[] h_da = new float[dataByteSize];
            float[] h_db = new float[dataByteSize];
            float[] h_dc = new float[dataByteSize];

            h_da[0] = ((Vector4)triangle[0][VertexShader.PositionName]).Z;
            h_db[0] = ((Vector4)triangle[1][VertexShader.PositionName]).Z;
            h_dc[0] = ((Vector4)triangle[2][VertexShader.PositionName]).Z;

            int currentIndex = 1;

            foreach (var key in triangle[0].Keys)
            {
                if (key == VertexShader.PositionName)
                {
                    continue;
                }

                switch (triangle[0][key])
                {
                    case float _:
                        {
                            h_da[currentIndex] = (float)triangle[0][key];
                            h_db[currentIndex] = (float)triangle[1][key];
                            h_dc[currentIndex] = (float)triangle[2][key];
                            currentIndex += 1;
                            break;
                        }
                    case Vector2 _:
                        {
                            Vector2 v0 = (Vector2)triangle[0][key];
                            Vector2 v1 = (Vector2)triangle[1][key];
                            Vector2 v2 = (Vector2)triangle[2][key];

                            h_da[currentIndex] = v0.X;
                            h_da[currentIndex + 1] = v0.Y;
                            h_db[currentIndex] = v1.X;
                            h_db[currentIndex + 1] = v1.Y;
                            h_dc[currentIndex] = v2.X;
                            h_dc[currentIndex + 1] = v2.Y;

                            currentIndex += 2;
                            break;
                        }
                    case Vector3 _:
                        {
                            Vector3 v0 = (Vector3)triangle[0][key];
                            Vector3 v1 = (Vector3)triangle[1][key];
                            Vector3 v2 = (Vector3)triangle[2][key];

                            h_da[currentIndex] = v0.X;
                            h_da[currentIndex + 1] = v0.Y;
                            h_da[currentIndex + 2] = v0.Z;
                            h_db[currentIndex] = v1.X;
                            h_db[currentIndex + 1] = v1.Y;
                            h_db[currentIndex + 2] = v1.Z;
                            h_dc[currentIndex] = v2.X;
                            h_dc[currentIndex + 1] = v2.Y;
                            h_dc[currentIndex + 2] = v2.Z;

                            currentIndex += 3;
                            break;
                        }
                    case Vector4 _:
                        {
                            Vector4 v0 = (Vector4)triangle[0][key];
                            Vector4 v1 = (Vector4)triangle[1][key];
                            Vector4 v2 = (Vector4)triangle[2][key];

                            h_da[currentIndex] = v0.X;
                            h_da[currentIndex + 1] = v0.Y;
                            h_da[currentIndex + 2] = v0.Z;
                            h_da[currentIndex + 3] = v0.W;
                            h_db[currentIndex] = v1.X;
                            h_db[currentIndex + 1] = v1.Y;
                            h_db[currentIndex + 2] = v1.Z;
                            h_db[currentIndex + 3] = v1.W;
                            h_dc[currentIndex] = v2.X;
                            h_dc[currentIndex + 1] = v2.Y;
                            h_dc[currentIndex + 2] = v2.Z;
                            h_dc[currentIndex + 3] = v2.W;

                            currentIndex += 4;
                            break;
                        }
                }
            }

            
            float[] h_dOut = new float[Width * Height * dataByteSize];
            int[] h_dOut_valid = new int[Width * Height];

            // Allocate vectors in device memory and copy vectors from host memory to device memory 
            // Notice the new syntax with implicit conversion operators: Allocation of device memory and data copy is one operation.
            CudaDeviceVariable<float2> dev_v0 = h_v0;
            CudaDeviceVariable<float2> dev_v1 = h_v1;
            CudaDeviceVariable<float2> dev_v2 = h_v2;
            CudaDeviceVariable<float> dev_da = h_da;
            CudaDeviceVariable<float> dev_db = h_db;
            CudaDeviceVariable<float> dev_dc = h_dc;

            CudaDeviceVariable<float> dev_dOut = new CudaDeviceVariable<float>(Width * Height * dataByteSize);
            CudaDeviceVariable<int> dev_dOut_valid = new CudaDeviceVariable<int>(Width * Height);
                        

            dim3 windowSize = new dim3(Width, Height);
            dim3 blockSize = new dim3(16, 16, 1);
            dim3 gridSize = new dim3(windowSize.x / blockSize.x + 1, windowSize.y / blockSize.y + 1);

            baryKernel.BlockDimensions = blockSize;
            baryKernel.GridDimensions = gridSize;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            baryKernel.Run(dev_v0.DevicePointer, 
                           dev_v1.DevicePointer, 
                           dev_v2.DevicePointer, 
                           dataByteSize, 
                           dev_da.DevicePointer, 
                           dev_db.DevicePointer,
                           dev_dc.DevicePointer, 
                           dev_dOut.DevicePointer, 
                           dev_dOut_valid.DevicePointer,
                           Width, 
                           Height);


            // Copy result from device memory to host memory
            // h_C contains the result in host memory
            h_dOut = dev_dOut;
            h_dOut_valid = dev_dOut_valid;

            runtime = sw.Elapsed.TotalMilliseconds;

            //Cleanup
            if (dev_v0 != null)
                dev_v0.Dispose();

            if (dev_v1 != null)
                dev_v1.Dispose();

            if (dev_v2 != null)
                dev_v2.Dispose();

            if (dev_da != null)
                dev_da.Dispose();

            if (dev_db != null)
                dev_db.Dispose();

            if (dev_dc != null)
                dev_dc.Dispose();

            if (dev_dOut != null)
                dev_dOut.Dispose();

            if (dev_dOut_valid != null)
                dev_dOut_valid.Dispose();
            

            List<BarycentricReturn> outData = new List<BarycentricReturn>();

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int baseIndex = y * Width + x;
                    int nextValueStep = Height * Width;

                    if (h_dOut_valid[baseIndex] == 0)
                    {
                        continue;
                    }

                    float depth = h_dOut[baseIndex];
                    List<object> fragmentData = new List<object>();

                    currentIndex = 1;

                    foreach (var key in triangle[0].Keys)
                    {
                        if (key == VertexShader.PositionName)
                        {
                            continue;
                        }

                        switch (triangle[0][key])
                        {

                            case float _:
                                {
                                    fragmentData.Add((float)h_dOut[baseIndex + currentIndex * nextValueStep]);
                                    currentIndex += 1;
                                    break;
                                }
                            case Vector2 _:
                                {
                                    Vector2 vec2 = new Vector2(h_dOut[baseIndex + (currentIndex + 0) * nextValueStep],
                                                               h_dOut[baseIndex + (currentIndex + 1) * nextValueStep]);
                                    fragmentData.Add(vec2);
                                    currentIndex += 2;
                                    break;
                                }
                            case Vector3 _:
                                {
                                    Vector3 vec3 = new Vector3(h_dOut[baseIndex + (currentIndex + 0) * nextValueStep],
                                                               h_dOut[baseIndex + (currentIndex + 1) * nextValueStep],
                                                               h_dOut[baseIndex + (currentIndex + 2) * nextValueStep]);
                                    fragmentData.Add(vec3);
                                    currentIndex += 3;
                                    break;
                                }
                            case Vector4 _:
                                {
                                    Vector4 vec4 = new Vector4(h_dOut[baseIndex + (currentIndex + 0) * nextValueStep],
                                                               h_dOut[baseIndex + (currentIndex + 1) * nextValueStep],
                                                               h_dOut[baseIndex + (currentIndex + 2) * nextValueStep],
                                                               h_dOut[baseIndex + (currentIndex + 3) * nextValueStep]);
                                    fragmentData.Add(vec4);
                                    currentIndex += 4;
                                    break;
                                }
                        }
                    }

                    outData.Add(new BarycentricReturn(x, y, depth, fragmentData, true));

                }
            }

            return outData;
        }
    }
}

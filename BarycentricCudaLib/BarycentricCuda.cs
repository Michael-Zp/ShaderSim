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
        public readonly float Depth;
        public readonly List<object> FragmentData;
        public readonly bool Valid;

        public BarycentricReturn(float depth, List<object> fragmentData, bool valid)
        {
            Depth = depth;
            FragmentData = fragmentData;
            Valid = valid;
        }
    }

    public class BarycentricCuda
    {
        static CudaContext ctx;
        static CudaKernel baryKernel;
        //static bool noprompt;
        // Variables
        static float2 h_v0;
        static float2 h_v1;
        static float2 h_v2;
        static int h_dCount;
        static float[] h_da;
        static float[] h_db;
        static float[] h_dc;
        static float[] h_dOut;
        static int[] h_dOut_valid;
        static int h_width;
        static int h_height;

        static CudaDeviceVariable<float2> dev_v0;
        static CudaDeviceVariable<float2> dev_v1;
        static CudaDeviceVariable<float2> dev_v2;
        static CudaDeviceVariable<int> dev_dCount;
        static CudaDeviceVariable<float> dev_da;
        static CudaDeviceVariable<float> dev_db;
        static CudaDeviceVariable<float> dev_dc;
        static CudaDeviceVariable<float> dev_dOut;
        static CudaDeviceVariable<int> dev_dOut_valid;
        static CudaDeviceVariable<int> dev_width;
        static CudaDeviceVariable<int> dev_height;
        
        public static BarycentricReturn[,] Execute(Triangle triangle, int width, int height)
        {

            // Allocate input vectors in host memory
            if(ctx == null)
            {
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
            
            h_v0 = new float2(((Vector4)triangle[0][VertexShader.PositionName]).X, ((Vector4)triangle[0][VertexShader.PositionName]).Y);
            h_v1 = new float2(((Vector4)triangle[1][VertexShader.PositionName]).X, ((Vector4)triangle[1][VertexShader.PositionName]).Y);
            h_v2 = new float2(((Vector4)triangle[2][VertexShader.PositionName]).X, ((Vector4)triangle[2][VertexShader.PositionName]).Y);
            h_dCount = 1;
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
                            h_dCount += 1;
                            break;
                        }
                    case Vector2 _:
                        {
                            h_dCount += 2;
                            break;
                        }
                    case Vector3 _:
                        {
                            h_dCount += 3;
                            break;
                        }
                    case Vector4 _:
                        {
                            h_dCount += 4;
                            break;
                        }
                }
            }

            h_da = new float[h_dCount];
            h_db = new float[h_dCount];
            h_dc = new float[h_dCount];

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



            h_dOut = new float[width * height * h_dCount];
            h_dOut_valid = new int[width * height];
            h_width = width;
            h_height = height;

            // Allocate vectors in device memory and copy vectors from host memory to device memory 
            // Notice the new syntax with implicit conversion operators: Allocation of device memory and data copy is one operation.
            dev_v0 = h_v0;
            dev_v1 = h_v1;
            dev_v2 = h_v2;
            dev_dCount = h_dCount;
            dev_da = h_da;
            dev_db = h_db;
            dev_dc = h_dc;

            dev_dOut = new CudaDeviceVariable<float>(width * height * h_dCount);
            dev_dOut_valid = new CudaDeviceVariable<int>(width * height);

            dev_width = h_width;
            dev_height = h_height;
            

            dim3 windowSize = new dim3(width, height);
            dim3 blockSize = new dim3(16, 16, 1);
            dim3 gridSize = new dim3(windowSize.x / blockSize.x + 1, windowSize.y / blockSize.y + 1);

            baryKernel.BlockDimensions = blockSize;
            baryKernel.GridDimensions = gridSize;

            //Stopwatch sw = new Stopwatch();
            //sw.Start();

            baryKernel.Run(dev_v0.DevicePointer, 
                           dev_v1.DevicePointer, 
                           dev_v2.DevicePointer, 
                           dev_dCount.DevicePointer, 
                           dev_da.DevicePointer, 
                           dev_db.DevicePointer,
                           dev_dc.DevicePointer, 
                           dev_dOut.DevicePointer, 
                           dev_dOut_valid.DevicePointer,
                           dev_width.DevicePointer, 
                           dev_height.DevicePointer);


            // Copy result from device memory to host memory
            // h_C contains the result in host memory
            h_dOut = dev_dOut;
            h_dOut_valid = dev_dOut_valid;

            //Console.WriteLine("Only cuda kernel: " + sw.Elapsed.TotalMilliseconds);

            CleanupResources();

            BarycentricReturn[,] outData = new BarycentricReturn[width, height];

            //Console.Write("{\n");
            for (int x = 0; x < width; x++)
            {
                //Console.Write("  {");
                for (int y = 0; y < height; y++)
                {
                    int baseIndex = y * width + x;
                    int nextValueStep = height * width;

                    if(h_dOut_valid[baseIndex] == 0)
                    {
                        outData[x, y] = new BarycentricReturn(0, null, false);
                        continue;
                    }

                    float depth = h_dOut[baseIndex];
                    List<object> fragmentData = new List<object>();

                    currentIndex = 1;
                    
                    foreach(var key in triangle[0].Keys)
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

                    outData[x, y] = new BarycentricReturn(depth, fragmentData, true);

                    //for (int k = 0; k < h_dCount; k++)
                    //{

                    //    Console.Write("{0:N2};", h_dOut[y + x * height + k * (width * height)]);
                    //}
                    //Console.Write(" | ");
                }
                //Console.Write("}\n");
            }
            //Console.Write("}\n");

            //Console.ReadKey();

            return outData;
        }

        static void CleanupResources()
        {
            // Free device memory
            if (dev_v0 != null)
                dev_v0.Dispose();

            if (dev_v1 != null)
                dev_v1.Dispose();

            if (dev_v2 != null)
                dev_v2.Dispose();

            if (dev_dCount != null)
                dev_dCount.Dispose();

            if (dev_da != null)
                dev_da.Dispose();

            if (dev_db != null)
                dev_db.Dispose();

            if (dev_dc != null)
                dev_dc.Dispose();

            if (dev_dOut != null)
                dev_dOut.Dispose();

            if (dev_height != null)
                dev_height.Dispose();

            if (dev_width != null)
                dev_width.Dispose();

            // Free host memory
            // We have a GC for that :-)
        }
    }
}

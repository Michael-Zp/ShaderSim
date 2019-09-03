using ManagedCuda;
using ManagedCuda.VectorTypes;
using System;
using System.IO;
using System.Reflection;
using ShaderSimulator;
using System.Collections.Generic;
using ShaderUtils;
using ShaderUtils.Mathematics;
using System.Diagnostics;
using System.Collections;
using System.Threading.Tasks;

namespace BarycentricCudaLib
{
    public struct BarycentricReturnMultiple
    {
        public List<float>[,] Depths;
        public Dictionary<string, IList>[,] FragmentData;
        public int[,] FragmentCount;

        public BarycentricReturnMultiple(int width, int height)
        {
            Depths = new List<float>[width, height];
            FragmentData = new Dictionary<string, IList>[width, height];
            FragmentCount = new int[width, height];
        }
    }

    public class BarycentricCudaMultiple
    {
        private BarycentricReturnMultiple OutDataThreaded;

        private CudaContext ctx;
        private CudaKernel baryKernel;
        private readonly int Height;
        private readonly int Width;
        private readonly int ThreadCount;

        //static bool noprompt;
        // Variables
        private float2[] h_v0;
        private float2[] h_v1;
        private float2[] h_v2;

        float[] h_dOut;
        int[] h_dOut_valid_fragment;
        int[] h_dOut_valid_pixel;

        
        public BarycentricCudaMultiple(int width, int height, int threadCount = 12)
        {
            Width = width;
            Height = height;
            ThreadCount = threadCount;

            //Init Cuda context
            ctx = new CudaContext(CudaContext.GetMaxGflopsDeviceId());

            //Load Kernel image from resources
            string resName = "baryMultiple.ptx";

            string resNamespace = "BarycentricCudaLib";
            string resource = resNamespace + "." + resName;
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);
            if (stream == null) throw new ArgumentException("Kernel not found in resources.");

            baryKernel = ctx.LoadKernelPTX(stream, "baryKernel");
        }

        public BarycentricReturnMultiple Execute(List<Triangle> primitives, Dictionary<string, object>.KeyCollection dataKeys)
        {
            h_v0 = new float2[primitives.Count];
            h_v1 = new float2[primitives.Count];
            h_v2 = new float2[primitives.Count];

            for (int i = 0; i < primitives.Count; i++)
            {
                h_v0[i] = new float2(((Vector4)primitives[i][0][VertexShader.PositionName]).X, ((Vector4)primitives[i][0][VertexShader.PositionName]).Y);
                h_v1[i] = new float2(((Vector4)primitives[i][1][VertexShader.PositionName]).X, ((Vector4)primitives[i][1][VertexShader.PositionName]).Y);
                h_v2[i] = new float2(((Vector4)primitives[i][2][VertexShader.PositionName]).X, ((Vector4)primitives[i][2][VertexShader.PositionName]).Y);
            }

            int dataByteSize = 1;
            foreach (var key in dataKeys)
            {
                if (key == VertexShader.PositionName)
                {
                    continue;
                }

                switch (primitives[0][0][key])
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

            float[] h_da = new float[dataByteSize * primitives.Count];
            float[] h_db = new float[dataByteSize * primitives.Count];
            float[] h_dc = new float[dataByteSize * primitives.Count];

            for (int i = 0; i < primitives.Count; i++)
            {
                h_da[i * dataByteSize] = ((Vector4)primitives[i][0][VertexShader.PositionName]).Z;
                h_db[i * dataByteSize] = ((Vector4)primitives[i][1][VertexShader.PositionName]).Z;
                h_dc[i * dataByteSize] = ((Vector4)primitives[i][2][VertexShader.PositionName]).Z;

                int currentIndex = i * dataByteSize + 1;

                foreach (var key in dataKeys)
                {
                    if (key == VertexShader.PositionName)
                    {
                        continue;
                    }

                    switch (primitives[i][0][key])
                    {
                        case float _:
                            {
                                h_da[currentIndex] = (float)primitives[i][0][key];
                                h_db[currentIndex] = (float)primitives[i][1][key];
                                h_dc[currentIndex] = (float)primitives[i][2][key];
                                currentIndex += 1;
                                break;
                            }
                        case Vector2 _:
                            {
                                Vector2 v0 = (Vector2)primitives[i][0][key];
                                Vector2 v1 = (Vector2)primitives[i][1][key];
                                Vector2 v2 = (Vector2)primitives[i][2][key];

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
                                Vector3 v0 = (Vector3)primitives[i][0][key];
                                Vector3 v1 = (Vector3)primitives[i][1][key];
                                Vector3 v2 = (Vector3)primitives[i][2][key];

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
                                Vector4 v0 = (Vector4)primitives[i][0][key];
                                Vector4 v1 = (Vector4)primitives[i][1][key];
                                Vector4 v2 = (Vector4)primitives[i][2][key];

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
            }


            h_dOut = new float[Width * Height * dataByteSize * primitives.Count];
            h_dOut_valid_fragment = new int[Width * Height * primitives.Count];
            h_dOut_valid_pixel = new int[Width * Height];


            // Allocate vectors in device memory and copy vectors from host memory to device memory 
            // Notice the new syntax with implicit conversion operators: Allocation of device memory and data copy is one operation.
            CudaDeviceVariable<float2> dev_v0 = h_v0;
            CudaDeviceVariable<float2> dev_v1 = h_v1;
            CudaDeviceVariable<float2> dev_v2 = h_v2;
            CudaDeviceVariable<float> dev_da = h_da;
            CudaDeviceVariable<float> dev_db = h_db;
            CudaDeviceVariable<float> dev_dc = h_dc;

            CudaDeviceVariable<float> dev_dOut = new CudaDeviceVariable<float>(Width * Height * dataByteSize * primitives.Count);
            CudaDeviceVariable<int> dev_dOut_valid = new CudaDeviceVariable<int>(Width * Height * primitives.Count);
            CudaDeviceVariable<int> dev_dOut_valid2 = h_dOut_valid_pixel;


            dim3 windowSize = new dim3(Width, Height);
            dim3 blockSize = new dim3(8, 8, 8);
            dim3 gridSize = new dim3(windowSize.x / blockSize.x + 1, windowSize.y / blockSize.y + 1, ((uint)primitives.Count * (uint)dataByteSize) / blockSize.z + 1);

            baryKernel.BlockDimensions = blockSize;
            baryKernel.GridDimensions = gridSize;
            

            baryKernel.Run(dev_v0.DevicePointer,
                           dev_v1.DevicePointer,
                           dev_v2.DevicePointer,
                           dataByteSize,
                           primitives.Count,
                           dev_da.DevicePointer,
                           dev_db.DevicePointer,
                           dev_dc.DevicePointer,
                           dev_dOut.DevicePointer,
                           dev_dOut_valid.DevicePointer,
                           dev_dOut_valid2.DevicePointer,
                           Width,
                           Height);


            // Copy result from device memory to host memory
            // h_C contains the result in host memory
            h_dOut = dev_dOut;
            h_dOut_valid_fragment = dev_dOut_valid;
            h_dOut_valid_pixel = dev_dOut_valid2;
            

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

            if (dev_dOut_valid2 != null)
                dev_dOut_valid2.Dispose();

            OutDataThreaded = new BarycentricReturnMultiple(Width, Height);
            

            int dataRowSize = Width;
            int dataGridSize = dataRowSize * Height;
            int triangleBlockSize = dataGridSize * dataByteSize;

            Parallel.For(0, ThreadCount, (i) =>
            {
                for (int x = i; x < Width; x += ThreadCount)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        if (h_dOut_valid_pixel[x + y * dataRowSize] == 0)
                        {
                            continue;
                        }

                        for (int z = 0; z < primitives.Count; z++)
                        {
                            
                            if (h_dOut_valid_fragment[x + y * dataRowSize + z * dataGridSize] == 0)
                            {
                                continue;
                            }

                            int dataBaseIndex = x + y * dataRowSize + z * triangleBlockSize;

                            if (OutDataThreaded.Depths[x, y] == null)
                            {
                                OutDataThreaded.Depths[x, y] = new List<float>();
                                OutDataThreaded.FragmentData[x, y] = new Dictionary<string, IList>();
                                OutDataThreaded.FragmentCount[x, y] = 0;
                            }

                            OutDataThreaded.Depths[x, y].Add(h_dOut[dataBaseIndex]);
                            OutDataThreaded.FragmentCount[x, y]++;


                            //i == 0 is the depth and already handled above
                            int currentDataPoint = 1;
                            foreach (var key in dataKeys)
                            {
                                if (key == VertexShader.PositionName)
                                {
                                    continue;
                                }

                                if (!OutDataThreaded.FragmentData[x, y].ContainsKey(key))
                                {
                                    OutDataThreaded.FragmentData[x, y].Add(key, new List<object>());
                                }

                                switch (primitives[z][0][key])
                                {
                                    case float _:
                                        {
                                            OutDataThreaded.FragmentData[x, y][key].Add((float)h_dOut[dataBaseIndex + (currentDataPoint + 0) * dataGridSize]);
                                            currentDataPoint += 1;
                                            break;
                                        }
                                    case Vector2 _:
                                        {
                                            Vector2 vec2 = new Vector2(h_dOut[dataBaseIndex + (currentDataPoint + 0) * dataGridSize],
                                                                        h_dOut[dataBaseIndex + (currentDataPoint + 1) * dataGridSize]);
                                            OutDataThreaded.FragmentData[x, y][key].Add(vec2);
                                            currentDataPoint += 2;
                                            break;
                                        }
                                    case Vector3 _:
                                        {
                                            Vector3 vec3 = new Vector3(h_dOut[dataBaseIndex + (currentDataPoint + 0) * dataGridSize],
                                                                        h_dOut[dataBaseIndex + (currentDataPoint + 1) * dataGridSize],
                                                                        h_dOut[dataBaseIndex + (currentDataPoint + 2) * dataGridSize]);
                                            OutDataThreaded.FragmentData[x, y][key].Add(vec3);
                                            currentDataPoint += 3;
                                            break;
                                        }
                                    case Vector4 _:
                                        {
                                            Vector4 vec4 = new Vector4(h_dOut[dataBaseIndex + (currentDataPoint + 0) * dataGridSize],
                                                                        h_dOut[dataBaseIndex + (currentDataPoint + 1) * dataGridSize],
                                                                        h_dOut[dataBaseIndex + (currentDataPoint + 2) * dataGridSize],
                                                                        h_dOut[dataBaseIndex + (currentDataPoint + 3) * dataGridSize]);
                                            OutDataThreaded.FragmentData[x, y][key].Add(vec4);
                                            currentDataPoint += 4;
                                            break;
                                        }
                                }
                            }
                        }
                    }
                }
            });
            
            
            return OutDataThreaded;
        }
    }
}

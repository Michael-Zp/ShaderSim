﻿using ManagedCuda;
using System;
using System.IO;
using System.Reflection;

namespace TestManagedCuda
{
    public class VectorAdd
    {
        static CudaContext ctx;
        static Random rand = new Random();
        //static bool noprompt;
        // Variables
        static float[] h_A;
        static float[] h_B;
        static float[] h_C;
        static CudaDeviceVariable<float> d_A;
        static CudaDeviceVariable<float> d_B;
        static CudaDeviceVariable<float> d_C;

        public static void Execute()
        {
            Console.WriteLine("Vector Addition");
            int N = 10;

            //Init Cuda context
            ctx = new CudaContext(CudaContext.GetMaxGflopsDeviceId());

            //Load Kernel image from resources
            string resName;
            if (IntPtr.Size == 8)
                resName = "vectorAdd_x64.ptx";
            else
                resName = "vectorAdd.ptx";

            string resNamespace = "TestManagedCuda";
            string resource = resNamespace + "." + resName;
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);
            if (stream == null) throw new ArgumentException("Kernel not found in resources.");

            CudaKernel vectorAddKernel = ctx.LoadKernelPTX(stream, "VecAdd");

            // Allocate input vectors h_A and h_B in host memory
            h_A = new float[N];
            h_B = new float[N];


            // Initialize input vectors
            RandomInit(h_A, N);
            RandomInit(h_B, N);

            // Allocate vectors in device memory and copy vectors from host memory to device memory 
            // Notice the new syntax with implicit conversion operators: Allocation of device memory and data copy is one operation.
            d_A = h_A;
            d_B = h_B;
            d_C = new CudaDeviceVariable<float>(N);

            // Invoke kernel
            int threadsPerBlock = 256;
            vectorAddKernel.BlockDimensions = threadsPerBlock;
            vectorAddKernel.GridDimensions = (N + threadsPerBlock - 1) / threadsPerBlock;

            vectorAddKernel.Run(d_A.DevicePointer, d_B.DevicePointer, d_C.DevicePointer, N);

            // Copy result from device memory to host memory
            // h_C contains the result in host memory
            h_C = d_C;

            // Verify result
            int i;
            for (i = 0; i < N; ++i)
            {
                float sum = h_A[i] + h_B[i];
                if (Math.Abs(h_C[i] - sum) > 1e-5)
                    break;
            }

            CleanupResources();

            foreach (var item in h_C)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }

        static void CleanupResources()
        {
            // Free device memory
            if (d_A != null)
                d_A.Dispose();

            if (d_B != null)
                d_B.Dispose();

            if (d_C != null)
                d_C.Dispose();

            if (ctx != null)
                ctx.Dispose();

            // Free host memory
            // We have a GC for that :-)
        }

        // Allocates an array with random float entries.
        static void RandomInit(float[] data, int n)
        {
            for (int i = 0; i < n; ++i)
                data[i] = (float)rand.NextDouble();
        }
    }
}

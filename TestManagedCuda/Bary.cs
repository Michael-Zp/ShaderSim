using ManagedCuda;
using ManagedCuda.VectorTypes;
using System;
using System.IO;
using System.Reflection;

namespace TestManagedCuda
{
    public class Bary
    {
        static CudaContext ctx;
        //static bool noprompt;
        // Variables
        static int2 framebufferSize;
        static float3 h_v0;
        static float3 h_v1;
        static float3 h_v2;
        static float h_da;
        static float h_db;
        static float h_dc;
        static float[] h_dOut;
        static int h_width;
        static int h_height;

        static CudaDeviceVariable<float3> dev_v0;
        static CudaDeviceVariable<float3> dev_v1;
        static CudaDeviceVariable<float3> dev_v2;
        static CudaDeviceVariable<float> dev_da;
        static CudaDeviceVariable<float> dev_db;
        static CudaDeviceVariable<float> dev_dc;
        static CudaDeviceVariable<float> dev_dOut;
        static CudaDeviceVariable<int> dev_width;
        static CudaDeviceVariable<int> dev_height;
        
        public static void Execute()
        {
            Console.WriteLine("Barycentric stuff");

            //Init Cuda context
            ctx = new CudaContext(CudaContext.GetMaxGflopsDeviceId());

            //Load Kernel image from resources
            string resName = "baryTest.ptx";

            string resNamespace = "TestManagedCuda";
            string resource = resNamespace + "." + resName;
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);
            if (stream == null) throw new ArgumentException("Kernel not found in resources.");

            CudaKernel baryKernel = ctx.LoadKernelPTX(stream, "baryKernel");


            framebufferSize = new int2(5, 5);

            // Allocate input vectors h_A and h_B in host memory
            h_v0 = new float3(0, 1, 0);
            h_v1 = new float3(1, -1, 0);
            h_v2 = new float3(-1, -1, 0);
            h_da = 3;
            h_db = 2;
            h_dc = 1;
            h_dOut = new float[framebufferSize.x * framebufferSize.y];
            h_width = framebufferSize.x;
            h_height = framebufferSize.y;

            // Allocate vectors in device memory and copy vectors from host memory to device memory 
            // Notice the new syntax with implicit conversion operators: Allocation of device memory and data copy is one operation.
            dev_v0 = h_v0;
            dev_v1 = h_v1;
            dev_v2 = h_v2;
            dev_da = h_da;
            dev_db = h_db;
            dev_dc = h_dc;

            dev_dOut = new CudaDeviceVariable<float>(framebufferSize.x * framebufferSize.y);

            dev_width = h_width;
            dev_height = h_height;

            // Invoke kernel
            //int threadsPerBlock = 256;
            //vectorAddKernel.BlockDimensions = threadsPerBlock;
            //vectorAddKernel.GridDimensions = (framebufferSize.x + threadsPerBlock - 1) / threadsPerBlock;


            dim3 windowSize = new dim3(framebufferSize.x, framebufferSize.y);
            dim3 blockSize = new dim3(16, 16, 1);
            dim3 gridSize = new dim3(windowSize.x / blockSize.x + 1, windowSize.y / blockSize.y + 1);

            baryKernel.BlockDimensions = blockSize;
            baryKernel.GridDimensions = gridSize;

            baryKernel.Run(dev_v0.DevicePointer, dev_v1.DevicePointer, dev_v2.DevicePointer, dev_da.DevicePointer, dev_db.DevicePointer, dev_dc.DevicePointer, dev_dOut.DevicePointer, dev_width.DevicePointer, dev_height.DevicePointer);

            // Copy result from device memory to host memory
            // h_C contains the result in host memory
            h_dOut = dev_dOut;
            

            CleanupResources();

            Console.Write("{\n");
            for (int y = 0; y < framebufferSize.y; y++)
            {
                Console.Write("  {");
                for (int x = 0; x < framebufferSize.x; x++)
                {
                    Console.Write(h_dOut[x + y * framebufferSize.y] + "|");
                }
                Console.Write("}\n");
            }
            Console.Write("}\n");

            Console.ReadKey();
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

            if (ctx != null)
                ctx.Dispose();

            // Free host memory
            // We have a GC for that :-)
        }
    }
}

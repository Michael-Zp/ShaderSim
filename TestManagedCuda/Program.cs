/*
 * This code is based on code from the NVIDIA CUDA SDK. (Ported from C++ to C# using managedCUDA)
 * This software contains source code provided by NVIDIA Corporation.
 *
 */

using System.Collections.Generic;
using ShaderSimulator;
using ShaderUtils;
using ShaderUtils.Mathematics;

namespace TestManagedCuda
{
    class Program
    {
        static void Main(string[] args)
        {
            TestBarycentricCoodsMultiple.Test();
        }
    }
}

using BarycentricCudaLib;
using ShaderSimulator;
using ShaderUtils;
using ShaderUtils.Mathematics;
using System;
using System.Collections.Generic;

namespace TestManagedCuda
{
    class TestBarycentricCoodsMultiple
    {
        public static void Test()
        {
            List<Triangle> primitives = new List<Triangle>();

            Triangle triangle = new Triangle();
            triangle[0].Add(VertexShader.PositionName, new Vector4(0, 1, 0, 0));
            triangle[1].Add(VertexShader.PositionName, new Vector4(1, -1, 0, 0));
            triangle[2].Add(VertexShader.PositionName, new Vector4(-1, -1, 0, 0));
            triangle[0].Add("Test1", (float)1);
            triangle[1].Add("Test1", (float)2);
            triangle[2].Add("Test1", (float)3);
            triangle[0].Add("Test2", new Vector2(1, 2));
            triangle[1].Add("Test2", new Vector2(2, 2));
            triangle[2].Add("Test2", new Vector2(3, 2));
            //triangle[0].Add("Test3", new Vector3(1, 1, 3));
            //triangle[1].Add("Test3", new Vector3(2, 2, 3));
            //triangle[2].Add("Test3", new Vector3(3, 3, 3));
            //triangle[0].Add("Test4", new Vector4(1, 1, 1, 4));
            //triangle[1].Add("Test4", new Vector4(2, 2, 2, 4));
            //triangle[2].Add("Test4", new Vector4(3, 3, 3, 4));

            primitives.Add(triangle);


            //BarycentricCudaMultiple barycentricCuda = new BarycentricCudaMultiple(5, 5);

            //var outData = barycentricCuda.Execute(primitives, primitives[0][0].Keys, out double runtime);

            //foreach (var outDataPoint in outData)
            //{
            //    Console.WriteLine((float)outDataPoint.FragmentData[0][0]);
            //}

            //Console.WriteLine("######");

            //primitives.Add(triangle);
            
            //var outData2 = barycentricCuda.Execute(primitives, primitives[0][0].Keys, out runtime);

            //foreach (var outDataPoint in outData2)
            //{
            //    Console.WriteLine((float)outDataPoint.FragmentData[0][0]);
            //}

            //Console.ReadKey();
        }
    }
}

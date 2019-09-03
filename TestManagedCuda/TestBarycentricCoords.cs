using BarycentricCudaLib;
using ShaderSimulator;
using ShaderUtils;
using ShaderUtils.Mathematics;

namespace TestManagedCuda
{
    class TestBarycentricCoords
    {
        public static void Test()
        {
            //Bary.Execute();

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

            BarycentricCuda barycentricCuda = new BarycentricCuda(5, 5);

            var outData = barycentricCuda.Execute(triangle, out double runtime);
        }
    }
}

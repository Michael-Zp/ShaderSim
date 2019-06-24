using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShaderSim;
using ShaderSim.Mathematics;

namespace ShaderSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Mesh test = new DefaultMesh();
            test.AddAttributeValue("Position", new Vector3(0));
            ((DefaultMesh)test).Position.Add(new Vector3(1));
            ((DefaultMesh)test).Normal.Add(new Vector3(1));
            ((DefaultMesh)test).Normal.Add(new Vector3(15));
            TestVAO vao = VAOLoader.FromMesh<TestVAO>(test, new TestShader());
        }
    }
}

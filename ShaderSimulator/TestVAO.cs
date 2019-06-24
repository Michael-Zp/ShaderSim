using System;
using System.Collections.Generic;
using ShaderSim;

namespace ShaderSimulator
{
    public class TestVAO : IVertexArrayObject
    {
        public int IDLength { get; }
        public int PrimitiveType { get; set; }
        public int DrawElementsType { get; }
        public void SetIndex<T>(IEnumerable<T> data) where T : struct
        {
            //throw new NotImplementedException();
        }

        public void SetAttribute<T>(string name, Shader shader, IEnumerable<T> data, bool perInstance = false) where T : struct
        {
            //throw new NotImplementedException();
        }

        public void Draw(int instanceCount = 1)
        {
            throw new NotImplementedException();
        }
    }
}

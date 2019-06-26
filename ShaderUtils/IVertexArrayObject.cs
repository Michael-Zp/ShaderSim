using System;
using System.Collections;
using System.Collections.Generic;

namespace ShaderSim
{
    public interface IVertexArrayObject
    {
        int IDLength { get; }
        int PrimitiveType { get; set; }
        Type DrawElementsType { get; }

        void SetIndex<T>(IEnumerable<T> data) where T : struct;
        void SetAttribute<T>(string name, Shader shader, IEnumerable<T> data, bool perInstance = false) where T : struct;
        void Draw(int instanceCount = 1);
    }


}

using System;
using System.Collections.Generic;

namespace ShaderUtils
{
    public interface IVertexArrayObject
    {
        void SetIndex(IEnumerable<uint> data);
        void SetAttribute<T>(string name, Tuple<VertexShader, FragmentShader> shader, IList<T> data, bool perInstance = false) where T : struct;
        void Draw(int instanceCount = 1);
    }
}

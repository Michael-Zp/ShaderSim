using System.Collections.Generic;

namespace ShaderSim
{
    public interface IVertexArrayObject
    {
        void SetIndex(IEnumerable<uint> data);
        void SetAttribute<T>(string name, Shader vertexShader, IEnumerable<T> data, bool perInstance = false) where T : struct;
        void Draw(int instanceCount = 1);
    }


}

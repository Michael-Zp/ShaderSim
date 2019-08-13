using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaderUtils
{
    public class VertexArrayObject
    {
        private readonly RenderWrapper _wrapper;

        public VertexArrayObject(RenderWrapper wrapper)
        {
            _wrapper = wrapper;
        }

        public void SetIndex(IEnumerable<uint> data)
        {
            _wrapper.SetRenderData(this, data);
        }

        public void SetAttribute<T>(string name, Shader vertexShader, IEnumerable<T> data, bool perInstance = false) where T : struct
        {
            _wrapper.SetAttributes(vertexShader, name, data, perInstance);
        }

        public void Draw(int instanceCount = 1)
        {
            _wrapper.DrawElementsInstanced(instanceCount);
        }
    }
}

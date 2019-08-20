using System;
using System.Collections.Generic;
using ShaderUtils;

namespace ShaderSimulator
{
    public class SimulatorVAO : IVertexArrayObject
    {
        private readonly RenderSimulator _wrapper;

        public SimulatorVAO(RenderSimulator wrapper)
        {
            _wrapper = wrapper;
        }

        public void SetIndex(IEnumerable<uint> data)
        {
            _wrapper.SetRenderData(this, data);
        }

        public void SetAttribute<T>(string name, Tuple<VertexShader, FragmentShader> shader, IList<T> data, bool perInstance = false) where T : struct
        {
            _wrapper.SetAttributes(shader.Item1, name, data, perInstance);
        }

        public void Draw(int instanceCount = 1)
        {
            _wrapper.ActivateVAO(this);
            _wrapper.DrawElementsInstanced(instanceCount);
            _wrapper.DeactivateVAO();
        }
    }
}

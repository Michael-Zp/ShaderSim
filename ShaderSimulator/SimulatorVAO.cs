using System;
using System.Collections.Generic;
using System.Linq;
using ShaderSim;

namespace ShaderSimulator
{
    public class SimulatorVAO : IVertexArrayObject
    {
        private readonly RenderSimulator _simulator;

        public SimulatorVAO(RenderSimulator simulator)
        {
            _simulator = simulator;
        }

        public void SetIndex(IEnumerable<uint> data)
        {
            _simulator.SetRenderData(this, data);
        }

        public void SetAttribute<T>(string name, Shader vertexShader, IEnumerable<T> data, bool perInstance = false) where T : struct
        {
            _simulator.SetAttributes(vertexShader, name, data, perInstance);
        }

        public void Draw(int instanceCount = 1)
        {
            _simulator.DrawElementsInstanced(instanceCount);
        }
    }
}

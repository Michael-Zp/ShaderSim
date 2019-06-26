using System;
using System.Collections.Generic;
using System.Linq;
using ShaderSim;

namespace ShaderSimulator
{
    class SimulatorVAO : IVertexArrayObject
    {
        public int IDLength { get; private set; }
        public int PrimitiveType { get; set; }
        public Type DrawElementsType { get; private set; }

        private readonly RenderSimulator _simulator;

        public SimulatorVAO(RenderSimulator simulator)
        {
            _simulator = simulator;
        }

        public void SetIndex<T>(IEnumerable<T> data) where T : struct
        {
            _simulator.SetRenderData(this, data);
            IDLength = data.Count();
            DrawElementsType = typeof(T);
        }

        public void SetAttribute<T>(string name, Shader shader, IEnumerable<T> data, bool perInstance = false) where T : struct
        {
            _simulator.SetAttributes(shader, name, data, perInstance);
        }

        public void Draw(int instanceCount = 1)
        {
            _simulator.DrawElementsInstanced(this, instanceCount);
        }
    }
}

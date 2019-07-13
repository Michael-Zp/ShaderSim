using ShaderSim;
using ShaderSim.Mathematics;
using ShaderSimulator;

namespace ShaderExample
{
    class Program
    {
        static void Main(string[] args)
        {
            DefaultMesh triangle = new DefaultMesh();
            triangle.Position.Add(new Vector3(0f));
            triangle.Position.Add(new Vector3(0.2f, 0.2f, 0f));
            triangle.Position.Add(new Vector3(0f, 0.2f, 0f));
            triangle.IDs.Add(0);
            triangle.IDs.Add(1);
            triangle.IDs.Add(2);


            RenderSimulator simulator = new RenderSimulator();
            PassVertex vertexShader = new PassVertex();
            SimulatorVAO vao = VAOLoader.FromMesh<SimulatorVAO>(triangle, vertexShader, new object[] { simulator });
            vao.SetAttribute("InstancePosition", vertexShader, new Vector3[] { new Vector3(0f), new Vector3(0.5f, 0.5f, 0f) }, true);
            vao.SetAttribute("Color", vertexShader, new Vector4[] { new Vector4(1f), new Vector4(0.5f, 0.5f, 0.5f, 1f) }, true);
            simulator.ActivateShader(vertexShader, new PassFragment());
            simulator.ActivateVAO(vao);
            simulator.DrawElementsInstanced(2);
        }
    }
}

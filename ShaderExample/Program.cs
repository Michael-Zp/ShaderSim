using ShaderSim;
using ShaderSim.Mathematics;
using ShaderSimulator;

namespace ShaderExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Mesh testMesh = new DefaultMesh();
            testMesh.IDs.Add(0);
            testMesh.AddAttributeValue("Position", new Vector3(1));
            ((DefaultMesh)testMesh).Normal.Add(new Vector3(1));
            ((DefaultMesh)testMesh).Normal.Add(new Vector3(15));
            RenderSimulator simulator = new RenderSimulator();
            PassVertex testShader = new PassVertex();
            SimulatorVAO vao = VAOLoader.FromMesh<SimulatorVAO>(testMesh, testShader, new object[] { simulator });
            vao.SetAttribute("Test", testShader, new float[] { 2.5f }, true);
            simulator.ActivateShader(testShader, new PassFragment());
            simulator.ActivateVAO(vao);
            simulator.DrawElementsInstanced();
        }
    }
}

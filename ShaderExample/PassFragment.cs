using ShaderSim;
using ShaderSim.Attributes;
using ShaderSim.Mathematics;

namespace ShaderExample
{
    class PassFragment : Shader
    {
        [In]
        public Vector4 Col { private get; set; }

        [Out]
        public Vector4 Color { get; private set; }

        public override void Main()
        {
            Color = Col;
        }
    }
}

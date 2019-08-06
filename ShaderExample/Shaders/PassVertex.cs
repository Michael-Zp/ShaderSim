using ShaderSim;
using ShaderSim.Attributes;
using ShaderSim.Mathematics;

namespace ShaderExample.Shaders
{
    class PassVertex : VertexShader
    {
        [In]
        public Vector3 InstancePosition { private get; set; }

        [In]
        public Vector3 Pos { private get; set; }
        [In]
        public Vector4 Color { private get; set; }

        [Out]
        public Vector4 Col { get; private set; }

        public override void Main()
        {
            Position = new Vector4(Pos + InstancePosition, 1);
            Col = Color;
        }
    }
}

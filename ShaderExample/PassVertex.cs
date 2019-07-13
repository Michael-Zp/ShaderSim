using ShaderSim;
using ShaderSim.Attributes;
using ShaderSim.Mathematics;

namespace ShaderExample
{
    class PassVertex : Shader
    {
        [In]
        public Vector3 InstancePosition { private get; set; }

        [In]
        public Vector3 Position { private get; set; }
        [In]
        public Vector4 Color { private get; set; }

        [Out]
        public Vector3 Pos { get; private set; }
        [Out]
        public Vector4 Col { get; private set; }

        public override void Main()
        {
            Pos = Position + InstancePosition;
            Col = Color;
        }
    }
}

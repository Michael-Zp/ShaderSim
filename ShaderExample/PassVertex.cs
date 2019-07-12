using ShaderSim;
using ShaderSim.Attributes;
using ShaderSim.Mathematics;

namespace ShaderExample
{
    class PassVertex : Shader
    {
        [In]
        public float Test { get; set; }

        [In]
        public Vector3 Position { private get; set; }
        [In]
        public Vector3 Color { private get; set; }

        [Out]
        public Vector3 Pos { get; private set; }
        [Out]
        public Vector3 Col { get; private set; }

        public override void Main()
        {
            Pos = Position;
            Col = Color;
        }
    }
}

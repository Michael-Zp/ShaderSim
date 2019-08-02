using ShaderSim;
using ShaderSim.Attributes;
using ShaderSim.Mathematics;

namespace ShaderExample
{
    class LightedVertex : VertexShader
    {
        [Uniform]
        public Matrix4x4 Camera { private get; set; }

        [In]
        public Vector4 InstanceTransformation { private get; set; }

        [In]
        public Vector3 Pos { private get; set; }

        [In]
        public Vector3 Normal { private get; set; }

        [In]
        public Vector4 Color { private get; set; }

        [Out]
        public Vector4 Col { get; private set; }

        [Out]
        public Vector3 WorldPos { get; private set; }

        [Out]
        public Vector3 N { get; private set; }

        public override void Main()
        {
            WorldPos = (InstanceTransformation * new Vector4(Pos, 1)).XYZ;
            N = (InstanceTransformation * new Vector4(Normal, 0)).XYZ;
            Position = Camera * InstanceTransformation * new Vector4(Pos, 1);
            Col = Color;
        }
    }
}

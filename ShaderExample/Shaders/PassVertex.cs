using ShaderUtils;
using ShaderUtils.Attributes;
using ShaderUtils.Mathematics;

namespace ShaderExample.Shaders
{
    class PassVertex : VertexShader
    {
        [In]
        public Matrix4x4 InstanceTransformation { private get; set; }

        [In]
        public Vector3 Pos { private get; set; }
        [In]
        public Vector4 Color { private get; set; }

        [Out]
        public Vector4 Col { get; private set; }

        public override void Main()
        {
            Position = (InstanceTransformation * new Vector4(Pos, 1));
            Col = Color;
        }
    }
}

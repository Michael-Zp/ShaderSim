using ShaderUtils.Attributes;
using ShaderUtils.Mathematics;

namespace ShaderUtils
{
    public abstract class VertexShader : Shader
    {
        public const string PositionName = nameof(Position);

        [Translation("gl_Position")]
        public Vector4 Position { get; protected set; }
    }
}

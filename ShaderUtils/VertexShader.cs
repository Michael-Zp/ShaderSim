using ShaderSim.Attributes;
using ShaderSim.Mathematics;

namespace ShaderSim
{
    public abstract class VertexShader : Shader
    {
        public const string PositionName = nameof(Position);

        [Translation("gl_Position")]
        public Vector4 Position { get; protected set; }
    }
}

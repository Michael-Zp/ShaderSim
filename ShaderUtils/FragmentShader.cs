using ShaderSim.Attributes;
using ShaderSim.Mathematics;

namespace ShaderSim
{
    public abstract class FragmentShader : Shader
    {
        public const string ColorName = nameof(Color);

        [Out]
        public Vector4 Color { get; protected set; }
    }
}

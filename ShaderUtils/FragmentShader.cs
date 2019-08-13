using ShaderUtils.Attributes;
using ShaderUtils.Mathematics;

namespace ShaderUtils
{
    public abstract class FragmentShader : Shader
    {
        public const string ColorName = nameof(Color);

        [Out]
        public Vector4 Color { get; protected set; }
    }
}

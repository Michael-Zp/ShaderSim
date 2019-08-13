using ShaderUtils;
using ShaderUtils.Attributes;
using ShaderUtils.Mathematics;

namespace ShaderExample.Shaders
{
    class PassFragment : FragmentShader
    {
        [In]
        public Vector4 Col { private get; set; }

        public override void Main()
        {
            Color = Col;
        }
    }
}

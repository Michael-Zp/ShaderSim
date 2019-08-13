using ShaderUtils;
using ShaderUtils.Attributes;
using ShaderUtils.Mathematics;

namespace ShaderExample.Shaders
{
    class LightedFragment : FragmentShader
    {
        [Uniform]
        public Vector3 CameraPosition { private get; set; }
        [Uniform]
        public Vector4 AmbientLightColor { private get; set; }
        [Uniform]
        public Vector4 LightColor { private get; set; }
        [Uniform]
        public Vector3 LightDirection { private get; set; }

        [In]
        public Vector4 Col { private get; set; }
        [In]
        public Vector3 WorldPos { private get; set; }
        [In]
        public Vector3 N { private get; set; }

        private float Lambert(Vector3 n, Vector3 l)
        {
            return Max(0, Dot(n, l));
        }

        private float Specular(Vector3 n, Vector3 l, Vector3 v, float shininess)
        {
            Vector3 r = Reflect(-l, n);
            float illuminated = Step(Dot(n, l), 0);
            return Pow(Max(0, Dot(r, v)), shininess) * illuminated;
        }

        public override void Main()
        {
            Vector3 normal = Normalize(N);

            Vector4 ambient = AmbientLightColor * Col;
            Vector4 diffuse = Col * LightColor * Lambert(normal, -LightDirection);
            Vector4 specular = LightColor * Specular(N, LightDirection, Normalize(CameraPosition - WorldPos), 100f);

            Color = ambient + diffuse + specular;
        }
    }
}

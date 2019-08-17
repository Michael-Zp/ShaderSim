using System.Collections;
using System.Collections.Generic;

namespace ShaderUtils
{
    public abstract class RenderWrapper
    {
        public bool DepthEnabled { get; set; } = true;

        protected readonly Dictionary<string, object> Uniforms;

        protected VertexShader ActiveVertexShader;
        protected FragmentShader ActiveFragmentShader;
        protected IVertexArrayObject ActiveVAO;

        protected int Width;
        protected int Height;

        protected RenderWrapper()
        {
            Uniforms = new Dictionary<string, object>();
        }

        public void SetRenderSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void SetUniform<T>(string name, T value) where T : struct
        {
            if (Uniforms.ContainsKey(name))
            {
                Uniforms[name] = value;
            }
            else
            {
                Uniforms.Add(name, value);
            }
        }

        public void ActivateShader(VertexShader vertex, FragmentShader fragment)
        {
            ActiveVertexShader = vertex;
            ActiveFragmentShader = fragment;
        }

        public void DeactivateShader()
        {
            ActiveVertexShader = null;
            ActiveFragmentShader = null;
        }

        public void ActivateVAO(IVertexArrayObject vao)
        {
            ActiveVAO = vao;
        }

        public void DeactivateVAO()
        {
            ActiveVAO = null;
        }

        public abstract void DrawElementsInstanced(int instanceCount = 1);
    }
}

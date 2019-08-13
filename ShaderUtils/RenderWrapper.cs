using System.Collections;
using System.Collections.Generic;

namespace ShaderUtils
{
    public abstract class RenderWrapper
    {
        public bool DepthEnabled { get; set; } = true;

        protected readonly Dictionary<VertexArrayObject, IList<uint>> RenderData;
        protected readonly Dictionary<string, object> Uniforms;
        protected readonly Dictionary<Shader, Dictionary<string, IList>> Attributes;
        protected readonly Dictionary<Shader, Dictionary<string, IList>> InstancedAttributes;

        protected VertexShader ActiveVertexShader;
        protected FragmentShader ActiveFragmentShader;
        protected VertexArrayObject ActiveVAO;

        protected int Width;
        protected int Height;

        protected RenderWrapper()
        {
            RenderData = new Dictionary<VertexArrayObject, IList<uint>>();
            Uniforms = new Dictionary<string, object>();
            Attributes = new Dictionary<Shader, Dictionary<string, IList>>();
            InstancedAttributes = new Dictionary<Shader, Dictionary<string, IList>>();
        }

        public void SetRenderSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void SetRenderData(VertexArrayObject vao, IEnumerable<uint> data)
        {
            if (RenderData.ContainsKey(vao))
            {
                RenderData[vao] = (IList<uint>)data;
            }
            else
            {
                RenderData.Add(vao, (IList<uint>)data);
            }
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

        public void SetAttributes<T>(Shader shader, string name, IEnumerable<T> values, bool perInstance) where T : struct
        {
            if (perInstance)
            {
                if (InstancedAttributes.ContainsKey(shader))
                {
                    if (InstancedAttributes[shader].ContainsKey(name))
                    {
                        InstancedAttributes[shader][name] = (IList)values;
                    }
                    else
                    {
                        InstancedAttributes[shader].Add(name, (IList)values);
                    }
                }
                else
                {
                    InstancedAttributes.Add(shader, new Dictionary<string, IList>());
                    InstancedAttributes[shader].Add(name, (IList)values);
                }
            }
            else
            {
                if (Attributes.ContainsKey(shader))
                {
                    if (Attributes[shader].ContainsKey(name))
                    {
                        Attributes[shader][name] = (IList)values;
                    }
                    else
                    {
                        Attributes[shader].Add(name, (IList)values);
                    }
                }
                else
                {
                    Attributes.Add(shader, new Dictionary<string, IList>());
                    Attributes[shader].Add(name, (IList)values);
                }
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

        public void ActivateVAO(VertexArrayObject vao)
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

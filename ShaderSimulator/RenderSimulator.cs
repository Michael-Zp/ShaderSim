using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting;
using ShaderSim;
using ShaderSim.Attributes;

namespace ShaderSimulator
{
    class RenderSimulator
    {
        private readonly Dictionary<IVertexArrayObject, IEnumerable> _renderData;
        private readonly Dictionary<string, object> _uniforms;
        private readonly Dictionary<Shader, Dictionary<string, IEnumerable>> _attributes;
        private readonly Dictionary<Shader, Dictionary<string, IEnumerable>> _instancedAttributes;

        private Shader _activeVertexShader;
        private Shader _activeFragmentShader;
        private IVertexArrayObject _activeVAO;

        public RenderSimulator()
        {
            _renderData = new Dictionary<IVertexArrayObject, IEnumerable>();
            _uniforms = new Dictionary<string, object>();
            _attributes = new Dictionary<Shader, Dictionary<string, IEnumerable>>();
            _instancedAttributes = new Dictionary<Shader, Dictionary<string, IEnumerable>>();
        }

        public void SetRenderData<T>(IVertexArrayObject vao, IEnumerable<T> data) where T : struct
        {
            if (_renderData.ContainsKey(vao))
            {
                _renderData[vao] = data;
            }
            else
            {
                _renderData.Add(vao, data);
            }
        }

        public void SetUniform<T>(string name, T value) where T : struct
        {
            if (_uniforms.ContainsKey(name))
            {
                _uniforms[name] = value;
            }
            else
            {
                _uniforms.Add(name, value);
            }
        }

        public void SetAttributes<T>(Shader shader, string name, IEnumerable<T> values, bool perInstance) where T : struct
        {
            if (perInstance)
            {
                if (_instancedAttributes.ContainsKey(shader))
                {
                    if (_instancedAttributes[shader].ContainsKey(name))
                    {
                        _instancedAttributes[shader][name] = values;
                    }
                    else
                    {
                        _instancedAttributes[shader].Add(name, values);
                    }
                }
                else
                {
                    _instancedAttributes.Add(shader, new Dictionary<string, IEnumerable>());
                    _instancedAttributes[shader].Add(name, values);
                }
            }
            else
            {
                if (_attributes.ContainsKey(shader))
                {
                    if (_attributes[shader].ContainsKey(name))
                    {
                        _attributes[shader][name] = values;
                    }
                    else
                    {
                        _attributes[shader].Add(name, values);
                    }
                }
                else
                {
                    _attributes.Add(shader, new Dictionary<string, IEnumerable>());
                    _attributes[shader].Add(name, values);
                }
            }
        }

        public void ActivateShader(Shader vertex, Shader fragment)
        {
            _activeVertexShader = vertex;
            _activeFragmentShader = fragment;
        }

        public void DeactivateShader()
        {
            _activeVertexShader = null;
            _activeFragmentShader = null;
        }

        public void ActivateVAO(IVertexArrayObject vao)
        {
            _activeVAO = vao;
        }

        public void DeactivateVAO()
        {
            _activeVAO = null;
        }

        public void DrawElementsInstanced(int instanceCount)
        {
            SetUniforms();
        }

        private void SetUniforms()
        {
            MethodInfo method = typeof(Shader).GetMethod("SetValue");
            if (method != null)
            {
                foreach (var uniform in _uniforms)
                {
                    MethodInfo generic = method.MakeGenericMethod(typeof(UniformAttribute), uniform.Value.GetType());
                    generic.Invoke(_activeVertexShader, new object[] { uniform.Key, uniform.Value });
                    generic.Invoke(_activeFragmentShader, new object[] { uniform.Key, uniform.Value });
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using ShaderRenderer;
using ShaderSim;
using ShaderSim.Attributes;
using ShaderSim.Mathematics;

namespace ShaderSimulator
{
    public class RenderSimulator
    {
        private readonly Dictionary<IVertexArrayObject, IList<uint>> _renderData;
        private readonly Dictionary<string, object> _uniforms;
        private readonly Dictionary<Shader, Dictionary<string, IList>> _attributes;
        private readonly Dictionary<Shader, Dictionary<string, IList>> _instancedAttributes;

        private readonly MethodInfo _setValueMethod;
        private Shader _activeVertexShader;
        private Shader _activeFragmentShader;
        private IVertexArrayObject _activeVAO;

        private readonly Dictionary<string, IList> _vertexValues;
        private Dictionary<string, IList[,]> _fragments;

        private Renderer _renderer;

        public RenderSimulator()
        {
            _renderer = new Renderer();

            _renderData = new Dictionary<IVertexArrayObject, IList<uint>>();
            _uniforms = new Dictionary<string, object>();
            _attributes = new Dictionary<Shader, Dictionary<string, IList>>();
            _instancedAttributes = new Dictionary<Shader, Dictionary<string, IList>>();

            _setValueMethod = typeof(Shader).GetMethod("SetValue");

            _vertexValues = new Dictionary<string, IList>();
            _fragments = new Dictionary<string, IList[,]>();
        }

        public void SetRenderData(IVertexArrayObject vao, IEnumerable<uint> data)
        {
            if (_renderData.ContainsKey(vao))
            {
                _renderData[vao] = (IList<uint>)data;
            }
            else
            {
                _renderData.Add(vao, (IList<uint>)data);
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
                        _instancedAttributes[shader][name] = (IList)values;
                    }
                    else
                    {
                        _instancedAttributes[shader].Add(name, (IList)values);
                    }
                }
                else
                {
                    _instancedAttributes.Add(shader, new Dictionary<string, IList>());
                    _instancedAttributes[shader].Add(name, (IList)values);
                }
            }
            else
            {
                if (_attributes.ContainsKey(shader))
                {
                    if (_attributes[shader].ContainsKey(name))
                    {
                        _attributes[shader][name] = (IList)values;
                    }
                    else
                    {
                        _attributes[shader].Add(name, (IList)values);
                    }
                }
                else
                {
                    _attributes.Add(shader, new Dictionary<string, IList>());
                    _attributes[shader].Add(name, (IList)values);
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

        public void DrawElementsInstanced(int instanceCount = 1)
        {
            SetUniforms();
            CalculateVertexStep(instanceCount);
            CalculateFragments();
        }

        private void SetUniforms()
        {
            if (_setValueMethod != null)
            {
                foreach (var uniform in _uniforms)
                {
                    SetUniform(uniform.Key, uniform.Value);
                }
            }
        }

        private void SetUniform(string name, object value)
        {
            MethodInfo generic = _setValueMethod.MakeGenericMethod(typeof(UniformAttribute), value.GetType());
            generic.Invoke(_activeVertexShader, new object[] { name, value });
            generic.Invoke(_activeFragmentShader, new object[] { name, value });
        }

        private void CalculateVertexStep(int instanceCount)
        {
            for (int i = 0; i < instanceCount; i++)
            {
                foreach (var instancedAttribute in _instancedAttributes[_activeVertexShader])
                {
                    SetAttribute(_activeVertexShader, instancedAttribute.Key, instancedAttribute.Value[i]);
                }

                for (int j = 0; j < _renderData[_activeVAO].Count; j++)
                {
                    foreach (var attribute in _attributes[_activeVertexShader])
                    {
                        SetAttribute(_activeVertexShader, attribute.Key, attribute.Value[(int)_renderData[_activeVAO][j]]);
                    }

                    _activeVertexShader.Main();

                    foreach (var outValue in _activeVertexShader.GetOutValues())
                    {
                        if (_vertexValues.ContainsKey(outValue.Key))
                        {
                            _vertexValues[outValue.Key].Add(outValue.Value);
                        }
                        else
                        {
                            _vertexValues.Add(outValue.Key, new List<object> { outValue.Value });
                        }
                    }
                }
            }
        }

        private void SetAttribute(Shader shader, string name, object value)
        {
            MethodInfo generic = _setValueMethod.MakeGenericMethod(typeof(InAttribute), value.GetType());
            generic.Invoke(shader, new object[] { name, value });
        }

        private void CalculateFragments()
        {
            Vector2[,] positions = new Vector2[_renderer.Width, _renderer.Height];

            foreach (var key in _vertexValues.Keys)
            {
                _fragments.Add(key, new IList[_renderer.Width, _renderer.Height]);
            }

            for (int x = 0; x < _renderer.Width; x++)
            {
                for (int y = 0; y < _renderer.Height; y++)
                {
                    positions[x, y] = CalculatePosition(x, y);
                    //TODO interpolate values
                }
            }
        }

        private Vector2 CalculatePosition(int x, int y)
        {
            Vector2 fragSize = new Vector2(2f / _renderer.Width, 2f / _renderer.Height);

            return new Vector2(fragSize.X * x + fragSize.X / 2 - 1, fragSize.Y * y + fragSize.Y / 2 - 1);
        }

        private void CalculateFragmentStep()
        {
            //TODO implement Fragment step
        }
    }
}

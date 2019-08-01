using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ShaderRenderer;
using ShaderSim;
using ShaderSim.Attributes;
using ShaderSim.Mathematics;

namespace ShaderSimulator
{
    public class RenderSimulator
    {
        public bool DepthEnabled { get; set; } = true;

        private readonly Dictionary<IVertexArrayObject, IList<uint>> _renderData;
        private readonly Dictionary<string, object> _uniforms;
        private readonly Dictionary<Shader, Dictionary<string, IList>> _attributes;
        private readonly Dictionary<Shader, Dictionary<string, IList>> _instancedAttributes;

        private readonly MethodInfo _setValueMethod;
        private VertexShader _activeVertexShader;
        private FragmentShader _activeFragmentShader;
        private IVertexArrayObject _activeVAO;

        private readonly List<Vector4> _vertexPositions;
        private readonly Dictionary<string, IList> _vertexValues;
        private readonly List<Triangle> _primitives;
        private List<float>[,] _depths;
        private Dictionary<string, IList>[,] _fragments;

        private Renderer _renderer;

        public RenderSimulator()
        {
            _renderer = new Renderer();

            _renderData = new Dictionary<IVertexArrayObject, IList<uint>>();
            _uniforms = new Dictionary<string, object>();
            _attributes = new Dictionary<Shader, Dictionary<string, IList>>();
            _instancedAttributes = new Dictionary<Shader, Dictionary<string, IList>>();

            _setValueMethod = typeof(Shader).GetMethod("SetValue");

            _vertexPositions = new List<Vector4>();
            _vertexValues = new Dictionary<string, IList>();
            _primitives = new List<Triangle>();
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

        public void ActivateShader(VertexShader vertex, FragmentShader fragment)
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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();

            SetUniforms();
            CalculateVertexStep(instanceCount);
            GeneratePrimitives();
            CalculateFragments();
            Bitmap result = CalculateFragmentStep();

            stopwatch.Stop();
            Console.WriteLine($"Time: Ticks = {stopwatch.Elapsed.Ticks}; Ms = {stopwatch.Elapsed.Milliseconds}");

            Form form = new Form();
            form.Text = "Image Viewer";
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = result;
            form.Width = result.Width;
            form.Height = result.Height;
            pictureBox.Dock = DockStyle.Fill;
            form.Controls.Add(pictureBox);
            Application.Run(form);
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

                    _vertexPositions.Add(_activeVertexShader.Position);

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

        private void GeneratePrimitives()
        {
            for (int i = 0; i < _vertexValues.First().Value.Count; i += 3)
            {
                Triangle triangle = new Triangle();
                for (int j = 0; j < 3; j++)
                {
                    triangle[j].Add(VertexShader.PositionName, _vertexPositions[i + j]);
                    foreach (var key in _vertexValues.Keys)
                    {
                        triangle[j].Add(key, _vertexValues[key][i + j]);
                    }
                }
                _primitives.Add(triangle);
            }
        }

        private void CalculateFragments()
        {
            Vector2[,] positions = new Vector2[_renderer.Width, _renderer.Height];

            _depths = new List<float>[_renderer.Width, _renderer.Height];
            _fragments = new Dictionary<string, IList>[_renderer.Width, _renderer.Height];

            for (int x = 0; x < _renderer.Width; x++)
            {
                for (int y = 0; y < _renderer.Height; y++)
                {
                    positions[x, y] = CalculatePosition(x, y);
                    foreach (var primitive in _primitives)
                    {
                        Vector3 baricentric = Barycentric(positions[x, y], primitive);
                        if (baricentric.X >= 0 && baricentric.Y >= 0 && baricentric.Z >= 0)
                        {
                            if (_depths[x, y] == null)
                            {
                                _depths[x, y] = new List<float>();
                            }

                            _depths[x, y].Add(((Vector4)primitive[0][VertexShader.PositionName]).Z * baricentric.X +
                                            ((Vector4)primitive[1][VertexShader.PositionName]).Z * baricentric.Y +
                                            ((Vector4)primitive[2][VertexShader.PositionName]).Z * baricentric.Z);

                            if (_fragments[x, y] == null)
                            {
                                _fragments[x, y] = new Dictionary<string, IList>();
                            }
                            foreach (var key in _vertexValues.Keys)
                            {
                                if (!_fragments[x, y].ContainsKey(key))
                                {
                                    _fragments[x, y].Add(key, new List<object>());
                                }

                                MethodInfo mult = primitive[0][key].GetType().GetMethod("op_Multiply", new Type[] { primitive[0][key].GetType(), typeof(float) });
                                MethodInfo add = primitive[0][key].GetType().GetMethod("op_Addition", new Type[] { primitive[0][key].GetType(), primitive[0][key].GetType() });
                                var value0 = mult.Invoke(primitive[0][key], new object[] { primitive[0][key], baricentric.X });
                                var value1 = mult.Invoke(primitive[1][key], new object[] { primitive[1][key], baricentric.Y });
                                var value2 = mult.Invoke(primitive[2][key], new object[] { primitive[2][key], baricentric.Z });
                                var add1 = add.Invoke(value0, new object[] { value0, value1 });
                                var add2 = add.Invoke(add1, new object[] { add1, value2 });
                                _fragments[x, y][key].Add(add2);
                            }
                        }
                    }
                }
            }
        }

        private Vector2 CalculatePosition(int x, int y)
        {
            Vector2 fragSize = new Vector2(2f / _renderer.Width, 2f / _renderer.Height);

            return new Vector2(fragSize.X * x + fragSize.X / 2 - 1, -(fragSize.Y * y + fragSize.Y / 2 - 1));
        }

        private Vector3 Barycentric(System.Numerics.Vector2 pos, Triangle triangle)
        {
            System.Numerics.Vector2 t0 = new System.Numerics.Vector2(((Vector4)triangle[0][VertexShader.PositionName]).X, ((Vector4)triangle[0][VertexShader.PositionName]).Y);
            System.Numerics.Vector2 t1 = new System.Numerics.Vector2(((Vector4)triangle[1][VertexShader.PositionName]).X, ((Vector4)triangle[1][VertexShader.PositionName]).Y);
            System.Numerics.Vector2 t2 = new System.Numerics.Vector2(((Vector4)triangle[2][VertexShader.PositionName]).X, ((Vector4)triangle[2][VertexShader.PositionName]).Y);

            System.Numerics.Vector2 v0 = t1 - t0;
            System.Numerics.Vector2 v1 = t2 - t0;
            System.Numerics.Vector2 v2 = pos - t0;

            float d00 = System.Numerics.Vector2.Dot(v0, v0);
            float d01 = System.Numerics.Vector2.Dot(v0, v1);
            float d11 = System.Numerics.Vector2.Dot(v1, v1);
            float d20 = System.Numerics.Vector2.Dot(v2, v0);
            float d21 = System.Numerics.Vector2.Dot(v2, v1);
            float denom = d00 * d11 - d01 * d01;

            float v = (d11 * d20 - d01 * d21) / denom;
            float w = (d00 * d21 - d01 * d20) / denom;
            float u = 1 - v - w;

            return new Vector3(u, v, w);
        }

        private Bitmap CalculateFragmentStep()
        {
            Bitmap bmp = new Bitmap(_fragments.GetLength(0), _fragments.GetLength(1));
            for (int x = 0; x < _fragments.GetLength(0); x++)
            {
                for (int y = 0; y < _fragments.GetLength(1); y++)
                {
                    bmp.SetPixel(x, y, Color.Black);
                    if (_fragments[x, y] != null)
                    {
                        for (int i = 0; i < _fragments[x, y].Values.First().Count; i++)
                        {

                            bool closest = true;
                            if (DepthEnabled)
                            {
                                if (_depths[x, y][i] < 0)
                                {
                                    closest = false;
                                    continue;
                                }

                                for (int j = 0; j < _depths[x, y].Count; j++)
                                {
                                    if (i != j && _depths[x, y][j] >= 0)
                                    {
                                        if (_depths[x, y][i] > _depths[x, y][j])
                                        {
                                            closest = false;
                                        }
                                    }
                                }
                            }
                            if (closest)
                            {
                                foreach (var key in _fragments[x, y].Keys)
                                {
                                    SetAttribute(_activeFragmentShader, key, _fragments[x, y][key][i]);
                                }
                                _activeFragmentShader.Main();
                                foreach (var outValue in _activeFragmentShader.GetOutValues())
                                {
                                    if (outValue.Key == FragmentShader.ColorName)
                                    {
                                        Color color = Color.FromArgb((int)(((Vector4)outValue.Value).A * 255),
                                            (int)(((Vector4)outValue.Value).R * 255),
                                            (int)(((Vector4)outValue.Value).G * 255),
                                            (int)(((Vector4)outValue.Value).B * 255));

                                        bmp.SetPixel(x, y, color);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return bmp;
        }
    }
}

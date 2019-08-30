using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using BarycentricCudaLib;
using ShaderUtils;
using ShaderUtils.Attributes;
using ShaderUtils.Mathematics;

namespace ShaderSimulator
{
    public class RenderSimulator : RenderWrapper
    {
        public override bool DepthEnabled { get; set; }

        private readonly Dictionary<string, object> _uniforms;

        protected readonly Dictionary<IVertexArrayObject, IList<uint>> RenderData;
        protected readonly Dictionary<Shader, Dictionary<string, IList>> Attributes;
        protected readonly Dictionary<Shader, Dictionary<string, IList>> InstancedAttributes;

        private VertexShader _activeVertexShader;
        private FragmentShader _activeFragmentShader;

        public Bitmap RenderResult { get; private set; }

        private readonly MethodInfo _setValueMethod;

        private readonly List<Vector4> _vertexPositions;
        private readonly Dictionary<string, IList> _vertexValues;
        private readonly List<Triangle> _primitives;
        private List<float>[,] _depths;
        private Dictionary<string, IList>[,] _fragments;
        private int[,] _fragmentCount;


        public RenderSimulator()
        {
            DepthEnabled = true;
            _uniforms = new Dictionary<string, object>();
            RenderData = new Dictionary<IVertexArrayObject, IList<uint>>();
            Attributes = new Dictionary<Shader, Dictionary<string, IList>>();
            InstancedAttributes = new Dictionary<Shader, Dictionary<string, IList>>();

            _setValueMethod = typeof(Shader).GetMethod("SetValue");

            _vertexPositions = new List<Vector4>();
            _vertexValues = new Dictionary<string, IList>();
            _primitives = new List<Triangle>();
        }

        public override void ActivateShader(VertexShader vertex, FragmentShader fragment)
        {
            _activeVertexShader = vertex;
            _activeFragmentShader = fragment;
        }

        public override void DeactivateShader()
        {
            _activeVertexShader = null;
            _activeFragmentShader = null;
        }

        public override void SetUniform<T>(string name, T value)
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

        public void SetRenderData(SimulatorVAO vao, IEnumerable<uint> data)
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

        public void SetAttributes<T>(Shader shader, string name, IList<T> values, bool perInstance) where T : struct
        {
            if (typeof(T) == typeof(Matrix4x4))
            {
                for (int i = 0; i < values.Count(); i++)
                {
                    values[i] = (T)(object)(Matrix4x4)System.Numerics.Matrix4x4.Transpose((Matrix4x4)(object)values[i]);
                }
            }
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

        public void DrawElementsInstanced(int instanceCount = 1)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();

            SetUniforms();
            Console.WriteLine($"SetUniforms Time: Ticks = {stopwatch.Elapsed.Ticks}; Ms = {stopwatch.Elapsed.TotalMilliseconds}");
            stopwatch.Restart();

            CalculateVertexStep(instanceCount);
            Console.WriteLine($"CalculateVertexStep Time: Ticks = {stopwatch.Elapsed.Ticks}; Ms = {stopwatch.Elapsed.TotalMilliseconds}");
            stopwatch.Restart();

            GeneratePrimitives();
            Console.WriteLine($"GeneratePrimitives Time: Ticks = {stopwatch.Elapsed.Ticks}; Ms = {stopwatch.Elapsed.TotalMilliseconds}");
            stopwatch.Restart();

            CalculateFragments();
            Console.WriteLine($"CalculateFragments Time: Ticks = {stopwatch.Elapsed.Ticks}; Ms = {stopwatch.Elapsed.TotalMilliseconds}");
            stopwatch.Restart();

            RenderResult = CalculateFragmentStep();
            Console.WriteLine($"RenderResult Time: Ticks = {stopwatch.Elapsed.Ticks}; Ms = {stopwatch.Elapsed.TotalMilliseconds}");
            stopwatch.Restart();

            stopwatch.Stop();
            Console.WriteLine($"Time: Ticks = {stopwatch.Elapsed.Ticks}; Ms = {stopwatch.Elapsed.TotalMilliseconds}");

            Console.WriteLine("");
            Console.WriteLine("#########################");
            Console.WriteLine("");

            Attributes.Clear();
            InstancedAttributes.Clear();
            RenderData.Clear();

            _vertexPositions.Clear();
            _vertexValues.Clear();
            _primitives.Clear();
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
                foreach (var instancedAttribute in InstancedAttributes[_activeVertexShader])
                {
                    SetAttribute(_activeVertexShader, instancedAttribute.Key, instancedAttribute.Value[i]);
                }

                for (int j = 0; j < RenderData[ActiveVAO].Count; j++)
                {
                    foreach (var attribute in Attributes[_activeVertexShader])
                    {
                        SetAttribute(_activeVertexShader, attribute.Key, attribute.Value[(int)RenderData[ActiveVAO][j]]);
                    }

                    _activeVertexShader.Main();

                    _vertexPositions.Add(_activeVertexShader.Position * (1 / _activeVertexShader.Position.W));

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
            shader.InAttributeMethods[name].Invoke(shader, new object[] { name, value });
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
            //Vector2[,] positions = new Vector2[Width, Height];

            _depths = new List<float>[Width, Height];
            _fragments = new Dictionary<string, IList>[Width, Height];
            _fragmentCount = new int[Width, Height];

            List<List<BarycentricReturn>> interpolated = new List<List<BarycentricReturn>>();

            double totalOnlyKernelTime = 0;
            double tempKernelTime = 0;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            foreach (var primitive in _primitives)
            {
                interpolated.Add(BarycentricCuda.Execute(primitive, Width, Height, out tempKernelTime));
                totalOnlyKernelTime += tempKernelTime;
            }

            Console.WriteLine("Cuda Kernel Time: " + totalOnlyKernelTime);
            Console.WriteLine("Cuda Method Time: " + sw.Elapsed.TotalMilliseconds);

            foreach (var inter in interpolated)
            {
                foreach (var item in inter)
                {
                    if (_depths[item.X, item.Y] == null)
                    {
                        _depths[item.X, item.Y] = new List<float>();
                        _fragments[item.X, item.Y] = new Dictionary<string, IList>();
                        _fragmentCount[item.X, item.Y] = 0;
                    }

                    _depths[item.X, item.Y].Add(item.Depth);
                    _fragmentCount[item.X, item.Y]++;

                    int currentIndex = 0;

                    foreach (var key in _primitives[0][0].Keys)
                    {
                        if (key == VertexShader.PositionName)
                        {
                            continue;
                        }

                        if (!_fragments[item.X, item.Y].ContainsKey(key))
                        {
                            _fragments[item.X, item.Y].Add(key, new List<object>());
                        }

                        switch (_primitives[0][0][key])
                        {
                            case float _:
                                {
                                    _fragments[item.X, item.Y][key].Add((float)item.FragmentData[currentIndex]);
                                    break;
                                }
                            case Vector2 _:
                                {
                                    _fragments[item.X, item.Y][key].Add((Vector2)item.FragmentData[currentIndex]);
                                    break;
                                }
                            case Vector3 _:
                                {
                                    _fragments[item.X, item.Y][key].Add((Vector3)item.FragmentData[currentIndex]);
                                    break;
                                }
                            case Vector4 _:
                                {
                                    _fragments[item.X, item.Y][key].Add((Vector4)item.FragmentData[currentIndex]);
                                    break;
                                }
                        }
                        currentIndex++;
                    }
                }
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        struct Vector4ToInt
        {
            [FieldOffset(0)] public int ColorValue;
            [FieldOffset(0)] public byte valueB;
            [FieldOffset(1)] public byte valueG;
            [FieldOffset(2)] public byte valueR;
            [FieldOffset(3)] public byte valueA;
        }

        private Bitmap CalculateFragmentStep()
        {
            int[] imageData = new int[_fragments.GetLength(0) * _fragments.GetLength(1)];
            Vector4ToInt vec4ToInt = new Vector4ToInt();
            for (int x = 0; x < _fragments.GetLength(0); x++)
            {
                for (int y = 0; y < _fragments.GetLength(1); y++)
                {
                    if (_fragments[x, y] != null)
                    {
                        for (int i = 0; i < _fragmentCount[x, y]; i++)
                        {

                            bool closest = true;
                            if (DepthEnabled)
                            {
                                /*if (_depths[x, y][i] < 0)
                                {
                                    closest = false;
                                    continue;
                                }*/

                                for (int j = 0; j < _depths[x, y].Count; j++)
                                {
                                    if (i != j /*&& _depths[x, y][j] >= 0*/)
                                    {
                                        if (_depths[x, y][i] > _depths[x, y][j])
                                        {
                                            closest = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (closest)
                            {
                                foreach (var key in _fragments[x, y].Keys)
                                {
                                    var type = _activeFragmentShader.GetType();
                                    var prop = type.GetProperty(key);
                                    var value = _fragments[x, y][key][i];
                                    prop.SetValue(_activeFragmentShader, value);
                                }


                                _activeFragmentShader.Main();
                                foreach (var outValue in _activeFragmentShader.GetOutValues())
                                {
                                    if (outValue.Key == FragmentShader.ColorName)
                                    {
                                        vec4ToInt.ColorValue = 0;
                                        vec4ToInt.valueA = (byte)Math.Min((int)(((Vector4)outValue.Value).A * 255), 255);
                                        vec4ToInt.valueR = (byte)Math.Min((int)(((Vector4)outValue.Value).R * 255), 255);
                                        vec4ToInt.valueG = (byte)Math.Min((int)(((Vector4)outValue.Value).G * 255), 255);
                                        vec4ToInt.valueB = (byte)Math.Min((int)(((Vector4)outValue.Value).B * 255), 255);
                                        imageData[x + y * Width] = vec4ToInt.ColorValue;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return new Bitmap(Width, Height, Width, System.Drawing.Imaging.PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(imageData, 0));
        }
    }
}

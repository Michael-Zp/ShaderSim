using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using ShaderExample.Shaders;
using ShaderExample.Utils;
using ShaderRenderer;
using ShaderUtils;
using ShaderSimulator;
using Matrix4x4 = ShaderUtils.Mathematics.Matrix4x4;
using Vector4 = ShaderUtils.Mathematics.Vector4;

namespace ShaderExample
{
    class View
    {
        private readonly RenderWrapper _wrapper = new RenderSimulator();
        private VertexShader _vertex;
        private FragmentShader _fragment;

        readonly CameraPerspective _camera = new CameraPerspective();

        public View()
        {
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend);
        }

        public void Render(IEnumerable<Entity> entities)
        {
            _vertex = new LightedVertex();
            _fragment = new PassFragment();

            Dictionary<SimulatorVAO, int> simulatorVAOs = PrepareVAOs(entities);
            _camera.Position = new System.Numerics.Vector3(0f, 0f, 10f);
            _wrapper.SetUniform("Camera", (Matrix4x4)_camera.CalcMatrix());
            //_wrapper.SetUniform("Camera", (Matrix4x4)System.Numerics.Matrix4x4.Transpose(System.Numerics.Matrix4x4.CreateTranslation(new System.Numerics.Vector3(0f, 0.5f, 0))));
            _wrapper.ActivateShader(_vertex, _fragment);

            List<Texture2D> layers = new List<Texture2D>();

            foreach (var simulatorVAO in simulatorVAOs)
            {
                _wrapper.ActivateVAO(simulatorVAO.Key);

                _wrapper.DrawElementsInstanced(simulatorVAO.Value);
                layers.Add(new Texture2D(((RenderSimulator)_wrapper).RenderResult));

                _wrapper.DeactivateVAO();
            }
            _wrapper.DeactivateShader();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.Texture2D);
            layers[0].Bind();

            Drawer.DrawScreenQuad();

            Texture2D.Unbind();
            GL.Disable(EnableCap.Texture2D);
        }

        private Dictionary<SimulatorVAO, int> PrepareVAOs(IEnumerable<Entity> entities)
        {
            Dictionary<SimulatorVAO, int> simulatorVAOs = new Dictionary<SimulatorVAO, int>();

            Dictionary<Enums.EntityType, SimulatorVAO> simVAOs = new Dictionary<Enums.EntityType, SimulatorVAO>();
            Dictionary<Enums.EntityType, List<Matrix4x4>> transformations = new Dictionary<Enums.EntityType, List<Matrix4x4>>();
            Dictionary<Enums.EntityType, List<Vector4>> colors = new Dictionary<Enums.EntityType, List<Vector4>>();
            Dictionary<Enums.EntityType, int> instanceCounts = new Dictionary<Enums.EntityType, int>();

            foreach (var entity in entities)
            {
                if (!simVAOs.ContainsKey(entity.Type))
                {
                    Mesh mesh = null;
                    switch (entity.Type)
                    {
                        case Enums.EntityType.Triangle:
                            mesh = MeshCreator.CreateTriangle();
                            break;
                    }

                    if (mesh != null)
                    {
                        simVAOs.Add(entity.Type, VAOLoader.FromMesh<SimulatorVAO>(mesh, new Tuple<VertexShader, FragmentShader>(_vertex, _fragment), new object[] { _wrapper }));
                        transformations.Add(entity.Type, new List<Matrix4x4>());
                        colors.Add(entity.Type, new List<Vector4>());
                        instanceCounts.Add(entity.Type, 0);
                    }
                }
                if (transformations.ContainsKey(entity.Type))
                {
                    transformations[entity.Type].Add(entity.Transformation);
                    colors[entity.Type].Add(entity.Color);
                    instanceCounts[entity.Type]++;
                }
            }

            foreach (var key in simVAOs.Keys)
            {
                simVAOs[key].SetAttribute("InstanceTransformation", new Tuple<VertexShader, FragmentShader>(_vertex, _fragment), transformations[key], true);
                simVAOs[key].SetAttribute("Color", new Tuple<VertexShader, FragmentShader>(_vertex, _fragment), colors[key], true);
                simulatorVAOs.Add(simVAOs[key], instanceCounts[key]);
            }

            return simulatorVAOs;
        }

        public void Resize(int width, int height)
        {
            GL.Viewport(0, 0, width, height);
            _wrapper.SetRenderSize(width, height);
            _camera.Aspect = (float)width / height;
        }
    }
}

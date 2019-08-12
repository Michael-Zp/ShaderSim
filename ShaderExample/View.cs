using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using ShaderExample.Graphics;
using ShaderExample.Shaders;
using ShaderExample.Utils;
using ShaderSim;
using ShaderSimulator;
using Matrix4x4 = ShaderSim.Mathematics.Matrix4x4;
using Vector4 = ShaderSim.Mathematics.Vector4;

namespace ShaderExample
{
    class View
    {
        private readonly RenderSimulator _simulator = new RenderSimulator();
        private VertexShader _vertex;
        private FragmentShader _fragment;

        CameraPerspective _camera = new CameraPerspective();

        public View()
        {
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend);
        }

        public void Render(IEnumerable<Entity> entities)
        {
            _vertex = new LightedVertex();
            _fragment = new PassFragment();

            List<Bitmap> layers = new List<Bitmap>();

            Dictionary<SimulatorVAO, int> simulatorVAOs = PrepareSimVAOs(entities);
            _camera.Position = new System.Numerics.Vector3(0f, 0f, 10f);
            _simulator.SetUniform("Camera", (Matrix4x4)_camera.CalcMatrix());
            //_simulator.SetUniform("Camera", (Matrix4x4)System.Numerics.Matrix4x4.Transpose(System.Numerics.Matrix4x4.CreateTranslation(new System.Numerics.Vector3(0f, 0.5f, 0))));
            _simulator.ActivateShader(_vertex, _fragment);
            foreach (var simulatorVaO in simulatorVAOs)
            {
                _simulator.ActivateVAO(simulatorVaO.Key);

                layers.Add(_simulator.DrawElementsInstanced(simulatorVaO.Value));

                _simulator.DeactivateVAO();
            }
            _simulator.DeactivateShader();

            List<Texture2D> images = new List<Texture2D>();
            foreach (var bitmap in layers)
            {
                images.Add(new Texture2D(bitmap));
            }

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.Texture2D);
            images[0].Bind();

            Drawer.DrawScreenQuad();

            Texture2D.Unbind();
            GL.Disable(EnableCap.Texture2D);
        }

        private Dictionary<SimulatorVAO, int> PrepareSimVAOs(IEnumerable<Entity> entities)
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
                        simVAOs.Add(entity.Type, VAOLoader.FromMesh<SimulatorVAO>(mesh, _vertex, new object[] { _simulator }));
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
                simVAOs[key].SetAttribute("InstanceTransformation", _vertex, transformations[key], true);
                simVAOs[key].SetAttribute("Color", _vertex, colors[key], true);
                simulatorVAOs.Add(simVAOs[key], instanceCounts[key]);
            }

            return simulatorVAOs;
        }

        public void Resize(int width, int height)
        {
            GL.Viewport(0, 0, width, height);
            _simulator.SetRenderSize(width, height);
            _camera.Aspect = (float)width / height;
        }
    }
}

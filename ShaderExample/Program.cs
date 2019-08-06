using System.Collections.Generic;
using OpenTK;
using ShaderExample.Shaders;
using ShaderExample.Utils;
using ShaderSim;
using ShaderSimulator;
using Vector3 = ShaderSim.Mathematics.Vector3;
using Vector4 = ShaderSim.Mathematics.Vector4;

namespace ShaderExample
{
    class Program
    {
        static void Main(string[] args)
        {
            GameWindow window = new GameWindow();
            Model model = new Model();
            View view = new View();

            window.UpdateFrame += (s, e) => model.Update((float)e.Time);
            window.RenderFrame += (s, e) => view.Render(model.Entities);
            window.RenderFrame += (s, e) => window.SwapBuffers();
            window.Resize += (s, e) => view.Resize(window.Width, window.Height);

            window.Run();
        }
    }
}

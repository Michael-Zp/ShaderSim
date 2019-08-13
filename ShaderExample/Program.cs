using System;
using OpenTK;
using ShaderExample.Shaders;
using ShaderTranslator;
using ShaderUtils;

namespace ShaderExample
{
    class Program
    {
        static void Main(string[] args)
        {
            RenderTranslator translator = new RenderTranslator();
            Shader shader = new LightedVertex();

            translator.RegisterShader(shader, @"C:\Users\Matze\Documents\Git\Master-Thesis\ShaderSim\ShaderExample\Shaders\LightedVertex.cs");
            Console.Read();

            //GameWindow window = new GameWindow();
            //Model model = new Model();
            //View view = new View();

            //window.UpdateFrame += (s, e) => model.Update((float)e.Time);
            //window.RenderFrame += (s, e) => view.Render(model.Entities);
            //window.RenderFrame += (s, e) => window.SwapBuffers();
            //window.Resize += (s, e) => view.Resize(window.Width, window.Height);

            //window.Run();
        }
    }
}

using System;
using System.IO;
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
            string directory = Directory.GetCurrentDirectory();
            translator.RegisterShader(shader, directory + @"\..\..\Shaders\LightedFragment.cs");
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

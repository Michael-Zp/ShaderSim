using OpenTK;

namespace ShaderRenderer
{
    public class Renderer
    {
        readonly GameWindow window;

        public int Width => window.Width;
        public int Height => window.Height;


        public Renderer()
        {
            window = new GameWindow();
        }
    }
}

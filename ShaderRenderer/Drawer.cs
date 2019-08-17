using OpenTK.Graphics.OpenGL;

namespace ShaderRenderer
{
    public static class Drawer
    {
        public static void DrawScreenQuad()
        {
            GL.Begin(PrimitiveType.Quads);
            //when using textures we have to set a texture coordinate for each vertex
            //by using the TexCoord command BEFORE the Vertex command
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(-1, -1);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(1, -1);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(1, 1);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(-1, 1);
            GL.End();
        }
    }
}

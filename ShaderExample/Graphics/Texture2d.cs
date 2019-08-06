using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;

namespace ShaderExample.Graphics
{
    public struct Texture2D
    {
        public enum TextureFunction
        {
            Repeat = TextureWrapMode.Repeat,
            MirroredRepeat = TextureWrapMode.MirroredRepeat,
            ClampToEdge = TextureWrapMode.ClampToEdge,
            ClampToBorder = TextureWrapMode.ClampToBorder
        }

        public Texture2D(Bitmap image, bool filterLinear = true, TextureFunction textureFunction = TextureFunction.MirroredRepeat)
        {
            TexturId = GL.GenTexture();
            Width = image.Width;
            Height = image.Height;
            Bind();
            BitmapData data = image.LockBits(new System.Drawing.Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            image.UnlockBits(data);
            GL.TexParameter(Target, TextureParameterName.TextureWrapS, (int)textureFunction);
            GL.TexParameter(Target, TextureParameterName.TextureWrapT, (int)textureFunction);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            var filterMagMode = filterLinear ? TextureMagFilter.Linear : TextureMagFilter.Nearest;
            var filterMinMode = filterLinear ? TextureMinFilter.LinearMipmapLinear : TextureMinFilter.NearestMipmapNearest;
            GL.TexParameter(Target, TextureParameterName.TextureMagFilter, (int)filterMagMode);
            GL.TexParameter(Target, TextureParameterName.TextureMinFilter, (int)filterMinMode);
            Unbind();
        }

        public void Bind()
        {
            GL.BindTexture(Target, TexturId);
        }

        public static void Unbind()
        {
            GL.BindTexture(Target, 0);
        }

        public int TexturId { get; }
        public int Width { get; }
        public int Height { get; }

        public const TextureTarget Target = TextureTarget.Texture2D;
    }
}

using ShaderUtils;
using ShaderUtils.Mathematics;

namespace ShaderExample.Utils
{
    public static class MeshCreator
    {
        public static Mesh CreateTriangle()
        {
            DefaultMesh triangle = new DefaultMesh();
            triangle.Pos.Add(new Vector3(0f, 0f, 0f));
            triangle.Normal.Add(new Vector3(0f, 0f, 1f));
            triangle.Pos.Add(new Vector3(1f, 1f, 0f));
            triangle.Normal.Add(new Vector3(0f, 0f, 1f));
            triangle.Pos.Add(new Vector3(0f, 1f, 0f));
            triangle.Normal.Add(new Vector3(0f, 0f, 1f));
            triangle.IDs.Add(0);
            triangle.IDs.Add(1);
            triangle.IDs.Add(2);

            return triangle;
        }
    }
}

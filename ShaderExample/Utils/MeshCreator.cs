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

        public static Mesh CreateteTrahedron()
        {
            DefaultMesh tetrahedron = new DefaultMesh();

            //bottom
            tetrahedron.Pos.Add(new Vector3(0.5f, -0.289f, 0.289f));
            tetrahedron.Normal.Add(new Vector3(0f, -1f, 0f));
            tetrahedron.Pos.Add(new Vector3(-0.5f, -0.289f, 0.289f));
            tetrahedron.Normal.Add(new Vector3(0f, -1f, 0f));
            tetrahedron.Pos.Add(new Vector3(0f, -0.289f, -0.577f));
            tetrahedron.Normal.Add(new Vector3(0f, -1f, 0f));
            tetrahedron.IDs.Add(0);
            tetrahedron.IDs.Add(1);
            tetrahedron.IDs.Add(2);

            //front
            tetrahedron.Pos.Add(new Vector3(-0.5f, -0.289f, 0.289f));
            tetrahedron.Normal.Add(System.Numerics.Vector3.Normalize(new Vector3(0f, 0.289f, 0.816f)));
            tetrahedron.Pos.Add(new Vector3(0.5f, -0.289f, 0.289f));
            tetrahedron.Normal.Add(System.Numerics.Vector3.Normalize(new Vector3(0f, 0.289f, 0.816f)));
            tetrahedron.Pos.Add(new Vector3(0f, 0.816f - 0.289f, 0f));
            tetrahedron.Normal.Add(System.Numerics.Vector3.Normalize(new Vector3(0f, 0.289f, 0.816f)));
            tetrahedron.IDs.Add(3);
            tetrahedron.IDs.Add(4);
            tetrahedron.IDs.Add(5);

            //right
            tetrahedron.Pos.Add(new Vector3(0f, 0f - 0.289f, -0.577f));
            tetrahedron.Normal.Add(System.Numerics.Vector3.Normalize(new Vector3(0.707f, 0.289f, -0.408f)));
            tetrahedron.Pos.Add(new Vector3(0.5f, 0f - 0.289f, 0.289f));
            tetrahedron.Normal.Add(System.Numerics.Vector3.Normalize(new Vector3(0.707f, 0.289f, -0.408f)));
            tetrahedron.Pos.Add(new Vector3(0f, 0.816f - 0.289f, 0f));
            tetrahedron.Normal.Add(System.Numerics.Vector3.Normalize(new Vector3(0.707f, 0.289f, -0.408f)));
            tetrahedron.IDs.Add(6);
            tetrahedron.IDs.Add(7);
            tetrahedron.IDs.Add(8);

            //left
            tetrahedron.Pos.Add(new Vector3(-0.5f, -0.289f, 0.289f));
            tetrahedron.Normal.Add(System.Numerics.Vector3.Normalize(new Vector3(-0.707f, 0.289f, -0.408f)));
            tetrahedron.Pos.Add(new Vector3(0f, -0.289f, -0.577f));
            tetrahedron.Normal.Add(System.Numerics.Vector3.Normalize(new Vector3(-0.707f, 0.289f, -0.408f)));
            tetrahedron.Pos.Add(new Vector3(0f, 0.816f - 0.289f, 0f));
            tetrahedron.Normal.Add(System.Numerics.Vector3.Normalize(new Vector3(-0.707f, 0.289f, -0.408f)));
            tetrahedron.IDs.Add(9);
            tetrahedron.IDs.Add(10);
            tetrahedron.IDs.Add(11);

            return tetrahedron;
        }
    }
}

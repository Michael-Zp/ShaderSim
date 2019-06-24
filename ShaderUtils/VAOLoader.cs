using System;
using System.Collections;
using System.Collections.Generic;

namespace ShaderSim
{
    public static class VAOLoader
    {
        public static T FromMesh<T>(Mesh mesh, Shader shader) where T : IVertexArrayObject, new()
        {
            T vao = new T();

            foreach (KeyValuePair<string, IEnumerable> pair in mesh.Attributes)
            {
                if (((ICollection)(pair.Value)).Count > 0)
                {
                    vao.SetAttribute(pair.Key, shader, (dynamic)pair.Value);
                }
            }
            vao.SetIndex(mesh.IDs);
            vao.PrimitiveType = 4;
            return vao;
        }
    }
}

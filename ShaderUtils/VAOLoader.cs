using System;
using System.Collections;
using System.Collections.Generic;

namespace ShaderUtils
{
    public static class VAOLoader
    {
        public static VertexArrayObject FromMesh(Mesh mesh, Shader vertexShader, RenderWrapper wrapper)
        {
            VertexArrayObject vao = new VertexArrayObject(wrapper);

            foreach (KeyValuePair<string, IEnumerable> pair in mesh.Attributes)
            {
                if (((ICollection)(pair.Value)).Count > 0)
                {
                    vao.SetAttribute(pair.Key, vertexShader, (dynamic)pair.Value);
                }
            }
            vao.SetIndex(mesh.IDs);
            return vao;
        }
    }
}

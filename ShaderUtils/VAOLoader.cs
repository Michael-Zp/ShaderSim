using System;
using System.Collections;
using System.Collections.Generic;

namespace ShaderSim
{
    public static class VAOLoader
    {
        public static T FromMesh<T>(Mesh mesh, Shader vertexShader, Object[] args) where T : IVertexArrayObject
        {
            IVertexArrayObject vao = (IVertexArrayObject)Activator.CreateInstance(typeof(T), args);

            foreach (KeyValuePair<string, IEnumerable> pair in mesh.Attributes)
            {
                if (((ICollection)(pair.Value)).Count > 0)
                {
                    vao.SetAttribute(pair.Key, vertexShader, (dynamic)pair.Value);
                }
            }
            vao.SetIndex(mesh.IDs);
            return (T)vao;
        }
    }
}

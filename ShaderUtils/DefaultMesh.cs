using System.Collections.Generic;
using ShaderSim.Mathematics;

namespace ShaderSim
{
    public class DefaultMesh : Mesh
    {
        public List<Vector3> Pos { get; }
        public List<Vector3> Normal { get; }
        public List<Vector2> TexCoord { get; }

        public DefaultMesh()
        {
            Pos = AddAttribute<Vector3>(nameof(Pos));
            Normal = AddAttribute<Vector3>(nameof(Normal));
            TexCoord = AddAttribute<Vector2>(nameof(TexCoord));
        }
    }
}

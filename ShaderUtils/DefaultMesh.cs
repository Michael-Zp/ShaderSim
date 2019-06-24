using System.Collections.Generic;
using ShaderSim.Mathematics;

namespace ShaderSim
{
    public class DefaultMesh : Mesh
    {
        public List<Vector3> Position { get; }
        public List<Vector3> Normal { get; }
        public List<Vector2> TexCoord { get; }

        public DefaultMesh()
        {
            Position = AddAttribute<Vector3>(nameof(Position));
            Normal = AddAttribute<Vector3>(nameof(Normal));
            TexCoord = AddAttribute<Vector2>(nameof(TexCoord));
        }
    }
}

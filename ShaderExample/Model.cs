using System.Collections.Generic;
using System.Numerics;
using ShaderExample.Utils;

namespace ShaderExample
{
    class Model
    {
        private List<Entity> _entities;

        public IEnumerable<Entity> Entities => _entities;

        public Model()
        {
            _entities = new List<Entity>();

            _entities.Add(new Entity(Enums.EntityType.Triangle, new Vector4(1f, 0, 0, 1), new Vector3(0.5f, 0, 0), Vector3.Zero, Vector3.One));
            _entities.Add(new Entity(Enums.EntityType.Triangle, new Vector4(1f, 1, 0, 1), new Vector3(-0.5f, 0, -2), Vector3.Zero, Vector3.One));
        }

        public void Update(float deltaTime)
        {
            _entities[0].Rotate(new Vector3(0, 0, deltaTime));
            if (_entities[0].Color == new Vector4(1, 0, 0, 1))
            {
                _entities[0].Color = new Vector4(0, 1, 0, 1);
            }
            else
            {
                _entities[0].Color = new Vector4(1, 0, 0, 1);
            }
        }
    }
}

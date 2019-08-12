﻿using System.Numerics;
using ShaderExample.Utils;

namespace ShaderExample
{
    public class Entity
    {
        public Enums.EntityType Type { get; }
        public Vector4 Color { get; set; }

        public Matrix4x4 AdditionalTransformation { private get; set; } = Matrix4x4.Identity;

        public Matrix4x4 Transformation => Matrix4x4.Transpose(Matrix4x4.CreateScale(_scale * ScaleFactor) *
                                           MatrixHelper.CreateRotation(_rotation) *
                                           Matrix4x4.CreateTranslation(_position) * AdditionalTransformation);

        private Vector3 _position;
        private Vector3 _rotation;
        private Vector3 _scale;

        public float ScaleFactor { private get; set; }


        public Entity(Enums.EntityType type, Vector4 color, Vector3 position, Vector3 rotation, float scale) : this(type, color, position,
            rotation, new Vector3(scale))
        {
        }

        public Entity(Enums.EntityType type, Vector4 color, Vector3 position, Vector3 rotation, Vector3 scale)
        {
            Type = type;
            Color = color;
            _position = position;
            _rotation = rotation;
            _scale = scale;
            ScaleFactor = 1;
        }

        public void Rotate(Vector3 rotation)
        {
            _rotation += rotation;
        }

        public void Translate(Vector3 translation)
        {
            _position += translation;
        }

        public void Scale(float scale) => Scale(new Vector3(scale));

        public void Scale(Vector3 scale)
        {
            _scale *= scale;
        }
    }
}

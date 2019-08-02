using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ShaderSim.Attributes;
using ShaderSim.Mathematics;

namespace ShaderSim
{
    public abstract class Shader
    {
        public abstract void Main();

        public void SetValue<TAttribute, TValue>(string name, TValue value) where TAttribute : Attribute where TValue : struct
        {
            if (GetType().GetProperty(name) != null)
            {
                if (GetType().GetProperty(name).GetCustomAttribute<TAttribute>() != null)
                {
                    GetType().GetProperty(name)?.SetValue(this, value);
                }
            }
        }

        public IEnumerable<KeyValuePair<string, object>> GetOutValues()
        {
            IEnumerable<PropertyInfo> properties = GetType().GetProperties();

            return properties.Where(property => property.GetCustomAttribute<OutAttribute>() != null).ToDictionary(property => property.Name, property => property.GetValue(this));
        }

        [Translation("max")]
        protected float Max(float val1, float val2)
        {
            return Math.Max(val1, val2);
        }

        [Translation("pow")]
        protected float Pow(float val1, float val2)
        {
            return (float)Math.Pow(val1, val2);
        }

        [Translation("step")]
        protected float Step(float edge, float x)
        {
            return x > edge ? 1f : 0f;
        }

        [Translation("step")]
        protected Vector2 Step(Vector2 edge, Vector2 x)
        {
            return new Vector2(Step(edge.X, x.X), Step(edge.Y, x.Y));
        }

        [Translation("step")]
        protected Vector3 Step(Vector3 edge, Vector3 x)
        {
            return new Vector3(Step(edge.X, x.X), Step(edge.Y, x.Y), Step(edge.Z, x.Z));
        }

        [Translation("step")]
        protected Vector4 Step(Vector4 edge, Vector4 x)
        {
            return new Vector4(Step(edge.X, x.X), Step(edge.Y, x.Y), Step(edge.Z, x.Z), Step(edge.W, x.W));
        }

        [Translation("dot")]
        protected float Dot(Vector2 v1, Vector2 v2)
        {
            return System.Numerics.Vector2.Dot(v1, v2);
        }

        [Translation("dot")]
        protected float Dot(Vector3 v1, Vector3 v2)
        {
            return System.Numerics.Vector3.Dot(v1, v2);
        }

        [Translation("dot")]
        protected float Dot(Vector4 v1, Vector4 v2)
        {
            return System.Numerics.Vector4.Dot(v1, v2);
        }

        [Translation("reflect")]
        protected Vector2 Reflect(Vector2 v1, Vector2 v2)
        {
            return System.Numerics.Vector2.Reflect(v1, v2);
        }

        [Translation("cross")]
        protected Vector3 Cross(Vector3 v1, Vector3 v2)
        {
            return System.Numerics.Vector3.Cross(v1, v2);
        }

        [Translation("reflect")]
        protected Vector3 Reflect(Vector3 v1, Vector3 v2)
        {
            return System.Numerics.Vector3.Reflect(v1, v2);
        }

        protected Vector2 Normalize(Vector2 v)
        {
            return System.Numerics.Vector2.Normalize(v);
        }

        protected Vector3 Normalize(Vector3 v)
        {
            return System.Numerics.Vector3.Normalize(v);
        }

        protected Vector4 Normalize(Vector4 v)
        {
            return System.Numerics.Vector4.Normalize(v);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ShaderUtils.Attributes;
using ShaderUtils.Mathematics;

namespace ShaderUtils
{
    public abstract class Shader
    {
        [Translation("main")]
        public abstract void Main();

        public Dictionary<string, MethodInfo> InAttributeMethods = new Dictionary<string, MethodInfo>();
        
        private Dictionary<string, PropertyInfo> _outProperties = new Dictionary<string, PropertyInfo>();
        private Dictionary<string, MethodInfo> _outValueGetMethods = null;

        protected Shader()
        {
            IEnumerable<PropertyInfo> properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            _outProperties = properties.Where(property => property.GetCustomAttribute<OutAttribute>() != null).ToDictionary(property => property.Name, property => property);
            
            foreach(var inProperty in properties.Where(property => property.GetCustomAttribute<InAttribute>() != null))
            {
                MethodInfo generic = typeof(Shader).GetMethod("SetValue").MakeGenericMethod(typeof(InAttribute), inProperty.GetValue(this).GetType());
                InAttributeMethods.Add(inProperty.Name, generic);
            }
        }

        public void SetValue<TAttribute, TValue>(string name, TValue value) where TAttribute : Attribute where TValue : struct
        {
            PropertyInfo info = GetType().GetProperty(name);

            if (info != null)
            {
                if (info.GetCustomAttribute<TAttribute>() != null)
                {
                    info?.SetValue(this, value);
                }
            }
        }


        public IEnumerable<KeyValuePair<string, object>> GetOutValues()
        {
            if(_outValueGetMethods == null)
            {
                _outValueGetMethods = new Dictionary<string, MethodInfo>();
                foreach (var key in _outProperties.Keys)
                {
                    _outValueGetMethods.Add(key, _outProperties[key].GetGetMethod());
                }
            }

            Dictionary<string, object> ret = new Dictionary<string, object>();

            foreach(var key in _outProperties.Keys)
            {
                ret.Add(key, _outValueGetMethods[key].Invoke(this, null));
            }

            return ret;
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

        [Translation("normalize")]
        protected Vector2 Normalize(Vector2 v)
        {
            return System.Numerics.Vector2.Normalize(v);
        }

        [Translation("normalize")]
        protected Vector3 Normalize(Vector3 v)
        {
            return System.Numerics.Vector3.Normalize(v);
        }

        [Translation("normalize")]
        protected Vector4 Normalize(Vector4 v)
        {
            return System.Numerics.Vector4.Normalize(v);
        }
    }
}

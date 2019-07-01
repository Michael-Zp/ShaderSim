using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ShaderSim.Attributes;

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
    }
}

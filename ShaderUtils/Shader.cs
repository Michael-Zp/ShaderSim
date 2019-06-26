using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting.Channels;

namespace ShaderSim
{
    public abstract class Shader
    {
        public abstract void Main();

        public void SetValue<T>(string name, T value) where T : struct
        {
            GetType().GetProperty(name)?.SetValue(this, value);
        }

        public IEnumerable<KeyValuePair<string, object>> GetValues()
        {
            IEnumerable<PropertyInfo> properties = GetType().GetProperties();

            Dictionary<string, object> values = new Dictionary<string, object>();

            foreach (var property in properties)
            {
                values.Add(property.Name, property.GetValue(this));
            }

            return values;
        }
    }
}

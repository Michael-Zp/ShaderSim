using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ShaderSim
{
    public class Mesh
    {
        public List<uint> IDs { get; }

        private readonly Dictionary<string, IEnumerable> _attributes;
        public IEnumerable<KeyValuePair<string, IEnumerable>> Attributes => _attributes;

        public Mesh()
        {
            IDs = new List<uint>();
            _attributes = new Dictionary<string, IEnumerable>();
        }

        public List<T> AddAttribute<T>(string name) where T : struct
        {
            if (_attributes.ContainsKey(name))
                throw new ArgumentException($"Attribute{name} already exists.");
            _attributes.Add(name, new List<T>());
            return (List<T>)_attributes[name];
        }

        public bool Contains(string name)
        {
            return _attributes.ContainsKey(name);
        }

        public void AddAttributeValue<T>(string name, T value) where T : struct
        {
            if (!_attributes.ContainsKey(name))
                throw new ArgumentException($"Attribute{name} does not exist.");
            if (_attributes[name].GetType().GetGenericArguments().Single() != value.GetType())
                throw new ArgumentException($"Attribute{name} has type{_attributes[name].GetType().GetGenericArguments().Single().FullName}");
            ((IList<T>)_attributes[name]).Add(value);
        }
    }
}

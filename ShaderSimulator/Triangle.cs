﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ShaderUtils.Mathematics;

namespace ShaderSimulator
{
    public class Triangle
    {
        private readonly Dictionary<string, object>[] _vertices;
        public Dictionary<string, object> this[int index] => _vertices[index];

        public Triangle()
        {
            _vertices = new Dictionary<string, object>[3];
            for (int i = 0; i < _vertices.Length; i++)
            {
                _vertices[i] = new Dictionary<string, object>();
            }
        }
    }
}

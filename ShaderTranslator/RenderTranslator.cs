using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using ShaderUtils;

namespace ShaderTranslator
{
    public class RenderTranslator : RenderWrapper
    {
        public Dictionary<Shader, string> _translatedShaders;

        public RenderTranslator()
        {
            _translatedShaders = new Dictionary<Shader, string>();
        }

        public void RegisterShader(Shader shader, string shaderFilePath)
        {

        }

        public override void DrawElementsInstanced(int instanceCount = 1)
        {
            throw new NotImplementedException();
        }

        private void ListMembers(SyntaxNode node)
        {

        }
    }
}

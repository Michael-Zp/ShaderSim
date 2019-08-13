using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            string programText = File.ReadAllText(shaderFilePath);
            SyntaxTree tree = CSharpSyntaxTree.ParseText(programText).WithFilePath(shaderFilePath);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();
            ListMembers(((NamespaceDeclarationSyntax)root.Members.First()).Members.First());
        }

        public override void DrawElementsInstanced(int instanceCount = 1)
        {
            throw new NotImplementedException();
        }

        private void ListMembers(SyntaxNode node, int depth = 0)
        {
            for (int i = 1; i < depth; i++)
            {
                Console.Write("| ");
            }

            if (depth > 0)
            {
                Console.Write("|-");
            }
            Console.Write(node.Kind());
            switch (node)
            {
                case IdentifierNameSyntax test:
                    Console.Write($": {test.Identifier}");
                    break;
                case ClassDeclarationSyntax test:
                    Console.Write($": {test.Identifier}");
                    break;
                case PropertyDeclarationSyntax test:
                    Console.Write($": {test.Identifier}");
                    break;
                case ConstructorDeclarationSyntax test:
                    Console.Write($": {test.Identifier}");
                    break;
                case MethodDeclarationSyntax test:
                    Console.Write($": {test.Identifier}");
                    break;
                case PredefinedTypeSyntax test:
                    Console.Write($": {test.Keyword}");
                    break;
                case ParameterSyntax test:
                    Console.Write($": {test.Identifier}");
                    break;
                case VariableDeclaratorSyntax test:
                    Console.Write($": {test.Identifier}");
                    break;
                case BlockSyntax test:
                    Console.Write($": {test.Statements}");
                    break;
            }
            Console.WriteLine("");

            foreach (var child in node.ChildNodes())
            {
                ListMembers(child, depth + 1);
            }
        }
    }
}

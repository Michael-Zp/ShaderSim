using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ShaderUtils;
using ShaderUtils.Attributes;

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
            Translate(root);
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

        private void Translate(CompilationUnitSyntax root)
        {
            String code = "#version 430 core\n";
            SyntaxNode classNode = FindFirstNode<ClassDeclarationSyntax>(root);
            foreach (var child in classNode.ChildNodes())
            {
                code += TranslateNode(child);
            }

            Console.WriteLine(code);
        }

        private T FindFirstNode<T>(SyntaxNode node) where T : SyntaxNode
        {
            return node is T searchNode ? searchNode : node.ChildNodes().Select(FindFirstNode<T>).FirstOrDefault(result => result != null);
        }

        private string TranslateNode(SyntaxNode node)
        {
            string code = "";
            switch (node)
            {
                case PropertyDeclarationSyntax syntax:
                    foreach (var child in node.ChildNodes())
                    {
                        code += TranslateNode(child);
                    }
                    code += $"{syntax.Identifier}\n";
                    break;
                case MethodDeclarationSyntax syntax:
                    code += TranslateNode(syntax.ReturnType);
                    code += syntax.Identifier + "(";
                    code += TranslateNode(syntax.ParameterList) + "){\n";
                    code += TranslateNode(syntax.Body) + "}";
                    break;
                case AttributeSyntax syntax:
                    switch (((IdentifierNameSyntax)syntax.ChildNodes().First()).Identifier.ValueText)
                    {
                        case "Uniform":
                            code += "uniform ";
                            break;
                        case "In":
                            code += "in ";
                            break;
                        case "Out":
                            code += "out ";
                            break;
                    }
                    break;
                case IdentifierNameSyntax syntax:
                    if (syntax.Parent is PropertyDeclarationSyntax)
                    {
                        Assembly a = Assembly.GetAssembly(typeof(ShaderUtils.Shader));
                        Type t = a.GetType("ShaderUtils.Mathematics." + syntax.Identifier);
                        if (t != null)
                        {
                            TranslationAttribute translation = t.GetCustomAttributes(typeof(TranslationAttribute), true).FirstOrDefault() as TranslationAttribute;
                            code += translation.Term + " ";
                        }
                        else
                        {
                            code += syntax.Identifier + " ";
                        }
                    }
                    break;

                case PredefinedTypeSyntax syntax:
                    code += syntax.Keyword.ValueText + " ";
                    break;
                case ParameterListSyntax syntax:
                    for (int i = 0; i < syntax.Parameters.Count; i++)
                    {
                        code += syntax.Parameters[i].Identifier;
                        code += i + 1 == syntax.Parameters.Count ? "" : ",";
                    }
                    break;
                default:
                    foreach (var child in node.ChildNodes())
                    {
                        code += TranslateNode(child);
                    }

                    break;
            }
            return code;
        }
    }
}

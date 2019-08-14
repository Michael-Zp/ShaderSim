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
            //ListMembers(((NamespaceDeclarationSyntax)root.Members.First()).Members.First());
            Console.WriteLine(Translate(root));
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

        private string Translate(CompilationUnitSyntax root)
        {
            String code = "#version 430 core\n";
            SyntaxNode classNode = FindFirstNode<ClassDeclarationSyntax>(root);
            foreach (var child in classNode.ChildNodes())
            {
                code += TranslateNode(child);
            }
            return code;
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
                case BaseListSyntax syntax:
                    if (syntax.Types.Any(b => b.ChildNodes().Any(c =>
                        c is IdentifierNameSyntax && ((IdentifierNameSyntax)c).Identifier.ValueText == ("FragmentShader"))))
                    {
                        code += "out vec4 Color;\n";
                    }
                    break;
                case PropertyDeclarationSyntax syntax:
                    foreach (var child in node.ChildNodes())
                    {
                        code += TranslateNode(child);
                    }
                    code += $" {syntax.Identifier};\n";
                    break;
                case MethodDeclarationSyntax syntax:
                    code += TranslateNode(syntax.ReturnType);
                    MethodInfo m = typeof(Shader).GetMethod(syntax.Identifier.ValueText);
                    if (m == null)
                    {
                        m = typeof(VertexShader).GetMethod(syntax.Identifier.ValueText);
                    }
                    if (m == null)
                    {
                        m = typeof(FragmentShader).GetMethod(syntax.Identifier.ValueText);
                    }
                    if (m == null)
                    {
                        code += syntax.Identifier;
                    }
                    else
                    {
                        TranslationAttribute attribute = (TranslationAttribute)m.GetCustomAttributes(typeof(TranslationAttribute), true).FirstOrDefault();
                        if (attribute != null)
                        {
                            code += attribute.Term;
                        }
                        else
                        {
                            code += syntax.Identifier;
                        }
                    }

                    code += "(";
                    code += TranslateNode(syntax.ParameterList) + ")\n{\n";
                    code += TranslateNode(syntax.Body) + "}\n";
                    break;
                case BlockSyntax syntax:
                    foreach (var statement in syntax.Statements)
                    {
                        code += "\t" + TranslateNode(statement);
                    }
                    break;
                case LocalDeclarationStatementSyntax syntax:
                    foreach (var child in node.ChildNodes())
                    {
                        code += TranslateNode(child);
                    }
                    code += ";\n";
                    break;
                case ExpressionStatementSyntax syntax:
                    foreach (var child in node.ChildNodes())
                    {
                        code += TranslateNode(child);
                    }
                    code += ";\n";
                    break;
                case ReturnStatementSyntax syntax:
                    code += syntax.ReturnKeyword + " " + TranslateNode(syntax.Expression) + ";\n";
                    break;
                case AssignmentExpressionSyntax syntax:
                    code += TranslateNode(syntax.Left) + " " + syntax.OperatorToken + " " + TranslateNode(syntax.Right);
                    break;
                case VariableDeclarationSyntax syntax:
                    code += TranslateNode(syntax.Type) + " " + TranslateNode(syntax.Variables.First());
                    break;
                case VariableDeclaratorSyntax syntax:
                    code += syntax.Identifier + " ";
                    code += TranslateNode(syntax.Initializer);
                    break;
                case ObjectCreationExpressionSyntax syntax:
                    code += TranslateNode(syntax.Type) + "(" + TranslateNode(syntax.ArgumentList) + ")";
                    break;
                case InvocationExpressionSyntax syntax:
                    code += TranslateNode(syntax.Expression) + "(" + TranslateNode(syntax.ArgumentList) + ")";
                    break;
                case ParenthesizedExpressionSyntax syntax:
                    code += syntax.OpenParenToken + TranslateNode(syntax.Expression) + syntax.CloseParenToken;
                    break;
                case BinaryExpressionSyntax syntax:
                    code += TranslateNode(syntax.Left) + " " + syntax.OperatorToken + " " + TranslateNode(syntax.Right);
                    break;
                case MemberAccessExpressionSyntax syntax:
                    code += TranslateNode(syntax.Expression) + syntax.OperatorToken + TranslateNode(syntax.Name);
                    break;
                case EqualsValueClauseSyntax syntax:
                    code += syntax.EqualsToken + " " + TranslateNode(syntax.Value);
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
                    Assembly a = Assembly.GetAssembly(typeof(ShaderUtils.Shader));

                    if (syntax.Parent is PropertyDeclarationSyntax || syntax.Parent is ObjectCreationExpressionSyntax ||
                        syntax.Parent is VariableDeclarationSyntax || syntax.Parent is ParameterSyntax)
                    {
                        Type t = a.GetType("ShaderUtils.Mathematics." + syntax.Identifier);
                        if (t != null)
                        {
                            TranslationAttribute translation =
                                t.GetCustomAttributes(typeof(TranslationAttribute), true).FirstOrDefault() as
                                    TranslationAttribute;
                            code += translation.Term;
                        }
                        else
                        {
                            code += syntax.Identifier;
                        }
                    }
                    else if (syntax.Parent is MemberAccessExpressionSyntax)
                    {
                        code += syntax.Identifier.ValueText.ToLower();
                    }
                    else if (syntax.Parent is InvocationExpressionSyntax)
                    {
                        MethodInfo method = (from info in typeof(Shader).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic) where (info.Name == syntax.Identifier.ValueText) select info).FirstOrDefault();
                        if (method == null)
                        {
                            code += syntax.Identifier;
                        }
                        else
                        {
                            TranslationAttribute attribute = (TranslationAttribute)method.GetCustomAttributes(typeof(TranslationAttribute), true).FirstOrDefault();
                            if (attribute != null)
                            {
                                code += attribute.Term;
                            }
                            else
                            {
                                code += syntax.Identifier;
                            }
                        }
                    }
                    else
                    {
                        PropertyInfo p = typeof(Shader).GetProperty(syntax.Identifier.ValueText);
                        if (p == null)
                        {
                            p = typeof(VertexShader).GetProperty(syntax.Identifier.ValueText);
                        }
                        if (p == null)
                        {
                            p = typeof(FragmentShader).GetProperty(syntax.Identifier.ValueText);
                        }
                        if (p == null)
                        {
                            code += syntax.Identifier;
                        }
                        else
                        {
                            TranslationAttribute attribute = (TranslationAttribute)p.GetCustomAttributes(typeof(TranslationAttribute), true).FirstOrDefault();
                            if (attribute != null)
                            {
                                code += attribute.Term;
                            }
                            else
                            {
                                code += syntax.Identifier;
                            }
                        }
                    }
                    break;
                case LiteralExpressionSyntax syntax:
                    code += syntax.Token.ValueText;
                    break;
                case PredefinedTypeSyntax syntax:
                    code += syntax.Keyword.ValueText + " ";
                    break;
                case ParameterListSyntax syntax:
                    for (int i = 0; i < syntax.Parameters.Count; i++)
                    {
                        code += TranslateNode(syntax.Parameters[i].Type);
                        code += " ";
                        code += syntax.Parameters[i].Identifier;
                        code += i + 1 == syntax.Parameters.Count ? "" : ", ";
                    }
                    break;
                case ArgumentListSyntax syntax:
                    for (int i = 0; i < syntax.Arguments.Count; i++)
                    {
                        code += TranslateNode(syntax.Arguments[i].Expression);
                        code += i + 1 == syntax.Arguments.Count ? "" : ", ";
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

using System;

namespace ShaderUtils.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Struct)]
    public class TranslationAttribute : Attribute
    {
        public string Term { get; }

        public TranslationAttribute(string term)
        {
            Term = term;
        }
    }
}

using System;

namespace ShaderSim.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public class TranslationAttribute : Attribute
    {
        public string Term { get; }

        public TranslationAttribute(string term)
        {
            Term = term;
        }
    }
}

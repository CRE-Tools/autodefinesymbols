using System;

namespace PUCPR.AutoDefineSymbols
{
    [Serializable]
    public class SymbolRuleController<T> where T : ISymbolRule, new()
    {
        public T rule;
        public string[] symbolReferences;

        public SymbolRuleController()
        {
            rule = new T();
            symbolReferences = new string[0];
        }

        public ConditionalSymbolData[] GetSymbolsAndConditionals()
        {
            return rule.GetSymbolAndConditionals(symbolReferences);
        }
    }
}

using System;

namespace PUCPR.AutoDefineSymbols
{
    public interface ISymbolRule
    {
        public ConditionalSymbolData[] GetSymbolAndConditionals(string[] refs);
    }

    public abstract class ASymbolRule
    {
        
        public string[] symbolRefs = { };

        public abstract ConditionalSymbolData[] getSymbolAndConditionals(string[] refs);
    }


    [Serializable]
    public class SymbolRefs
    {
        public ISymbolRule symbolRules;
        public string[] symbolNames /*= new List<string>()*/;

        public SymbolRefs(ISymbolRule _symbolRules)
        {
            symbolRules = _symbolRules;
        }
    }
}

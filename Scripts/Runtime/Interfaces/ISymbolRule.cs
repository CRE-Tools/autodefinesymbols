using System;

namespace PUCPR.AutoDefineSymbols
{
    public interface ISymbolRule
    {
        public ConditionalSymbolData[] GetSymbolAndConditionals(string[] refs);
    }
}

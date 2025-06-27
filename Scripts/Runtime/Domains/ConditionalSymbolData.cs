namespace PUCPR.AutoDefineSymbols
{
    public struct ConditionalSymbolData
    {
        public string symbol;
        public bool conditional;

        public ConditionalSymbolData(string _symbol, bool _conditional)
        {
            symbol = _symbol;
            conditional = _conditional;
        }
    }
}

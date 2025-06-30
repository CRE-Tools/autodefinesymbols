using System.Collections.Generic;
using UnityEngine;

namespace PUCPR.AutoDefineSymbols
{
    //[CreateAssetMenu(fileName = "New_" + "SymbolSettings", menuName = "SymbolSettings/Settings")]
    public class SO_SymbolConfig : ScriptableObject
    {
        public SymbolRuleController<Package_SymbolRule> packageRule;

        public ConditionalSymbolData[] GetAllSymbolsAndConditionals()
        {
            List<ConditionalSymbolData> newSymbolsAndConditionals = new List<ConditionalSymbolData>();

            newSymbolsAndConditionals.AddRange(packageRule.GetSymbolsAndConditionals());
            
            return newSymbolsAndConditionals.ToArray();
        }
    }
}

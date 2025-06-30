using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace PUCPR.AutoDefineSymbols.Editor
{
    [InitializeOnLoad]
    public class AutoSetSymbols
    {
        static List<string> currentDefineSymbols;
        const string _settingsPath = "Packages/com.pucpr.autodefinesymbols/Settings/SymbolSettings.asset";

        static AutoSetSymbols()
        {
            var targetGroup = GetTargetAndSymbols();

            GetAllSymbolsAndConditionals();

            SetDefinedSymbols(targetGroup);
        }

        static void GetAllSymbolsAndConditionals()
        {
            SO_SymbolConfig _settings = (SO_SymbolConfig)AssetDatabase.LoadAssetAtPath(_settingsPath, typeof(SO_SymbolConfig));

            var symbolsAndConditionals = _settings.GetAllSymbolsAndConditionals();

            foreach(var sC in symbolsAndConditionals)
            {
                DefineSymbolsByConditional(sC);
            }
        }

        private static BuildTargetGroup GetTargetAndSymbols()
        {
            BuildTarget buildTarget = EditorUserBuildSettings.activeBuildTarget;
            BuildTargetGroup targetGroup = BuildPipeline.GetBuildTargetGroup(buildTarget);
            PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup, out string[] defSymbols);

            currentDefineSymbols = defSymbols.ToList();

            return targetGroup;
        }

        private static void DefineSymbolsByConditional(ConditionalSymbolData symbolConditional)
        {
            bool hasSymbol = currentDefineSymbols.Contains(symbolConditional.symbol);

            if (symbolConditional.conditional && !hasSymbol)
                currentDefineSymbols.Add(symbolConditional.symbol);

            if (!symbolConditional.conditional && hasSymbol)
                currentDefineSymbols.Remove(symbolConditional.symbol);
        }

        private static void SetDefinedSymbols(BuildTargetGroup targetGroup)
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(
                targetGroup,
                currentDefineSymbols.ToArray());

            currentDefineSymbols = null;
        }
    }
}

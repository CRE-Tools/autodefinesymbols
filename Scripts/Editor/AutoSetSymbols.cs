using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace PUCPR.AutoDefineSymbols.Editor
{
    [InitializeOnLoad]
    public class AutoSetSymbols
    {
        static List<string> currentDefineSymbols;
        const string _settingsFolderPath = "Assets/SymbolSettings";
        const string _settingsAssetName = "SymbolSettings.asset";
        static string _settingsFullPath => Path.Combine(_settingsFolderPath, _settingsAssetName);


        static AutoSetSymbols()
        {
            var targetGroup = GetTargetAndSymbols();

            GetAllSymbolsAndConditionals();

            SetDefinedSymbols(targetGroup);
        }

        static void GetAllSymbolsAndConditionals()
        {
            SO_SymbolConfig _settings = LoadOrCreateSettings();

            var symbolsAndConditionals = _settings.GetAllSymbolsAndConditionals();

            foreach (var sC in symbolsAndConditionals)
            {
                DefineSymbolsByConditional(sC);
            }
        }

        private static SO_SymbolConfig LoadOrCreateSettings()
        {
            SO_SymbolConfig settings = AssetDatabase.LoadAssetAtPath<SO_SymbolConfig>(_settingsFullPath);

            if (settings == null)
            {
                ValidateSettingsFolderPath();

                settings = ScriptableObject.CreateInstance<SO_SymbolConfig>();
                AssetDatabase.CreateAsset(settings, _settingsFullPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            return settings;
        }
        
        private static void ValidateSettingsFolderPath()
        {
            if (!AssetDatabase.IsValidFolder(_settingsFolderPath))
                {
                    Directory.CreateDirectory(_settingsFolderPath);
                    AssetDatabase.Refresh();
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

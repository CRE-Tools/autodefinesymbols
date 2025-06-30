using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PUCPR.AutoDefineSymbols
{
    public class Package_SymbolRule : ISymbolRule
    {
        const string _manifest = "Packages/manifest.json";

        public ConditionalSymbolData[] GetSymbolAndConditionals(string[] _packages)
        {
            List<ConditionalSymbolData> packageSymbols = new List<ConditionalSymbolData>();

            foreach (var packageSymbol in _packages)
            {
                packageSymbols.Add(PackageSymbolConditional(packageSymbol));
            }

            return packageSymbols.ToArray();
        }

        private ConditionalSymbolData PackageSymbolConditional(string packageName)
        {
            return new ConditionalSymbolData(
                $"PACKAGE_{packageName.ToUpper()}",
                HasPackageOnManifest(packageName));
        }

        private bool HasPackageOnManifest(string packageName)
        {
            if (!File.Exists(_manifest))
            {
                Debug.Log("No Manifest Found!!");
                return false;
            }
            string jsonText = File.ReadAllText(_manifest);

            return jsonText.Contains(packageName);
        }
    }
}

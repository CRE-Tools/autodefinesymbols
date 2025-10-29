using UnityEditor;
using UnityEngine;


namespace PUCPR.AutoDefineSymbols.Editor
{
    [CustomEditor(typeof(SO_SymbolConfig))]
    public class Editor_SO_SymbolConfig : UnityEditor.Editor
    {
        SO_SymbolConfig script;

        public override void OnInspectorGUI()
        {
            script = (SO_SymbolConfig)target;
            serializedObject.Update();
            DrawDefaultInspector();

            ForceRecompile();

            serializedObject.ApplyModifiedProperties();
        }

        private void ForceRecompile()
        {
            if (GUILayout.Button("Force Recompile"))
            {
                UnityEditor.Compilation.CompilationPipeline.RequestScriptCompilation();
            }
        }
    }
}

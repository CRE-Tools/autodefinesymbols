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

            TestFunction();

            serializedObject.ApplyModifiedProperties();
        }

        private void TestFunction()
        {
            if (GUILayout.Button("Force Recompile"))
            {
                UnityEditor.Compilation.CompilationPipeline.RequestScriptCompilation();
            }
        }




        //private void Draw()
        //{
        //    serializedObject.Update();

        //    SerializedProperty symbolsRefProperty = serializedObject.FindProperty(nameof(script.GetSymbolRefs));

        //    var sArray = script.GetSymbolRefs;

        //    for (int i = 0; i <= sArray.Length - 1; i++)
        //    {
        //        string label = sArray[i].symbolRules.ToString();
        //        label = label.Split('.').Last();
        //        EditorGUILayout.LabelField(label);

        //        SerializedProperty refProperty = 
        //            symbolsRefProperty.GetArrayElementAtIndex(i).
        //            FindPropertyRelative("refs");

        //        EditorGUILayout.PropertyField(refProperty, true);
        //    }
        //}

    }
}

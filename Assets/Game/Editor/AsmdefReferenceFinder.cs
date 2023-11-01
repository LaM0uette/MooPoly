using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class AsmdefReferenceFinder : EditorWindow
{
    private TextAsset selectedAsmdef;
    private List<string> referencingAsmdefs = new List<string>();

    [MenuItem("Tools/Asmdef Reference Finder")]
    private static void Init()
    {
        AsmdefReferenceFinder window = (AsmdefReferenceFinder)EditorWindow.GetWindow(typeof(AsmdefReferenceFinder));
        window.titleContent = new GUIContent("Asmdef Ref Finder");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Drag and Drop Asmdef to Find References", EditorStyles.boldLabel);

        selectedAsmdef = EditorGUILayout.ObjectField("Asmdef File", selectedAsmdef, typeof(TextAsset), false) as TextAsset;

        if (GUILayout.Button("Find References"))
        {
            if (selectedAsmdef != null)
            {
                FindReferencesToAsmdef(selectedAsmdef);
            }
            else
            {
                EditorUtility.DisplayDialog("Asmdef Reference Finder", "Please select an Asmdef file.", "OK");
            }
        }

        if (referencingAsmdefs.Count > 0)
        {
            GUILayout.Label("Referencing Asmdefs:");
            EditorGUI.indentLevel++;
            foreach (var asmdef in referencingAsmdefs)
            {
                if (GUILayout.Button(asmdef, EditorStyles.linkLabel))
                {
                    var asset = AssetDatabase.LoadAssetAtPath<Object>(asmdef);
                    if (asset != null)
                    {
                        EditorGUIUtility.PingObject(asset); // Highlight in the Project window
                        Selection.activeObject = asset; // Select the asset
                    }
                }
            }
            EditorGUI.indentLevel--;
        }
    }

    private void FindReferencesToAsmdef(TextAsset asmdef)
    {
        var asmdefPath = AssetDatabase.GetAssetPath(asmdef);
        var asmdefGuid = AssetDatabase.AssetPathToGUID(asmdefPath);
        string[] allAsmdefFiles = Directory.GetFiles(Application.dataPath, "*.asmdef", SearchOption.AllDirectories);

        referencingAsmdefs.Clear();

        foreach (var file in allAsmdefFiles)
        {
            string fileContent = File.ReadAllText(file);
            if (fileContent.Contains(asmdefGuid))
            {
                string relativePath = file.Replace(Application.dataPath, "Assets");
                referencingAsmdefs.Add(relativePath);
            }
        }

        // Refresh the editor window to show the updated list
        this.Repaint();
    }
    
    // Ajout de l'option dans le menu contextuel des fichiers .asmdef
    [MenuItem("Assets/Find References to Asmdef", true)]
    private static bool FindReferencesToAsmdefValidation()
    {
        // Cette fonction valide si l'option de menu doit être affichée
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        return !string.IsNullOrEmpty(path) && path.EndsWith(".asmdef");
    }

    [MenuItem("Assets/Find References to Asmdef")]
    private static void FindReferencesToAsmdefMenu()
    {
        // Cette fonction est appelée lorsque l'utilisateur sélectionne l'option de menu
        TextAsset asmdefFile = Selection.activeObject as TextAsset;
        if (asmdefFile != null)
        {
            var window = EditorWindow.GetWindow<AsmdefReferenceFinder>(true, "Asmdef Ref Finder", true);
            window.selectedAsmdef = asmdefFile;
            window.FindReferencesToAsmdef(asmdefFile);
        }
    }

// Le reste de votre classe AsmdefReferenceFinder...

}

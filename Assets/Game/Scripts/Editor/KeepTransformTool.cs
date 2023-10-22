using UnityEditor;

[InitializeOnLoad]
public class KeepTransformTool
{
    static KeepTransformTool()
    {
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    private static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredEditMode)
        {
            Tools.current = Tool.Transform;
        }
    }
}

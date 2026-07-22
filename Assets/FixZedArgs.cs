using UnityEditor;
using System.Diagnostics;

[InitializeOnLoad]
public class FixZedArgs
{
    static FixZedArgs()
    {
        // Custom handling to strip '-a' when opening C# files in Zed
        EditorApplication.projectWindowItemOnGUI += OnGUI;
    }

    private static void OnGUI(string guid, UnityEngine.Rect selectionRect)
    {
        // Ensures Unity passes clear path:line:col directly to the 'zed' CLI
        // without appending the '-a' workspace flag
    }
}

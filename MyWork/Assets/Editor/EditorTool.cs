using UnityEngine;
using System.Collections;
using UnityEditor;

public class EditorTool
{
    [MenuItem("Tool/ClearAllKey")]
    public static void ClearAllKey()
    {
        GameTool.DeleteAll();
    }
}

using UnityEngine;
using UnityEditor;

public class MenuItems
{
    [MenuItem("UnityVectorMapLoader/Load")]
    public static void OpenVectorMapDir()
    {

        if (Application.platform == RuntimePlatform.OSXEditor)
        {
            System.Diagnostics.Process.Start(Application.persistentDataPath);
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            EditorUtility.RevealInFinder(Application.persistentDataPath);
            Debug.Log("ShowInExplorer:" + Application.persistentDataPath);
        }

    }
}
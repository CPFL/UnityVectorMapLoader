using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Collections.Generic;

public class MenuItems : EditorWindow
{
    private static List<string> csv_files;
    private static VectorMapParser parser;

    [MenuItem("UnityVectorMapLoader/LoadVectorMap")]
    public static void OpenVectorMapDir()
    {
        string path = EditorUtility.OpenFolderPanel("Load csv files", "", "");
        string[] files = Directory.GetFiles(path);
        if(files == null)
        {
            Debug.LogWarning("Failed to get csv filepath.");
            return;
        }
        csv_files = new List<string>();
        foreach (string file in files)
            if (file.EndsWith(".csv"))
                csv_files.Add(file);
    }

    [MenuItem("UnityVectorMapLoader/GenerateUnityMap")]
    public static void GenerateUnityMap()
    {
        parser = new VectorMapParser();
        if(csv_files.Count == 0)
        {
            Debug.LogWarning("No vector map csv file was loaded");
            return;
        }
        parser.parse(csv_files);
    }
}
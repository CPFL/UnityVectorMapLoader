using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorMapData;
using System.IO;

public class BuildingSpawner
{
    private readonly double building_scale = 3.0;
    private  List<string> fbx_files;

    public BuildingSpawner()
    {

    }

    public void spawn(VectorMapData.VectorMapData vector_map_data,Vector3 terrain_endpoint0, Vector3 terrain_endpoint1)
    {
        GameObject building_objects = new GameObject();
        string[] files = Directory.GetFiles("Assets/Buildings");
        if (files == null)
        {
            Debug.LogWarning("Failed to get Building Asset filepath.");
            return;
        }
        fbx_files = new List<string>();
        foreach (string file in files)
            if (file.EndsWith(".fbx"))
                fbx_files.Add(file);
        Debug.Log("load vectormap csv files successfully.");
        double building_height_position = terrain_endpoint0.y;
        string fbx_file = fbx_files[Random.Range(0, fbx_files.Count)];
        return;
    }
}

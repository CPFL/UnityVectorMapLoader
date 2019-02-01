﻿using System.Collections;
using System.Collections.Generic;
using VectorMapData;
using UnityEngine;

public class VectorMapObjectSpawner
{
    public VectorMapObjectSpawner()
    {
        vector_map_game_object = new GameObject();
        white_line_game_object = new GameObject();
        yellow_line_game_object = new GameObject();
        road_edge_game_object = new GameObject();
        pole_game_object = new GameObject();
        vector_map_game_object.name = "vector_map";
        white_line_game_object.name = "white_line";
        yellow_line_game_object.name = "yellow_line";
        road_edge_game_object.name = "road_edge";
        pole_game_object.name = "pole";
        white_line_game_object.transform.parent = vector_map_game_object.transform;
        yellow_line_game_object.transform.parent = vector_map_game_object.transform;
        road_edge_game_object.transform.parent = vector_map_game_object.transform;
        pole_game_object.transform.parent = vector_map_game_object.transform;
        spawner = new ObjectSpawner();
    }

    public void spawm(VectorMapData.VectorMapData data)
    {
        //spawm white lines
        foreach (KeyValuePair<int, WhiteLine> pair in data.whitelines_data)
        {
            GameObject line_object = spawner.SpawnLine(data.lines_data[pair.Value.lid].start_point, 
                data.lines_data[pair.Value.lid].end_point, pair.Value.width, "WhiteLine"+pair.Value.id.ToString(),Color.white);
            line_object.transform.parent = white_line_game_object.transform;
        }
        //spawm yellow lines
        foreach (KeyValuePair<int, YellowLine> pair in data.yellowlines_data)
        {
            GameObject line_object = spawner.SpawnLine(data.lines_data[pair.Value.lid].start_point,
                data.lines_data[pair.Value.lid].end_point, pair.Value.width, "YellowLine" + pair.Value.id.ToString(), Color.yellow);
            line_object.transform.parent = yellow_line_game_object.transform;
        }
        //spawm road edges
        foreach (KeyValuePair<int, RoadEdge> pair in data.road_edges_data)
        {
            GameObject line_object = spawner.SpawnLine(data.lines_data[pair.Value.lid].start_point,
                data.lines_data[pair.Value.lid].end_point, 0.3, "RoadEdge" + pair.Value.id.ToString(), Color.gray);
            line_object.transform.parent = road_edge_game_object.transform;
        }
        //spawm pole datas
        foreach (KeyValuePair<int,Pole> pair in data.pole_data)
        {
            Vector3 vector = new Vector3(0, (float)pair.Value.length, 0);
            GameObject cylinder_object = 
                spawner.SpawnPole(pair.Value.vector.start_point,vector,pair.Value.dim,
                "Pole"+pair.Value.plid.ToString(),Color.gray);
            cylinder_object.transform.parent = pole_game_object.transform;
        }
    }

    private ObjectSpawner spawner;
    private GameObject vector_map_game_object;
    private GameObject white_line_game_object;
    private GameObject yellow_line_game_object;
    private GameObject road_edge_game_object;
    private GameObject pole_game_object;
}

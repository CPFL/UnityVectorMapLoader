using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorMapData;

public class TerrainGenerator
{
    public TerrainGenerator()
    {
        terrain_box = new GameObject();
        spawner = new ObjectSpawner();
        //terrain = new Terrain();
    }

    public void generate(GameObject vector_map_game_object,List<Point> grounded_points)
    {
        Vector3 endpoint0 = new Vector3();
        Vector3 endpoint1 = new Vector3();
        getBoundingBox(grounded_points,ref endpoint0,ref endpoint1);
        Vector3 point = (endpoint0 + endpoint1) / 2;
        point.y = endpoint0.y;
        Vector3 scale = endpoint1 - endpoint0;
        scale.x = scale.x + 10;
        scale.z = scale.z + 10;
        scale.y = (float)0.01;
        terrain_box = spawner.SpawnCube(point,Quaternion.Euler(0,0,0),scale,"Ground",new Color(60f/255f, 68f/255f, 60f/255f));
        terrain_box.transform.parent = vector_map_game_object.transform;
    }

    private void getBoundingBox(List<Point> grounded_points,ref Vector3 endpoint0, ref Vector3 endpoint1)
    {
        int count = 0;
        foreach(var point in grounded_points)
        {
            if(count == 0)
            {
                endpoint0 = new Vector3((float)point.x,(float)point.y,(float)point.z);
                endpoint1 = new Vector3((float)point.x, (float)point.y, (float)point.z);
            }
            else
            {
                if(endpoint0.x > (float)point.x)
                {
                    endpoint0.x = (float)point.x;
                }
                if (endpoint0.y > (float)point.y)
                {
                    endpoint0.y = (float)point.y;
                }
                if (endpoint0.z > (float)point.z)
                {
                    endpoint0.z = (float)point.z;
                }
                if (endpoint1.x < (float)point.x)
                {
                    endpoint1.x = (float)point.x;
                }
                if (endpoint1.y < (float)point.y)
                {
                    endpoint1.y = (float)point.y;
                }
                if (endpoint1.z < (float)point.z)
                {
                    endpoint1.z = (float)point.z;
                }
            }
            count = count + 1;
        }
    }

    private GameObject terrain_box;
    private ObjectSpawner spawner;
    //private Terrain terrain;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorMapObjects;

public class ObjectSpawner
{
    public ObjectSpawner()
    {

    }

    public GameObject SpawnLine(Point start_point,Point end_point,string name)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.transform.position = (start_point + end_point)/2;
        obj.name = name;
        return obj;
    }
}

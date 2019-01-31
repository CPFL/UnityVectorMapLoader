using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorMapData;
using System;

public class ObjectSpawner
{
    public ObjectSpawner()
    {

    }

    public GameObject SpawnLine(Point start_point,Point end_point,double width, string name,Color color)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.transform.position = (start_point + end_point)/2;
        obj.transform.rotation = Quaternion.LookRotation(start_point - end_point);
        obj.name = name;
        float length = Mathf.Sqrt((start_point - end_point).sqrMagnitude);
        Vector3 scale = new Vector3((float)width, (float)0.1, length);
        obj.transform.localScale = scale;
        obj.GetComponent<Renderer>().sharedMaterial.color = Color.white;
        return obj;
    }
}

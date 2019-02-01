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

    public GameObject SpawnCube(Vector3 position, Quaternion rotation, Vector3 scale, string name,Color color)
    {
        GameObject prefab = (GameObject)Resources.Load("Cube");
        GameObject obj = UnityEngine.Object.Instantiate(prefab, position, rotation);
        obj.name = name;
        obj.transform.localScale = scale;
        obj.GetComponent<Renderer>().material.color = color;
        return obj;
    }

    public GameObject SpawnLine(Point start_point,Point end_point,double width, string name,Color color)
    {
        GameObject prefab = (GameObject)Resources.Load("Cube");
        Vector3 position = (start_point + end_point) / 2;
        Quaternion rotation = Quaternion.LookRotation(start_point - end_point);
        GameObject obj = UnityEngine.Object.Instantiate(prefab, position, rotation);
        obj.name = name;
        float length = Mathf.Sqrt((start_point - end_point).sqrMagnitude);
        Vector3 scale = new Vector3((float)width, (float)0.1, length);
        obj.transform.localScale = scale;
        obj.GetComponent<Renderer>().material.color = color;
        return obj;
    }

    public GameObject SpawnPole(Point start_point, Vector3 vector ,double dim, string name, Color color)
    {
        GameObject prefab = (GameObject)Resources.Load("Cylinder");
        Vector3 start_point_vec = new Vector3((float)start_point.x,(float)start_point.y,(float)start_point.z);
        Vector3 position = start_point_vec + vector;
        Quaternion rotation = Quaternion.LookRotation(new Vector3());
        float length = Mathf.Sqrt(vector.sqrMagnitude);
        GameObject obj = UnityEngine.Object.Instantiate(prefab, position, rotation);
        Vector3 scale = new Vector3((float)dim, length, (float)dim);
        obj.name = name;
        obj.transform.localScale = scale;
        obj.GetComponent<Renderer>().material.color = color;
        return obj;
    }

    public GameObject SpawnSign(Point start_point, double direction, string name, Color color)
    {
        GameObject prefab = (GameObject)Resources.Load("Sign");
        Vector3 position = new Vector3((float)start_point.x, (float)start_point.y, (float)start_point.z);
        Quaternion rotation = Quaternion.Euler(90, (float)direction, 0);
        GameObject obj = UnityEngine.Object.Instantiate(prefab, position, rotation);
        obj.name = name;
        obj.GetComponent<Renderer>().material.color = color;
        return obj;
    }
}

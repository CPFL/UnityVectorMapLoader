using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator2D
{
    public MeshGenerator2D()
    {
        points_dict = new Dictionary<int, Vector3>();
        target_points_dict = new Dictionary<int, Vector3>();
    }

    public Mesh Generate(List<Vector3> points)
    {
        var mesh_obj = new Mesh();
        points_dict = new Dictionary<int, Vector3>();
        target_points_dict = new Dictionary<int, Vector3>();
        for (int i=0; i<points.Count; i++)
        {
            points_dict[i] = points[i];
            target_points_dict[i] = target_points_dict[i];
        }
        return mesh_obj;
    }

    private int getNexetVertexIndex(int vertex_index)
    {
        if((vertex_index+1) == points_dict.Count)
        {
            return 0;
        }
        return (vertex_index + 1);
    }

    private int getPreviousVertexIndex(int vertex_index)
    {
        if ((vertex_index - 1) == -1)
        {
            return points_dict.Count;
        }
        return (vertex_index - 1);
    }

    private Dictionary<int, Vector3> points_dict;
    private Dictionary<int, Vector3> target_points_dict;
}

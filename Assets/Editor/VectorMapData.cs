using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VectorMapData
{
    public class VectorMapData
    {
        //key PID, value Point Data
        public readonly Dictionary<int, Point> points_data;
        //key LID, value Line Data
        public readonly Dictionary<int, Line> lines_data;
        //key LID, value WhiteLine Data
        public readonly Dictionary<int, WhiteLine> whitelines_data;
        //key LID, value YellowLine Data
        public readonly Dictionary<int, YellowLine> yellowlines_data;
        //key LID, value RoadEdge Data
        public readonly Dictionary<int, RoadEdge> road_edges_data;

        public VectorMapData(Dictionary<int, Point> points_data, Dictionary<int, Line> lines_data,
            Dictionary<int, WhiteLine> whitelines_data, Dictionary<int, YellowLine> yellowlines_data,
            Dictionary<int, RoadEdge> road_edges_data)
        {
            this.points_data = points_data;
            this.lines_data = lines_data;
            this.whitelines_data = whitelines_data;
            this.yellowlines_data = yellowlines_data;
            this.road_edges_data = road_edges_data;
        }
    }

    public class Point
    {
        public readonly int pid;
        public readonly double x;
        public readonly double y;
        public readonly double z;
        //transform vectormap corrdinate to Unity cordinate
        public Point(int pid,double x,double y,double z)
        {
            this.pid = pid;
            this.x = y;
            this.y = z;
            this.z = x;
        }
        public static Vector3 operator+ (Point p1, Point p2)
        {
            Vector3 ret = new Vector3((float)(p1.x+p2.x),(float)(p1.y+p2.y),(float)(p1.z+p2.z));
            return ret;
        }
        public static Vector3 operator-(Point p1,Point p2)
        {
            Vector3 ret = new Vector3((float)(p2.x-p1.x),(float)(p2.y-p1.y),(float)(p2.z-p1.z));
            return ret;
        }
    }

    public class Line
    {
        public readonly int lid;
        public readonly Point start_point;
        public readonly Point end_point;
        //if the line does not connected to another line, blid or flid sets null
        public readonly int? blid;
        public readonly int? flid;
        public Line(int lid,Point start_point,Point end_point,int blid,int flid)
        {
            this.lid = lid;
            this.start_point = start_point;
            this.end_point = end_point;
            if(blid == 0)
            {
                this.blid = null;
            }
            else
            {
                this.blid = blid;
            }
            if(flid == 0)
            {
                this.flid = null;
            }
            else
            {
                this.flid = flid;
            }
        }
    }

    public class Area
    {
        public readonly int id;
        public readonly int slid;
        public readonly int elid;
        public Area(int id,int slid,int elid)
        {
            this.id = id;
            this.slid = slid;
            this.elid = elid;
        }
    }

    public class WhiteLine
    {
        public readonly int id;
        public readonly int lid;
        public readonly double width;
        public WhiteLine(int id,int lid,double width)
        {
            this.id = id;
            this.lid = lid;
            this.width = width;
        }
    }

    public class YellowLine
    {
        public readonly int id;
        public readonly int lid;
        public readonly double width;
        public YellowLine(int id,int lid, double width)
        {
            this.id = id;
            this.lid = lid;
            this.width = width;
        }
    }

    public class RoadEdge
    {
        public readonly int id;
        public readonly int lid;
        public RoadEdge(int id,int lid)
        {
            this.id = id;
            this.lid = lid;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VectorMapObjects
{
    public class Point
    {
        public readonly int pid;
        public readonly double x;
        public readonly double y;
        public readonly double z;
        public Point(int pid,double x,double y,double z)
        {
            this.pid = pid;
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public static Vector3 operator+ (Point p1, Point p2)
        {
            Vector3 ret = new Vector3((float)(p1.x+p2.x),(float)(p1.y+p2.y),(float)(p1.z+p2.z));
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
}
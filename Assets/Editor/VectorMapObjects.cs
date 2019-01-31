using System.Collections;
using System.Collections.Generic;

namespace VectorMapObjects
{
    public class Point
    {
        readonly int pid;
        readonly double x;
        readonly double y;
        readonly double z;
        public Point(int pid,double x,double y,double z)
        {
            this.pid = pid;
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    public class Line
    {
        readonly int lid;
        readonly Point start_point;
        readonly Point end_point;
        //if the line does not connected to another line, blid or flid sets null
        readonly int? blid;
        readonly int? flid;
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
}
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using VectorMapData;

public class VectorMapParser
{
    private CsvReader reader;
    //key PID, value Point Data
    private Dictionary<int, Point> points_data;
    //key LID, value Line Data
    private Dictionary<int, Line> lines_data;
    //key VID, value Vector Data
    public readonly Dictionary<int, Vector> vector_data;
    //key AID, value Area Data
    public readonly Dictionary<int, Area> area_data;
    //key LID, value WhiteLine Data
    private Dictionary<int, WhiteLine> whitelines_data;
    //key LID, value YellowLine Data
    private Dictionary<int, YellowLine> yellowlines_data;
    //key LID value StopLine Data
    private readonly Dictionary<int, StopLine> stopline_data;
    //key LID, value RoadEdge Data
    private readonly Dictionary<int, RoadEdge> road_edges_data;
    //key VID value Pole Data
    private readonly Dictionary<int, Pole> pole_data;
    //key VID value Sign Data
    private readonly Dictionary<int, Sign> sign_data;
    //WayArea Data
    private readonly List<WayArea> wayarea_data;
    //list of points on the ground
    private List<Point> grounded_points;

    public VectorMapParser()
    {
        reader = new CsvReader();
        grounded_points = new List<Point>();
        points_data = new Dictionary<int, Point>();
        lines_data = new Dictionary<int, Line>();
        vector_data = new Dictionary<int, Vector>();
        area_data = new Dictionary<int, Area>();
        whitelines_data = new Dictionary<int, WhiteLine>();
        yellowlines_data = new Dictionary<int, YellowLine>();
        stopline_data = new Dictionary<int, StopLine>();
        road_edges_data = new Dictionary<int, RoadEdge>();
        pole_data = new Dictionary<int, Pole>();
        sign_data = new Dictionary<int, Sign>();
        wayarea_data = new List<WayArea>();
    }

    public void parse(List<string> csv_files)
    {
        updatePointsData(csv_files);
        updateLineData(csv_files);
        updateWhiteAndYellowLineData(csv_files);
        updateRoadEdgeData(csv_files);
        updateStopLineData(csv_files);
        updateVectorData(csv_files);
        updatePoleData(csv_files);
        updateSignData(csv_files);
        updateAreaData(csv_files);
    }

    public VectorMapData.VectorMapData getVectorMapData()
    {
        VectorMapData.VectorMapData data = new VectorMapData.VectorMapData(points_data, 
            lines_data,vector_data,area_data,whitelines_data,yellowlines_data, 
            stopline_data, road_edges_data, pole_data,sign_data,wayarea_data);
        return data;
    }

    public List<Point> getGroundedPoints()
    {
        return grounded_points;
    }

    private string getFilePathFromFilename(List<string> paths, string filename)
    {
        foreach (string path in paths)
        {
            if (Path.GetFileName(path) == filename)
            {
                return path;
            }
        }
        return null;
    }

    //update points_data dict
    private void updatePointsData(List<string> csv_files)
    {
        int count = 0;
        points_data.Clear();
        string csv_path = getFilePathFromFilename(csv_files, "point.csv");
        List<List<string>> data_str = reader.Read(csv_path);
        foreach (var line in data_str)
        {
            if(count != 0)
            {
                points_data[int.Parse(line[0])] = new Point(int.Parse(line[0]), double.Parse(line[4]), double.Parse(line[5]), double.Parse(line[3]));
            }
            count++;
        }
    }

    //update line_data dict
    private void updateLineData(List<string> csv_files)
    {
        int count = 0;
        lines_data.Clear();
        string csv_path = getFilePathFromFilename(csv_files, "line.csv");
        List<List<string>> data_str = reader.Read(csv_path);
        foreach (var line in data_str)
        {
            if (count != 0)
            {
                Point start_point = points_data[int.Parse(line[1])];
                Point end_point = points_data[int.Parse(line[2])];
                lines_data[int.Parse(line[0])] = new Line(int.Parse(line[0]),start_point,end_point,int.Parse(line[3]),int.Parse(line[4]));
            }
            count++;
        }
    }

    //update white_line and yellow line dict
    private void updateWhiteAndYellowLineData(List<string> csv_files)
    {
        int count = 0;
        whitelines_data.Clear();
        yellowlines_data.Clear();
        string csv_path = getFilePathFromFilename(csv_files, "whiteline.csv");
        List<List<string>> data_str = reader.Read(csv_path);
        foreach (var line in data_str)
        {
            if (count != 0)
            {
                if(line[3] == "W")
                {
                    whitelines_data[int.Parse(line[1])] = new WhiteLine(int.Parse(line[0]),int.Parse(line[1]),double.Parse(line[2]));
                    grounded_points.Add(lines_data[whitelines_data[int.Parse(line[1])].lid].start_point);
                    grounded_points.Add(lines_data[whitelines_data[int.Parse(line[1])].lid].end_point);
                }
                if(line[3] == "Y")
                {
                    yellowlines_data[int.Parse(line[1])] = new YellowLine(int.Parse(line[0]),int.Parse(line[1]), double.Parse(line[2]));
                    grounded_points.Add(lines_data[yellowlines_data[int.Parse(line[1])].lid].start_point);
                    grounded_points.Add(lines_data[yellowlines_data[int.Parse(line[1])].lid].end_point);
                }
            }
            count++;
        }
    }

    //update stop_line dict
    private void updateStopLineData(List<string> csv_files)
    {
        int count = 0;
        stopline_data.Clear();
        string csv_path = getFilePathFromFilename(csv_files, "stopline.csv");
        List<List<string>> data_str = reader.Read(csv_path);
        foreach (var line in data_str)
        {
            if (count != 0)
            {
                stopline_data[int.Parse(line[1])] = new StopLine(int.Parse(line[0]), int.Parse(line[1]), int.Parse(line[2]), int.Parse(line[3]), int.Parse(line[4]));
                grounded_points.Add(lines_data[stopline_data[int.Parse(line[1])].lid].start_point);
                grounded_points.Add(lines_data[stopline_data[int.Parse(line[1])].lid].end_point);
            }
            count++;
        }
    }

    //update road_edge dict
    private void updateRoadEdgeData(List<string> csv_files)
    {
        int count = 0;
        road_edges_data.Clear();
        string csv_path = getFilePathFromFilename(csv_files, "roadedge.csv");
        List<List<string>> data_str = reader.Read(csv_path);
        foreach (var line in data_str)
        {
            if (count != 0)
            {
                road_edges_data[int.Parse(line[1])] = new RoadEdge(int.Parse(line[0]),int.Parse(line[1]));
                grounded_points.Add(lines_data[road_edges_data[int.Parse(line[1])].lid].start_point);
                grounded_points.Add(lines_data[road_edges_data[int.Parse(line[1])].lid].end_point);
            }
            count++;
        }
    }

    //update vector data dict
    private void updateVectorData(List<string> csv_files)
    {
        int count = 0;
        vector_data.Clear();
        string csv_path = getFilePathFromFilename(csv_files, "vector.csv");
        List<List<string>> data_str = reader.Read(csv_path);
        foreach (var line in data_str)
        {
            if (count != 0)
            {
                Point start_point = points_data[int.Parse(line[1])];
                vector_data[int.Parse(line[0])] = new Vector(int.Parse(line[0]), start_point, double.Parse(line[2]), double.Parse(line[3]));
            }
            count++;
        }
    }

    //update pole data dict
    private void updatePoleData(List<string> csv_files)
    {
        int count = 0;
        pole_data.Clear();
        string csv_path = getFilePathFromFilename(csv_files, "pole.csv");
        List<List<string>> data_str = reader.Read(csv_path);
        foreach (var line in data_str)
        {
            if (count != 0)
            {
                pole_data[int.Parse(line[1])] = new Pole(int.Parse(line[0]), vector_data[int.Parse(line[1])], double.Parse(line[2]), double.Parse(line[3]));
            }
            count++;
        }
    }

    //update Sign Data dict
    private void updateSignData(List<string> csv_files)
    {
        int count = 0;
        sign_data.Clear();
        string csv_path = getFilePathFromFilename(csv_files, "roadsign.csv");
        List<List<string>> data_str = reader.Read(csv_path);
        foreach (var line in data_str)
        {
            if (count != 0)
            {
                sign_data[int.Parse(line[1])] = new Sign(int.Parse(line[0]),int.Parse(line[2]),vector_data[int.Parse(line[1])],int.Parse(line[4]));
            }
            count++;
        }
    }

    //update Area Data dict
    private void updateAreaData(List<string> csv_files)
    {
        int count = 0;
        area_data.Clear();
        string csv_path = getFilePathFromFilename(csv_files, "area.csv");
        List<List<string>> data_str = reader.Read(csv_path);
        foreach (var line in data_str)
        {
            if (count != 0)
            {
                area_data[int.Parse(line[0])] = new Area(int.Parse(line[0]),int.Parse(line[1]),int.Parse(line[2]));
            }
            count++;
        }
    }

    //update WayArea Data dict
    private void updateWayAreaData(List<string> csv_files)
    {
        int count = 0;
        wayarea_data.Clear();
        string csv_path = getFilePathFromFilename(csv_files, "wayarea.csv");
        List<List<string>> data_str = reader.Read(csv_path);
        foreach (var line in data_str)
        {
            if (count != 0)
            {
                wayarea_data.Add(new WayArea(int.Parse(line[0]), int.Parse(line[1])));
            }
            count++;
        }
    }
}

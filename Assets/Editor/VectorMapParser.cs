using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using VectorMapData;

public class VectorMapParser
{
    public VectorMapParser()
    {
        reader = new CsvReader();
        points_data = new Dictionary<int, Point>();
        lines_data = new Dictionary<int, Line>();
        whitelines_data = new Dictionary<int, WhiteLine>();
        yellowlines_data = new Dictionary<int, YellowLine>();
        road_edges_data = new Dictionary<int, RoadEdge>();
    }

    public void parse(List<string> csv_files)
    {
        updatePointsData(csv_files);
        updateLineData(csv_files);
        updateWhiteAndYellowLineData(csv_files);
        updateRoadEdgeData(csv_files);
    }

    public VectorMapData.VectorMapData getVectorMapData()
    {
        VectorMapData.VectorMapData data = new VectorMapData.VectorMapData(points_data,lines_data,whitelines_data,yellowlines_data,road_edges_data);
        return data;
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
                }
                if(line[3] == "Y")
                {
                    yellowlines_data[int.Parse(line[1])] = new YellowLine(int.Parse(line[0]),int.Parse(line[1]), double.Parse(line[2]));
                }
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
            }
            count++;
        }
    }

    private CsvReader reader;
    //key PID, value Point Data
    private Dictionary<int, Point> points_data;
    //key LID, value Line Data
    private Dictionary<int, Line> lines_data;
    //key LID, value WhiteLine Data
    private Dictionary<int, WhiteLine> whitelines_data;
    //key LID, value YellowLine Data
    private Dictionary<int, YellowLine> yellowlines_data;
    //key LID, value RoadEdge Data
    private readonly Dictionary<int, RoadEdge> road_edges_data;
}

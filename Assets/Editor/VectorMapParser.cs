using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using VectorMapObjects;

public class VectorMapParser
{
    public VectorMapParser()
    {
        reader = new CsvReader();
    }

    public void parse(List<string> csv_files)
    {
        updatePointsData(csv_files);
        updateLineData(csv_files);
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
        string point_csv_path = getFilePathFromFilename(csv_files, "point.csv");
        List<List<string>> points_data_str = reader.Read(point_csv_path);
        foreach (var line in points_data_str)
        {
            if(count != 0)
            {
                points_data[int.Parse(line[0])] = new Point(int.Parse(line[0]), double.Parse(line[5]), double.Parse(line[4]), double.Parse(line[3]));
            }
            count++;
        }
    }

    //update line_data dict
    private void updateLineData(List<string> csv_files)
    {
        int count = 0;
        lines_data.Clear();
        string lines_csv_path = getFilePathFromFilename(csv_files, "line.csv");
        List<List<string>> line_data_str = reader.Read(lines_csv_path);
        foreach (var line in line_data_str)
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

    private CsvReader reader;
    private Dictionary<int, Point> points_data;
    private Dictionary<int, Line> lines_data;
}

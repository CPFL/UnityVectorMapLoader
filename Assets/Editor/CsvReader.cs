using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CsvReader
{
    public List<List<string> > Read(string filepath)
    {
        List < List<string> > lines = new List<List<string>>();
        if(filepath == null)
        {
            Debug.LogWarning("csv file was not found");
            return lines;
        }
        using (var sr = new System.IO.StreamReader(filepath))
        {
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                List<string> values = new List<string>(line.Split(','));
                lines.Add(values);
            }
        }
        return lines;
    }
}

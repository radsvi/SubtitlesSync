using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO.Enumeration;
using System.IO;
using System;


namespace SubtitlesSync.Lib
{
    static class JSON
    {
        public static Settings ImportFromFile(string fileName = "SubtitlesSync.json")
        {
            Settings source = new Settings();
            using (StreamReader r = new StreamReader(fileName))
            {
                string json = r.ReadToEnd();
                source = JsonSerializer.Deserialize<Settings>(json);
            }
            return source;
        }
        public static void ExportToFile(Settings exportItem, string fileName = "SubtitlesSync.json")
        {
            JsonSerializerOptions options = new() { WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
            string jsonString = JsonSerializer.Serialize(exportItem, options);
            File.WriteAllText(fileName, jsonString);

            //// jina verze jak napsat WriteAllText
            //// https://learn.microsoft.com/en-us/answers/questions/699941/read-and-process-json-file-with-c
            //using (StreamWriter outputFile = new StreamWriter("dataReady.json"))
            //{
            //    outputFile.WriteLine(jsonString);
            //}
        }

    }
    public class Person
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string City { get; set; }
    }
    public class DataReadyPerson
    {
        public int DataReadPersonId { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string CityOfResidence { get; set; }
    }
    public class Settings
    {
        public string FolderPath { get; set; }
    }
}

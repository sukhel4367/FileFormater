using ExporterCore;
using System.Text.Json;

namespace ExporterFormat
{
    public class GenericJsonExporter<T> : IExporter<T>
    {
        public string Name => "JSON";
        public string Extension => "json";

        public void Export(List<T> data, string filePath)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
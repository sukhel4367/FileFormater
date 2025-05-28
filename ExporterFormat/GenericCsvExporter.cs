using ExporterCore;
using System.Reflection;
using System.Text;

namespace ExporterFormat
{
    public class GenericCsvExporter<T> : IExporter<T>
    {
        public string Name => "CSV";
        public string Extension => "csv";

        public void Export(List<T> data, string filePath)
        {
            var sb = new StringBuilder();
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            sb.AppendLine(string.Join(",", props.Select(p => p.Name)));

            foreach (var item in data)
            {
                sb.AppendLine(string.Join(",", props.Select(p => p.GetValue(item))));
            }

            File.WriteAllText(filePath, sb.ToString());
        }
    }
}
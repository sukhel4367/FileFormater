namespace ExporterCore
{
    public interface IExporter<T>
    {
        string Name { get; }
        string Extension { get; }
        void Export(List<T> data, string filePath);
    }

   

  
}
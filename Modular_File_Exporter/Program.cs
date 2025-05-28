using ExporterCore;
using System.Reflection;

namespace ExpoterApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Which Data Model are you choosing :\n");
            Console.WriteLine("1-> Employee");
            Console.WriteLine("2-> Product");
            Console.Write("\nEnter Model number : ");
            string choice = Console.ReadLine();

            switch (choice.Trim())
            {
                case "1":
                    ExportData<Employee>();
                    break;
                case "2":
                    ExportData<Product>();
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }



        public static void ExportData<T>() where T : new()
        {
            Console.WriteLine("");
            Console.Write("No of records to be added: ");
            int count = Convert.ToInt32(Console.ReadLine());

            var dataList = new List<T>();
            var propertyList = typeof(T).GetProperties();

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\n--- Record {i + 1} ---\n");
                var obj = new T();          // new object of employee or product is created to get input and store the object in list

                foreach (var j in propertyList)
                {
                    Console.Write($"{j.Name}: ");
                    string input = Console.ReadLine();

                    try
                    {
                        object value = Convert.ChangeType(input, j.PropertyType);
                        j.SetValue(obj, value);
                    }
                    catch
                    {
                        Console.WriteLine($"Invalid input for {j.Name} enter again.");
                        i--;
                        break;
                    }
                }

                dataList.Add(obj);
            }




            var dllFolderPath = @"C:\Users\SukhelAhamedN\source\repos\Modular_File_Exporter\Dlls";  
            
            var exporterInstanceList = new List<IExporter<T>>();

            var dllFiles = Directory.GetFiles(dllFolderPath, "*.dll");

            foreach (var dll in dllFiles)
            {
                var assembly = Assembly.LoadFrom(dll);

                var exporterTypes = assembly.GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract)
                    .Where(t => t.GetInterfaces().Any(i =>
                        i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IExporter<>)))
                    .ToList();

                foreach (var openGenerics in exporterTypes)
                {
                    var closedGenerics = openGenerics.MakeGenericType(typeof(T));
                    var instance = (IExporter<T>)Activator.CreateInstance(closedGenerics);
                    exporterInstanceList.Add(instance);
                }
            }

            if (exporterInstanceList==null)
            {
                Console.WriteLine("No exporters found");
                return;
            }





            Console.WriteLine("");

            for (int i = 0; i < exporterInstanceList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {exporterInstanceList[i].Name}");
            }

            Console.Write("\nChoose format: ");
            int formatOption = Convert.ToInt32(Console.ReadLine());

            var option = exporterInstanceList[formatOption - 1];



            Console.Write("\nEnter file name: ");
            
            var filePath = $"{Console.ReadLine()}.{option.Extension}";
            filePath = Path.Combine(@"C:\MiniProj1", filePath);

            option.Export(dataList, filePath);
            Console.WriteLine($"Export completed. File saved at: {Path.GetFullPath(filePath)}");
        }
    }
}

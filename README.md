# Modular File Exporter (C# Console Application)

## 📦 Overview

This is a **Modular File Exporter** built using C#. It allows exporting data for different models (like `Employee`, `Product`, etc.) into multiple file formats (like CSV, JSON, XML) using a plugin-based architecture with dynamic DLL loading and reflection.

Each exporter format (e.g., CSV, JSON, XML) is implemented as a separate class library (DLL) and discovered at runtime, making the system **easily extensible**.

---

## 📁 Project Structure

Modular_File_Exporter/
│
├── ExporterCore/ # Contains common models (Employee, Product) and interfaces (IExporter<T>)
├── ExporterFormats/ # Contains format-specific exporters (e.g., CsvExporter, JsonExporter)
├── ExporterXML/ # (Optional) New format-specific exporter (e.g., XmlExporter)
├── Dlls/ # Folder where all built exporter DLLs are placed
├── ExpoterApp/ # Console application where user interacts and data is exported
├── MiniProj1/ # Folder where exported files are saved
└── README.md # This file

markdown
Copy
Edit

---

## ⚙️ How It Works

1. **User selects a data model** (Employee/Product).
2. **User inputs the number of records** and enters data for each.
3. The program scans the `Dlls` folder to **dynamically load all exporters** implementing `IExporter<T>`.
4. **Available exporters are listed** to the user.
5. **User selects a format**, provides a file name, and the data is exported in the selected format.

---

## 🏗️ Prerequisites

- .NET SDK (7 or 8)
- Visual Studio or any C#-compatible IDE
- Proper folder setup for:
  - `Dlls` (containing built exporter DLLs like `ExporterFormat.dll`, `ExporterXML.dll`)
  - `MiniProj1` (or any folder to save exported files)

---

## 🧪 How to Run

1. Clone or download the repository.
2. Build all projects:
   - `ExporterCore`
   - All exporter format projects (e.g., `ExporterFormats`, `ExporterXML`)
   - Place their `.dll` files into the `Dlls` folder.
3. Run the `ExpoterApp` console application.
4. Follow the on-screen prompts to:
   - Select model
   - Input data
   - Choose export format
   - Provide file name

---

## ➕ Adding a New Export Format

1. Create a new **Class Library** project (e.g., `ExporterExcel`).
2. Reference the `ExporterCore` project.
3. Implement the `IExporter<T>` interface in a class (e.g., `GenericExcelExporter<T>`).
4. Build the project and place the resulting `.dll` in the `Dlls` folder.
5. Run the app — the new format will appear automatically!

---

## 📄 Sample Interface

```csharp
public interface IExporter<T>
{
    string Name { get; }
    string Extension { get; }
    void Export(List<T> data, string filePath);
}
🙋‍♂️ Author
Sukhel Ahamed N

Developed as a learning project to understand generics, reflection, interfaces, and plugin architecture in C#.


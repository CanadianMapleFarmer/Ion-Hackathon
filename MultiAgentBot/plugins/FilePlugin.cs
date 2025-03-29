using System;
using System.ComponentModel;
using System.IO;
using Microsoft.SemanticKernel;

public class FilePlugin
{

    /// <summary>
    /// Writes content to an existing file or creates a new one if it does not exist.
    /// </summary>
    /// <param name="filePath">The full path of the file.</param>
    /// <param name="content">The content to write into the file.</param>
    [KernelFunction("WriteToFile")]
    [Description("Writes to a file, takes in a file path and content")]
    // [return: Description("A list of markets")]
    public static void WriteToFile(string filePath, string content)
    {
        try
        {
            EnsureDirectoryExists(filePath);
            File.AppendAllText(filePath, content);
            Console.WriteLine($"Content written successfully to: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to file: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates an empty file at the specified path.
    /// </summary>
    /// <param name="filePath">The full path where the file should be created.</param>
    [KernelFunction("CreateFile")]
    [Description("Creates a file, takes in a file path")]
    [return: Description("A list of markets")]
    public static void CreateFile(string filePath)
    {
        try
        {
            EnsureDirectoryExists(filePath);
            using (File.Create(filePath)) { }
            Console.WriteLine($"File created successfully at: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating file: {ex.Message}");
        }
    }

    /// <summary>
    /// Ensures that the directory for the given file path exists.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    private static void EnsureDirectoryExists(string filePath)
    {
        string? directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }
}

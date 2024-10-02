using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SelectionSortAI
{
    public static class DataReader
    {
        //=====================================================================================
        //
        //  Class Name          :   DataReader
        //
        //  Developer           :   Luis Caceres
        //                          NBCC Miramichi
        //
        //  Synopsis            :   This class is responsible for reading and processing data files.
        //          
        //
        //          Date                    Developer               Comments
        //          ====                    =========               ========
        //          Sep 26 2024             Luis Caceres            Initial Setup
        //
        //=====================================================================================
        public static List<FileData> ReadFiles(string dataDirectory)
        //=====================================================================================
        //
        //  Method Name         :   ReadFiles
        //
        //  Developer           :   Luis Caceres
        //                          NBCC Miramichi
        //
        //  Synopsis            :   This method reads and processes all data files from the specified directory.
        //                          Based on the file name, it extracts a numerical value for sorting purposes. 
        //
        //          Date                    Developer               Comments
        //          ====                    =========               ========
        //          Sep 26 2024             Luis Caceres            Initial Setup
        //
        //=====================================================================================
        {
            // Check if the specified directory exists
            if (!Directory.Exists(dataDirectory))
            {
                // If the directory does not exist, throw an exception
                throw new DirectoryNotFoundException($"The directory '{dataDirectory}' does not exist.");
            }

            // Retrieve all .txt files from the specified directory
            string[] dataFiles = Directory.GetFiles(dataDirectory, "*.txt");

            // If no data files are found, notify the user and return an empty list
            if (dataFiles.Length == 0)
            {
                Console.WriteLine("No data files found in the specified directory.");
                return new List<FileData>();
            }

            // Process each file to create a list of FileData objects
            var sortedFiles = dataFiles
                .Select(file => new FileData
                {
                    FilePath = file, // Full path to the data file
                    FileName = Path.GetFileName(file), // Name of the data file with extension
                    Number = ExtractNumber(Path.GetFileNameWithoutExtension(file)), // Extracted number from filename
                    IsSorted = Path.GetFileNameWithoutExtension(file).Contains("sorted"), // Check if filename contains "sorted"
                    Numbers = ReadNumbersFromFile(file) // Parse numbers from the file
                })
                .OrderBy(f => f.Number) // Order the list by the extracted number
                .ThenBy(f => !f.IsSorted) // Within the same number, place sorted files before unsorted
                .ToList(); // Convert the result to a List<FileData>

            // Return the sorted list of FileData objects
            return sortedFiles;
        }

        public static int[] ReadNumbersFromFile(string filePath)
        //=====================================================================================
        //
        //  Method Name         :   ReadNumbersFromFile
        //
        //  Developer           :   Luis Caceres
        //                          NBCC Miramichi
        //
        //  Synopsis            :   This method reads integers from the specified file. It assumes that
        //                          integers are separated by spaces, commas, new lines, or tabs.
        //
        //          Date                    Developer               Comments
        //          ====                    =========               ========
        //          Sep 26 2024             Luis Caceres            Initial Setup
        //
        //=====================================================================================
        {
            try
            {
                // Read the entire content of the file as a single string
                string content = File.ReadAllText(filePath);

                // Split the content into individual string tokens based on delimiters
                // Delimiters include space, comma, newline, carriage return, and tab
                return content
                    .Split(new[] { ' ', ',', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse) // Convert each string token to an integer
                    .ToArray(); // Convert the IEnumerable<int> to an int array
            }
            catch (Exception ex)
            {
                // If an error occurs (e.g., file not found, invalid format), throw a new exception with additional context
                throw new InvalidOperationException($"Failed to read numbers from file '{filePath}'.", ex);
            }
        }

        public static int ExtractNumber(string fileName)
        //=====================================================================================
        //
        //  Method Name         :   ExtractNumber
        //
        //  Developer           :   Luis Caceres
        //                          NBCC Miramichi
        //
        //  Synopsis            :   This method extracts the first numerical value found in the filename.
        //                          If no number is found, it returns 0.
        //
        //          Date                    Developer               Comments
        //          ====                    =========               ========
        //          Sep 26 2024             Luis Caceres            Initial Setup
        //
        //=====================================================================================
        {
            // Use regular expression to find the first sequence of digits in the filename
            var match = Regex.Match(fileName, @"\d+");

            // If a match is found, parse and return the number; otherwise, return 0
            return match.Success ? int.Parse(match.Value) : 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertionSortAI
{
    public static class PerformanceMeasurer
    //=====================================================================================
    //
    //  Class Name          :   PerformanceMeasurer
    //
    //  Developer           :   Luis Caceres
    //                          NBCC Miramichi
    //
    //  Synopsis            :   This class is responsible for measuring the performance
    //                          of a given sorting method across multiple files.
    //
    //          Date                    Developer               Comments
    //          ====                    =========               ========
    //          Sep 26 2024             Luis Caceres            Initial Setup
    //
    //=====================================================================================
    {
        public static void MeasurePerformance(Action<int[]> sortMethod, List<FileData> files, string resultsFile)
        //=====================================================================================
        //
        //  Method Name         :   MeasurePerformance
        //
        //  Developer           :   Luis Caceres
        //                          NBCC Miramichi
        //
        //  Synopsis            :   This method measures the performance of a given sorting
        //                          method across multiple files, logging the results to a CSV file.
        //
        //          Date                    Developer               Comments
        //          ====                    =========               ========
        //          Sep 26 2024             Luis Caceres            Initial Setup
        //
        //=====================================================================================
        {
            // Check if the sort method is provided; throw exception if null
            if (sortMethod == null)
                throw new ArgumentNullException(nameof(sortMethod), "Sort method cannot be null.");

            // Check if the files list is provided; throw exception if null
            if (files == null)
                throw new ArgumentNullException(nameof(files), "Files list cannot be null.");

            // Initialize the results CSV file with headers
            InitializeResultsFile(resultsFile);

            // Iterate through each file to perform sorting and logging
            foreach (var file in files)
            {
                try
                {
                    // Clone the numbers array to avoid modifying the original data
                    int[] numbersToSort = (int[])file.Numbers.Clone();

                    // Measure the time taken by the sorting method
                    double elapsedMilliseconds = MeasureSinglePerformance(sortMethod, numbersToSort);

                    // Log the sorting result to the CSV file
                    LogResult(resultsFile, file.FileName, file.Numbers.Length, elapsedMilliseconds);

                    // Output the result to the console for real-time monitoring
                    Console.WriteLine($"Processed {file.FileName}: Size={file.Numbers.Length}, Time={elapsedMilliseconds:F4} ms");
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occur during processing of a file
                    Console.WriteLine($"Error processing file {file.FilePath}: {ex.Message}");
                }
            }
        }

        private static double MeasureSinglePerformance(Action<int[]> sortMethod, int[] arrayToSort)
        //=====================================================================================
        //
        //  Method Name         :   MeasureSinglePerformance
        //
        //  Developer           :   Luis Caceres
        //                          NBCC Miramichi
        //
        //  Synopsis            :   This method measures the performance of a given sorting
        //                          method on a single array of integers. It returns the elapsed time.
        //                          This ensures that the performance measurement is isolated and accurate.
        //
        //          Date                    Developer               Comments
        //          ====                    =========               ========
        //          Sep 26 2024             Luis Caceres            Initial Setup
        //
        //=====================================================================================
        {
            // Start the stopwatch before sorting
            Stopwatch timer = Stopwatch.StartNew();

            // Execute the sorting method
            sortMethod(arrayToSort);

            // Stop the stopwatch after sorting
            timer.Stop();

            // Return the elapsed time in milliseconds
            return timer.Elapsed.TotalMilliseconds;
        }

        private static void InitializeResultsFile(string resultsFile)
        //=====================================================================================
        //
        //  Method Name         :   InitializeResultsFile
        //
        //  Developer           :   Luis Caceres
        //                          NBCC Miramichi
        //
        //  Synopsis            :   This method initializes the results CSV file by writing
        //                          the header row with column names. If the file already exists,
        //                          it will be overwritten.
        //
        //
        //          Date                    Developer               Comments
        //          ====                    =========               ========
        //          Sep 26 2024             Luis Caceres            Initial Setup
        //
        //=====================================================================================
        {
            // Define the header for the CSV file
            string header = "FileName,DataSize,ElapsedMilliseconds\n";

            // Write the header to the results file, overwriting any existing content
            File.WriteAllText(resultsFile, header);
        }

        // Appends a single result entry to the CSV file
        private static void LogResult(string resultsFile, string fileName, long dataSize, double elapsedMilliseconds)
        //=====================================================================================
        //
        //  Method Name         :   InitializeResultsFile
        //
        //  Developer           :   Luis Caceres
        //                          NBCC Miramichi
        //
        //  Synopsis            :   This method appends a single result entry to the results CSV file.
        //                          The entry includes the file name, data size, and elapsed time in milliseconds.
        //
        //
        //          Date                    Developer               Comments
        //          ====                    =========               ========
        //          Sep 26 2024             Luis Caceres            Initial Setup
        //
        //=====================================================================================
        {
            // Create a formatted CSV line with the sorting results
            string resultLine = $"{fileName},{dataSize},{elapsedMilliseconds:F4}\n";

            // Append the result line to the results CSV file
            File.AppendAllText(resultsFile, resultLine);
        }
    }
}

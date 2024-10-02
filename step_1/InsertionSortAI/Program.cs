using InsertionSortAI;
using System;
using System.IO;

class InsertionSortExample
{
    static void Main()
    //=====================================================================================
    //
    //  App Name            :   InsertionSortAI
    //
    //  Developer           :   Luis Caceres
    //                          NBCC Miramichi
    //
    //  Synopsis            :   This application measures the performance of an AI generated
    //                          Insertion Sort algorithm on a set of data files.
    //
    //          Date                    Developer               Comments
    //          ====                    =========               ========
    //          Sep 28 2024             Luis Caceres            Initial Setup
    //
    //=====================================================================================
    {
        try
        {
            // Combine the base directory of the executable with the "data" folder to get the data directory path
            string dataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");

            // Combine the base directory of the executable with the desired CSV results file name
            string resultsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SortResults.csv");

            // Use DataReader to read and process all data files from the specified data directory
            var files = DataReader.ReadFiles(dataDirectory);

            // Use PerformanceMeasurer to measure the performance of the InsertionSort method on the read files
            // The results will be logged to the specified CSV file
            PerformanceMeasurer.MeasurePerformance(InsertionSort, files, resultsFile);

            // Output a confirmation message to the console indicating that sorting is complete
            // and where the results have been saved
            Console.WriteLine($"\nSorting completed. Results saved to {resultsFile}");
        }
        catch (Exception ex)
        {
            // If any exceptions occur during the execution of the try block,
            // catch them and output an error message to the console
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Insertion Sort function
    static void InsertionSort(int[] array)
    {
        for (int i = 1; i < array.Length; i++)
        {
            int key = array[i];
            int j = i - 1;

            // Move elements of array[0..i-1] that are greater than key
            // to one position ahead of their current position
            while (j >= 0 && array[j] > key)
            {
                array[j + 1] = array[j];
                j--;
            }
            array[j + 1] = key;
        }
    }
}

using System;
using System.IO;
using SelectionSortAI;

class Program
{
    static void Main()
    //=====================================================================================
    //
    //  App Name            :   SelectionSortAI
    //
    //  Developer           :   Luis Caceres
    //                          NBCC Miramichi
    //
    //  Synopsis            :   This application measures the performance of an AI generated
    //                          SelectionSort algorithm on a set of data files.
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

            // Use PerformanceMeasurer to measure the performance of the SelectionSort method on the read files
            // The results will be logged to the specified CSV file
            PerformanceMeasurer.MeasurePerformance(SelectionSort, files, resultsFile);

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

    // Method to perform selection sort
    static void SelectionSort(int[] arr)
    {
        int n = arr.Length;

        // Loop through the array
        for (int i = 0; i < n - 1; i++)
        {
            // Find the minimum element in unsorted portion of the array
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (arr[j] < arr[minIndex])
                {
                    minIndex = j;
                }
            }

            // Swap the found minimum element with the first element of unsorted portion
            int temp = arr[minIndex];
            arr[minIndex] = arr[i];
            arr[i] = temp;
        }
    }
}

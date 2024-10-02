using BubbleSortAI;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    //=====================================================================================
    //
    //  App Name            :   BubbleSortAI
    //
    //  Developer           :   Luis Caceres
    //                          NBCC Miramichi
    //
    //  Synopsis            :   This application measures the performance of an AI generated
    //                          Bubble Sort algorithm on a set of data files.
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

            // Use PerformanceMeasurer to measure the performance of the BubbleSort method on the read files
            // The results will be logged to the specified CSV file
            PerformanceMeasurer.MeasurePerformance(BubbleSort, files, resultsFile);

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


    // Bubble Sort algorithm
    static void BubbleSort(int[] array)
    {
        int n = array.Length;
        bool swapped;
        for (int i = 0; i < n - 1; i++)
        {
            swapped = false;
            for (int j = 0; j < n - i - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    // Swap array[j] and array[j + 1]
                    int temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                    swapped = true;
                }
            }
            // If no elements were swapped, the array is already sorted
            if (!swapped)
                break;
        }
    }
}

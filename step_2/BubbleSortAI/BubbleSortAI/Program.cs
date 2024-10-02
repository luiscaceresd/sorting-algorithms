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
    //  App Name            :   BubbleSortAIOptimized
    //
    //  Developer           :   Luis Caceres
    //                          NBCC Miramichi
    //
    //  Synopsis            :   This application measures the performance of an AI generated
    //                          Bubble Sort algorithm on a set of data files. This algortihm
    //                          was optimized to improve performance and readability.
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
            PerformanceMeasurer.MeasurePerformance(BubbleSortOptimized, files, resultsFile);

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


    static void BubbleSortOptimized(int[] array)
    //=====================================================================================
    //
    //  Method Name         :   BubbleSortOptimized
    //
    //  Developer           :   Luis Caceres
    //                          NBCC Miramichi
    //
    //  Synopsis            :   This method sorts an array of integers using the Bubble Sort
    //                          The algorithm has been optimized for performance and readability.
    //
    //          Date                    Developer               Comments
    //          ====                    =========               ========
    //          Sep 26 2024             Luis Caceres            Initial Setup
    //
    //=====================================================================================
    {
        // Initialize the last unsorted index to the last index of the array
        int lastUnsortedIndex = array.Length - 1;

        // Continue looping until the array is fully sorted
        while (lastUnsortedIndex > 0)
        {
            // Keep track of the index of the last swap made during this pass
            // If no swaps occur, the array is sorted, and lastSwapIndex will remain 0
            int lastSwapIndex = 0;

            // Iterate through the unsorted portion of the array
            for (int currentIndex = 0; currentIndex < lastUnsortedIndex; currentIndex++)
            {
                // The index of the next element to compare with
                int nextIndex = currentIndex + 1;

                // If the current element is greater than the next element, swap them
                if (array[currentIndex] > array[nextIndex])
                {
                    // Swap array[currentIndex] and array[nextIndex]
                    int temporary = array[currentIndex];
                    array[currentIndex] = array[nextIndex];
                    array[nextIndex] = temporary;

                    // Update lastSwapIndex to the current index where the swap occurred
                    lastSwapIndex = currentIndex;
                }
            }

            // Set lastUnsortedIndex to the position of the last swap
            // Elements beyond this index are already sorted
            lastUnsortedIndex = lastSwapIndex;
        }
    }
}

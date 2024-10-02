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

    static void InsertionSort(int[] array)
    //=====================================================================================
    //
    //  Method Name         :   InsertionSortOptimized
    //
    //  Developer           :   Luis Caceres
    //                          NBCC Miramichi
    //
    //  Synopsis            :   This method implements an optimized version of the Insertion Sort
    //                          algorithm that uses binary search to find the correct insertion point.
    //                          By using binary search, the algorithm reduces the number
    //                          of comparisons required to find the correct insertion point.
    //                          This results in a more efficient sorting process, especially
    //                          for large data sets.
    //
    //          Date                    Developer               Comments
    //          ====                    =========               ========
    //          Sep 26 2024             Luis Caceres            Initial Setup
    //
    //=====================================================================================
    {
        int length = array.Length;

        // Iterate over each element starting from the second one
        for (int unsortedIndex = 1; unsortedIndex < length; unsortedIndex++)
        {
            // Store the current value to be positioned in the sorted portion
            int currentValue = array[unsortedIndex];

            // Initialize the search boundaries for binary search
            int left = 0;
            int right = unsortedIndex - 1;

            // Use binary search to find the correct insertion point for currentValue
            while (left <= right)
            {
                // Calculate the middle index of the search range
                int middle = left + (right - left) / 2;

                if (array[middle] > currentValue)
                {
                    // If currentValue is less than the middle element, search the left half
                    right = middle - 1;
                }
                else
                {
                    // If currentValue is greater or equal, search the right half
                    left = middle + 1;
                }
            }

            // Shift elements in the sorted portion to make room for currentValue
            for (int shiftIndex = unsortedIndex - 1; shiftIndex >= left; shiftIndex--)
            {
                array[shiftIndex + 1] = array[shiftIndex];
            }

            // Insert currentValue at the determined position
            array[left] = currentValue;
        }
    }
}

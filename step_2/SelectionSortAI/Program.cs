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
            PerformanceMeasurer.MeasurePerformance(BidirectionalSelectionSort, files, resultsFile);

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

    static void BidirectionalSelectionSort(int[] array)
    //=====================================================================================
    //
    //  Method Name         :   BidirectionalSelectionSort
    //
    //  Developer           :   Luis Caceres
    //                          NBCC Miramichi
    //
    //  Synopsis            :   This method sorts an array of integers using the bidirectional
    //                          selection sort algorithm. Which is a variation of the selection
    //                          sort algorithm that simultaneously finds the minimum and maximum
    //                          elements in the unsorted portion of the array. It then swaps the
    //                          minimum element with the first element of the unsorted portion and
    //                          the maximum element with the last element of the unsorted portion.
    //                          Hence, it reduces the number of comparisons and swaps required to
    //                          sort the array.
    //
    //          Date                    Developer               Comments
    //          ====                    =========               ========
    //          Sep 26 2024             Luis Caceres            Initial Setup
    //
    //=====================================================================================
    {
        // Initialize pointers to the start and end of the unsorted portion of the array
        int leftIndex = 0;
        int rightIndex = array.Length - 1;

        // Continue sorting until the unsorted portion is fully sorted
        while (leftIndex < rightIndex)
        {
            // Assume the first element in the unsorted portion is both the minimum and maximum
            int minIndex = leftIndex;
            int maxIndex = leftIndex;

            // Traverse the unsorted portion to find the actual minimum and maximum elements
            for (int currentIndex = leftIndex; currentIndex <= rightIndex; currentIndex++)
            {
                // Update minIndex if a smaller element is found
                if (array[currentIndex] < array[minIndex])
                {
                    minIndex = currentIndex;
                }
                // Update maxIndex if a larger element is found
                if (array[currentIndex] > array[maxIndex])
                {
                    maxIndex = currentIndex;
                }
            }

            // Swap the minimum element with the first element of the unsorted portion
            if (minIndex != leftIndex)
            {
                int temporary = array[minIndex];
                array[minIndex] = array[leftIndex];
                array[leftIndex] = temporary;

                // If the maximum element was at leftIndex, update maxIndex after the swap
                if (maxIndex == leftIndex)
                {
                    maxIndex = minIndex;
                }
            }

            // Swap the maximum element with the last element of the unsorted portion
            if (maxIndex != rightIndex)
            {
                int temporary = array[maxIndex];
                array[maxIndex] = array[rightIndex];
                array[rightIndex] = temporary;
            }

            // Move the pointers inward to narrow the unsorted portion
            leftIndex++;
            rightIndex--;
        }
    }
}

namespace CustomSort
{
    internal class Program
    {
        static void Main()
        //=====================================================================================
        //
        //  App Name            :   CustomSort
        //
        //  Developer           :   Luis Caceres
        //                          NBCC Miramichi
        //
        //  Synopsis            :   This application measures the performance of an custom
        //                          sorting algorithm across multiple data files.
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

                // Use PerformanceMeasurer to measure the performance of the BuiltInSort method on the read files
                // The results will be logged to the specified CSV file
                PerformanceMeasurer.MeasurePerformance(BuiltInSort, files, resultsFile);

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

        static void BuiltInSort(int[] array)
        //=====================================================================================
        //
        //  Method Name         :   BuiltInSort
        //
        //  Developer           :   Luis Caceres
        //                          NBCC Miramichi
        //
        //  Synopsis            :   This method sorts an integer array using the built-in Array.Sort method.
        //                          It has a restriction of sorting a maximum of 500 elements at a time.
        //
        //          Date                    Developer               Comments
        //          ====                    =========               ========
        //          Sep 26 2024             Luis Caceres            Initial Setup
        //
        //=====================================================================================
        {
            // Maximum number of elements to sort at one time
            int chunkSize = 500;
            // Total length of the array
            int arrayLength = array.Length;
            // Calculate the number of chunks required to cover the entire array
            int numChunks = (arrayLength + chunkSize - 1) / chunkSize;

            // Step 1: Sort each chunk of 500 elements
            for (int chunkIndex = 0; chunkIndex < numChunks; chunkIndex++)
            {
                // Starting index of the current chunk
                int chunkStartIndex = chunkIndex * chunkSize;
                // Ending index (exclusive) of the current chunk
                int chunkEndIndex = Math.Min(chunkStartIndex + chunkSize, arrayLength);
                // Size of the current chunk
                int currentChunkSize = chunkEndIndex - chunkStartIndex;
                // Sort the current chunk using built-in sort
                Array.Sort(array, chunkStartIndex, currentChunkSize);
            }

            // Step 2: Iteratively merge the sorted chunks
            // Initialize current chunk size for merging
            int currentMergeSize = chunkSize;
            while (currentMergeSize < arrayLength)
            {
                // Merge chunks pairwise
                for (int mergeStartIndex = 0; mergeStartIndex < arrayLength; mergeStartIndex += 2 * currentMergeSize)
                {
                    // Starting index of the left chunk
                    int leftStartIndex = mergeStartIndex;
                    // Starting index of the right chunk
                    int rightStartIndex = Math.Min(mergeStartIndex + currentMergeSize, arrayLength);
                    // Ending index (exclusive) of the right chunk
                    int rightEndIndex = Math.Min(mergeStartIndex + 2 * currentMergeSize, arrayLength);

                    // Merge the two chunks
                    MergeChunks(array, leftStartIndex, rightStartIndex, rightEndIndex);
                }
                // Double the chunk size for the next level of merging
                currentMergeSize *= 2;
            }
        }

        static void MergeChunks(int[] array, int leftStartIndex, int rightStartIndex, int rightEndIndex)
        //=====================================================================================
        //
        //  Method Name         :   Merge
        //
        //  Developer           :   Luis Caceres
        //                          NBCC Miramichi
        //
        //  Synopsis            :   This method merges two sorted subarrays into a single sorted array.
        //                          It is used as part of the built-in sorting algorithm.
        //
        //          Date                    Developer               Comments
        //          ====                    =========               ========
        //          Sep 26 2024             Luis Caceres            Initial Setup
        //
        //=====================================================================================
        {
            // Size of the left subarray
            int leftSize = rightStartIndex - leftStartIndex;
            // Size of the right subarray
            int rightSize = rightEndIndex - rightStartIndex;

            // Create temporary arrays to hold the elements of the left and right subarrays
            int[] leftArray = new int[leftSize];
            int[] rightArray = new int[rightSize];

            // Copy data into temporary arrays
            Array.Copy(array, leftStartIndex, leftArray, 0, leftSize);
            Array.Copy(array, rightStartIndex, rightArray, 0, rightSize);

            // Indices to traverse leftArray, rightArray, and the main array
            int leftIndex = 0;  // Current index in leftArray
            int rightIndex = 0; // Current index in rightArray
            int mergedIndex = leftStartIndex; // Current index in the main array

            // Merge the two arrays back into the main array
            while (leftIndex < leftSize && rightIndex < rightSize)
            {
                // Compare the elements at the current indices of leftArray and rightArray
                if (leftArray[leftIndex] <= rightArray[rightIndex])
                {
                    // If the element in the left array is smaller or equal, copy it to the main array
                    array[mergedIndex++] = leftArray[leftIndex++];
                }
                else
                { 
                    // If the element in the right array is smaller, copy it to the main array
                    array[mergedIndex++] = rightArray[rightIndex++];
                }
            }

            // Copy any remaining elements from leftArray, if any
            while (leftIndex < leftSize)
            {
                array[mergedIndex++] = leftArray[leftIndex++];
            }

            // Copy any remaining elements from rightArray, if any
            while (rightIndex < rightSize)
            {
                array[mergedIndex++] = rightArray[rightIndex++];
            }
        }
    }
}

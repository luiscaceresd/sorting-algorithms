using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomSort
{
    public class FileData
    //=====================================================================================
    //
    //  Class Name          :   FileData
    //
    //  Developer           :   Luis Caceres
    //                          NBCC Miramichi
    //
    //  Synopsis            :   This class represents the data contained within a single file.
    //
    //          Date                    Developer               Comments
    //          ====                    =========               ========
    //          Sep 26 2024             Luis Caceres            Initial Setup
    //
    //=====================================================================================
    {
        // The full file path of the data file
        public required string FilePath { get; set; }

        // The name of the data file, including its extension
        public required string FileName { get; set; }

        // The numerical value extracted from the file name for sorting purposes
        public int Number { get; set; }

        // Indicates whether the data within the file is already sorted
        public bool IsSorted { get; set; }

        // An array of integers read from the data file
        public required int[] Numbers { get; set; }
    }
}

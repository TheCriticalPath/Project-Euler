using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;
using System.Windows.Forms;
using System.ComponentModel.Composition;


namespace Problem_11
{
    [Export(typeof(IEulerPlugin))]
    public class Problem_11 : IEulerPlugin
    {
        public bool IsAsync { get { return false; } }
        public long _limit;
        //public int[,] _grid;  //DIFFICULT TO ACCESS A SINGLE ROW WITHOUT LOOPING TO CREATE A NEW 1D ARRAY.
        public int[][] _grid;
        public bool ImplementsGetInput { get { return true; } }
        public int ID { get { return 11; } }
        public string Name
        {
            get { return string.Format("Problem {0}: {1}", ID, Title); }
        }
        public string Title { get { return string.Format("Largest product in a grid", ID); } }
        public string Description { get { return "What is the greatest product of four adjacent numbers in the same direction (up, down, left, right, or diagonally) in the 20×20 grid? (grid is hardcoded)"; } }
        public async Task<IEulerPluginContext> PerformActionAsync(IEulerPluginContext context)
        {
            throw new NotImplementedException();
        }
        public Problem_11()
        {
            _grid = new int[20][];
            _grid[0] = new int[] { 08, 02, 22, 97, 38, 15, 00, 40, 00, 75, 04, 05, 07, 78, 52, 12, 50, 77, 91, 08 };
            _grid[1] = new int[] { 49, 49, 99, 40, 17, 81, 18, 57, 60, 87, 17, 40, 98, 43, 69, 48, 04, 56, 62, 00 };
            _grid[2] = new int[] { 81, 49, 31, 73, 55, 79, 14, 29, 93, 71, 40, 67, 53, 88, 30, 03, 49, 13, 36, 65 };
            _grid[3] = new int[] { 52, 70, 95, 23, 04, 60, 11, 42, 69, 24, 68, 56, 01, 32, 56, 71, 37, 02, 36, 91 };
            _grid[4] = new int[] { 22, 31, 16, 71, 51, 67, 63, 89, 41, 92, 36, 54, 22, 40, 40, 28, 66, 33, 13, 80 };
            _grid[5] = new int[] { 24, 47, 32, 60, 99, 03, 45, 02, 44, 75, 33, 53, 78, 36, 84, 20, 35, 17, 12, 50 };
            _grid[6] = new int[] { 32, 98, 81, 28, 64, 23, 67, 10, 26, 38, 40, 67, 59, 54, 70, 66, 18, 38, 64, 70 };
            _grid[7] = new int[] { 67, 26, 20, 68, 02, 62, 12, 20, 95, 63, 94, 39, 63, 08, 40, 91, 66, 49, 94, 21 };
            _grid[8] = new int[] { 24, 55, 58, 05, 66, 73, 99, 26, 97, 17, 78, 78, 96, 83, 14, 88, 34, 89, 63, 72 };
            _grid[9] = new int[] { 21, 36, 23, 09, 75, 00, 76, 44, 20, 45, 35, 14, 00, 61, 33, 97, 34, 31, 33, 95 };
            _grid[10] = new int[] { 78, 17, 53, 28, 22, 75, 31, 67, 15, 94, 03, 80, 04, 62, 16, 14, 09, 53, 56, 92 };
            _grid[11] = new int[] { 16, 39, 05, 42, 96, 35, 31, 47, 55, 58, 88, 24, 00, 17, 54, 24, 36, 29, 85, 57 };
            _grid[12] = new int[] { 86, 56, 00, 48, 35, 71, 89, 07, 05, 44, 44, 37, 44, 60, 21, 58, 51, 54, 17, 58 };
            _grid[13] = new int[] { 19, 80, 81, 68, 05, 94, 47, 69, 28, 73, 92, 13, 86, 52, 17, 77, 04, 89, 55, 40 };
            _grid[14] = new int[] { 04, 52, 08, 83, 97, 35, 99, 16, 07, 97, 57, 32, 16, 26, 26, 79, 33, 27, 98, 66 };
            _grid[15] = new int[] { 88, 36, 68, 87, 57, 62, 20, 72, 03, 46, 33, 67, 46, 55, 12, 32, 63, 93, 53, 69 };
            _grid[16] = new int[] { 04, 42, 16, 73, 38, 25, 39, 11, 24, 94, 72, 18, 08, 46, 29, 32, 40, 62, 76, 36 };
            _grid[17] = new int[] { 20, 69, 36, 41, 72, 30, 23, 88, 34, 62, 99, 69, 82, 67, 59, 85, 74, 04, 36, 16 };
            _grid[18] = new int[] { 20, 73, 35, 29, 78, 31, 90, 01, 74, 31, 49, 71, 48, 86, 81, 16, 23, 57, 05, 54 };
            _grid[19] = new int[] { 01, 70, 54, 71, 83, 51, 54, 69, 16, 92, 33, 48, 61, 43, 52, 01, 89, 19, 67, 48 };
        }
        public IEulerPluginContext PerformAction(IEulerPluginContext context)
        {
            context.strResultLongText = GetLargestProductOfLength(_limit, ref _grid);
            return context;
        }
        private long GetLimit()
        {
            long lngLimit = 0;
            string strLimit = "4";

            while (lngLimit < 1)
            {
                Helpers.InputHelper.Show(Name, "Enter number of consecutive digits in expression", ref strLimit);
                if (!Int64.TryParse(strLimit, out lngLimit))
                {
                    lngLimit = 0;
                }

            }
            return lngLimit;
        }
        public void PerformGetInput(IEulerPluginContext context)
        {
            _limit = GetLimit();
        }
        public string GetLargestProductOfLength(long limit, ref int[][] grid)
        {
            string duples = string.Empty;
            string tmpNumbers = string.Empty, numbers = string.Empty;
            long tmpProduct = 0, product = 0;
            for (int row_offset = 0; row_offset < 20; row_offset++)
            {
                for (int col_offset = 0; col_offset < 20; col_offset++)
                {
                    if (row_offset <= 20 - limit)
                    {
                        duples = string.Empty;
                        tmpProduct = 1;
                        tmpNumbers = string.Empty;
                        for (int row = 0; row < limit; row++)
                        {
                            tmpNumbers = String.Format("{0}{1,2},", tmpNumbers,grid[row_offset + row][col_offset]);
                            tmpProduct *= grid[row_offset + row][col_offset];
                            if (tmpProduct == 0) { break; }
                            duples += string.Format("({0,2},{1,2}) ",row_offset + row, col_offset);
                            
                        }
     //                   if (tmpProduct > 0) { System.Diagnostics.Debug.WriteLine(String.Format("Left:       {0} => {1,12} => {2}", duples, tmpProduct, tmpNumbers)); }
                        if (tmpProduct > product) {
                            //System.Diagnostics.Debug.WriteLine("New product: " + tmpProduct + " > " + product);
                            product = tmpProduct; numbers = tmpNumbers;                         
                        }
                    }
                    if (col_offset <= 20 - limit)
                    { // go to the right
                        tmpProduct = 1;
                        tmpNumbers = string.Empty;
                        duples = string.Empty;
                        for (int col = 0; col < limit; col++)
                        {
                            tmpNumbers = String.Format("{0}{1,2},", tmpNumbers,grid[row_offset][col + col_offset]);
                            tmpProduct *= grid[row_offset][col + col_offset];
                            if (tmpProduct == 0) { break; }
                            duples += string.Format("({0,2},{1,2}) ", row_offset, col_offset + col);
                        }
   //                     if (tmpProduct > 0) { System.Diagnostics.Debug.WriteLine(String.Format("Right:      {0} => {1,12} => {2}", duples, tmpProduct, tmpNumbers)); }
                        //System.Diagnostics.Debug.WriteLine("Right:      " + duples + " => " + tmpProduct + " => " + tmpNumbers);
                        if (tmpProduct > product) {
                            //System.Diagnostics.Debug.WriteLine("New product: " + tmpProduct + " > " + product);
                            product = tmpProduct; numbers = tmpNumbers;
                        }
                    }
                    if (row_offset < 20 - limit && col_offset < 20 - limit) {
                        tmpProduct = 1;
                        tmpNumbers = string.Empty;
                        duples = string.Empty;
                        for (int idx = 0; idx < limit; idx++)
                        {
                            tmpNumbers = String.Format("{0}{1,2},", tmpNumbers, grid[row_offset + idx][idx + col_offset]);
                            tmpProduct *= grid[row_offset+idx][idx + col_offset];
                            if (tmpProduct == 0) { break; }
                            duples += string.Format("({0,2},{1,2}) ", row_offset + idx, col_offset + idx);
                        }
 //                       if (tmpProduct > 0) { System.Diagnostics.Debug.WriteLine(String.Format("Down Right: {0} => {1,12} => {2}", duples, tmpProduct, tmpNumbers)); }
                        if (tmpProduct > product)
                        {
                            //System.Diagnostics.Debug.WriteLine("New product: " + tmpProduct + " > " + product);
                            product = tmpProduct; 
                            numbers = tmpNumbers;
                        }                    
                    }
                    if (row_offset < 20 - limit && col_offset >= limit - 1)
                    {
                        tmpProduct = 1;
                        tmpNumbers = string.Empty;
                        duples = string.Empty;
                        for (int idx = 0; idx < limit; idx++)
                        {
                            tmpNumbers = String.Format("{0}{1,2},", tmpNumbers, grid[row_offset + idx][col_offset - idx]);
                            tmpProduct *= grid[row_offset + idx][col_offset - idx];
                            if (tmpProduct == 0) { break; }
                            duples += string.Format("({0,2},{1,2}) ", row_offset + idx, col_offset - idx);
                        }
//                        if (tmpProduct > 0) { System.Diagnostics.Debug.WriteLine(String.Format("Down Left:  {0} => {1,12} => {2}", duples, tmpProduct, tmpNumbers)); }
                        if (tmpProduct > product) {
                            //System.Diagnostics.Debug.WriteLine("New product: " + tmpProduct + " > " + product);
                            product = tmpProduct; 
                            numbers = tmpNumbers;
                        }
                    }

                }
            }



            return string.Format("The largest product of length {0} contains the numbers {1} and is equal to {2}", limit, numbers, product);
        }

        private long GetLargestProductInLine(ref int[] row, long limit, ref string numbers)
        {
            string local_numbers = string.Empty;
            long product = 1L;
            long retval = 0L;
            for (int i = 0; i < row.Length - limit; i++)
            {

                for (int k = i; k < limit + i; k++)
                {
                    product *= row[k];
                    local_numbers += row[k].ToString() + ',';
                }
                if (product == 0)
                {
                    i += (int)limit;
                }
                else if (product > retval)
                {
                    retval = product;
                    numbers = local_numbers.Substring(0, local_numbers.Length - 1);
                }
                product = 1;
                local_numbers = string.Empty;
            }
            return retval;
        }




    }
}

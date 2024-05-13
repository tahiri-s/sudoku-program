using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        int[][] sudoku = {
            new int[] {7,8,4,  1,5,9,  3,2,6},
            new int[] {5,3,9,  6,7,2,  8,4,1},
            new int[] {6,1,2,  4,3,8,  7,5,9},
            new int[] {9,2,8,  7,1,5,  4,6,3},
            new int[] {3,5,7,  8,4,6,  1,9,2},
            new int[] {4,6,1,  9,2,3,  5,8,7},
            new int[] {8,7,6,  3,9,4,  2,1,5},
            new int[] {2,4,3,  5,6,1,  9,7,8},
            new int[] {1,9,5,  2,8,7,  6,3,4}
        };

        int[][] goodSudoku2 = {
            new int[] {1,4, 2,3},
            new int[] {3,2, 4,1},

            new int[] {4,1, 3,2},
            new int[] {2,3, 1,4}
        };

        int[][] badSudoku1 =  {
            new int[] {1,2,3, 4,5,6, 7,8,9},
            new int[] {1,2,3, 4,5,6, 7,8,9},
            new int[] {1,2,3, 4,5,6, 7,8,9},

            new int[] {1,2,3, 4,5,6, 7,8,9},
            new int[] {1,2,3, 4,5,6, 7,8,9},
            new int[] {1,2,3, 4,5,6, 7,8,9},

            new int[] {1,2,3, 4,5,6, 7,8,9},
            new int[] {1,2,3, 4,5,6, 7,8,9},
            new int[] {1,2,3, 4,5,6, 7,8,9}
        };

        int[][] badSudoku2 = {
            new int[] {1,2,3,4,5},
            new int[] {1,2,3,4},
            new int[] {1,2,3,4},
            new int[] {1}
        };

        try
        {
            CheckSudoku(sudoku);
            CheckSudoku(goodSudoku2);
            CheckSudoku(badSudoku1);
            CheckSudoku(badSudoku2);
        }
        catch (Exception ex)
        {
            Console.Write(ex);
        }
    }

    public static int GetMatrixLength(int[][] matrix)
    {
        try
        {
            var length = matrix.Length;
            if (matrix.Any(row => row.Length != length)) throw new Exception();

            return length;
        }
        catch
        {
            return -1;
        }
    }

    public static bool IsSquare(int number)
    {
        try
        {
            if (number < 0) throw new Exception();
            var sqrt = Math.Sqrt(number);
            return sqrt == (int)sqrt;
        }
        catch
        {
            return false;
        }
    }

    public static int[][] TransposeMatrix(int length, int[][] matrix)
    {
        List<int[]> columns = new List<int[]>();
        for (int i = 0; i < length; i++)
        {
            columns.Add(Enumerable.Range(1, length).ToArray());
        }

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                columns[i][j] = matrix[j][i];
            }
        }

        return columns.ToArray();
    }

    public static int[][] GetSubmatrices(int length, int[][] matrix)
    {
        List<int[]> submatrices = new List<int[]>();
        var sqrt = (int)Math.Sqrt(length);

        for (int i = 0; i < length; i = i + 1)
        {
            int submatrixRow = (int)(i / sqrt) * sqrt;
            int submatrixColumn = (int)(i % sqrt) * sqrt;

            List<int> submatrix = new List<int>();
            for (int j = 0; j < sqrt; j++)
            {
                for (int k = 0; k < sqrt; k++)
                {
                    submatrix.Add(matrix[submatrixRow + j][submatrixColumn + k]);
                }
            }

            submatrices.Add(submatrix.ToArray());
        }

        return submatrices.ToArray();
    }

    public static bool ContainsAllNumbers(int length, int[] values)
    {
        try
        {
            int[] numbers = Enumerable.Range(1, length).ToArray();
            if (numbers.Any(number => !values.Contains(number))) throw new Exception();

            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool CheckSudoku(int[][] sudoku)
    {
        try
        {
            int length = GetMatrixLength(sudoku);
            bool isLengthValid = IsSquare(length);
            if (!isLengthValid) throw new Exception("Invalid Length");

            var rows = sudoku;
            var isRowsInvalid = rows.Any(row => !ContainsAllNumbers(length, row));
            if (isRowsInvalid) throw new Exception("There are invalid row(s)");

            var columns = TransposeMatrix(length, sudoku);
            var isColumnsInvalid = columns.Any(column => !ContainsAllNumbers(length, column));
            if (isColumnsInvalid) throw new Exception("There are invalid column(s)");

            var submatrices = GetSubmatrices(length, sudoku);
            var isSubmatricesInvalid = submatrices.Any(submatrix => !ContainsAllNumbers(length, submatrix));
            if (isSubmatricesInvalid) throw new Exception("There are invalid submatrix(es)");

            Console.WriteLine(true);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(false + " " + ex.Message);
            return false;
        }
    }
}
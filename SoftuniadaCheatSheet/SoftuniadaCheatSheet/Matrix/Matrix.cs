namespace SoftuniadaCheatSheet.Matrix
{
    public static class Matrix
    {
        private static int size;

        public static bool IsInBounds(char[][] matrix, int row, int col)
        {
            return row >= 0 && row < matrix.Length && col >= 0 && col < matrix[row].Length;
        }

        public static bool IsInBounds(char[,] matrix, int row, int col)
        {
            return row >= 0 && row < matrix.GetLength(0) && col >= 0 && col < matrix.GetLength(1);
        }
    }
}

public class Cell
{
    public Cell(int row, int col)
    {
        Row = row;
        Col = col;
    }

    public int Row { get; private set; }

    public int Col { get; private set; }
}

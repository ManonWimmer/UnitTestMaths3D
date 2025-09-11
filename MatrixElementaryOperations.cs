// ----- MANON WIMMER ----- //

namespace UnitTestMaths3DWimmer;

public static class MatrixElementaryOperations
{
    public static void SwapLines(MatrixInt matrix, int l1, int l2)
    {
        for (int j = 0; j < matrix.NbColumns; j++)
        {
            /*
            int temp = matrix[l1, j];
            matrix[l1, j] = matrix[l2, j];
            matrix[l2, j] = temp; 
            */
            
            (matrix[l1, j], matrix[l2, j]) = (matrix[l2, j], matrix[l1, j]);
        }
    }

    public static void SwapColumns(MatrixInt matrix, int c1, int c2)
    {
        for (int i = 0; i < matrix.NbLines; i++)
        {
            /*
            int temp = matrix[i, c1];
            matrix[i, c1] = matrix[i, c2];
            matrix[i, c2] = temp;
             */
            
            (matrix[i, c1], matrix[i, c2]) = (matrix[i, c2], matrix[i, c1]);
        }
    }

    public static void MultiplyLine(MatrixInt matrix, int line, int value)
    {
        if (value == 0)
        {
            throw new MatrixScalarZeroException("Multiply line with 0");
        }
        else if (line >= matrix.NbLines || line < 0)
        {
            throw new MatrixScalarOutOfRangeException("Multiply line out of range");
        }

        for (int j = 0; j < matrix.NbColumns; j++)
        {
            matrix[line, j] *= value;
        }
    }

    public static void MultiplyColumn(MatrixInt matrix, int column, int value)
    {
        if (value == 0)
        {
            throw new MatrixScalarZeroException("column line with 0");
        }
        else if (column >= matrix.NbColumns || column < 0)
        {
            throw new MatrixScalarOutOfRangeException("Multiply column out of range");
        }

        for (int i = 0; i < matrix.NbLines; i++)
        {
            matrix[i, column] *= value;
        }
    }

    public static void AddLineToAnother(MatrixInt matrixInt, int lineToAdd, int lineResult, int factor) // line result = result + line to add * factor
    {
        for (int j = 0; j < matrixInt.NbColumns; j++)
        {
            matrixInt[lineResult, j] += matrixInt[lineToAdd, j] * factor;
        }
    }

    public static void AddColumnToAnother(MatrixInt matrixInt, int columnToAdd, int columnResult, int factor)
    {
        for (int i = 0; i < matrixInt.NbLines; i++)
        {
            matrixInt[i, columnResult] += matrixInt[i, columnToAdd] * factor;
        }
    }
}

public class MatrixScalarZeroException : Exception
{
    public MatrixScalarZeroException() {}

    public MatrixScalarZeroException(string message) : base(message) {}

    public MatrixScalarZeroException(string message, Exception inner) : base(message, inner) {}
}

public class MatrixScalarOutOfRangeException : Exception
{
    public MatrixScalarOutOfRangeException() {}

    public MatrixScalarOutOfRangeException(string message) : base(message) {}

    public MatrixScalarOutOfRangeException(string message, Exception inner) : base(message, inner) {}
}
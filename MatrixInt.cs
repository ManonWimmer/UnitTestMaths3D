// ----- MANON WIMMER ----- //

namespace UnitTestMaths3DWimmer;

public class MatrixInt
    {
        public int NbLines;
        public int NbColumns;
        public int[,] Matrix;

        public MatrixInt(int n, int m)
        {
            this.NbLines = n;
            this.NbColumns = m;
            
            this.Matrix = new int[this.NbLines, this.NbColumns];
        }
        
        public MatrixInt(int[,] m)
        {
            this.NbLines = m.GetLength(0);
            this.NbColumns = m.GetLength(1);
            
            this.Matrix = m;
        }
        
        public MatrixInt(MatrixInt m)
        {
            this.NbLines = m.NbLines;
            this.NbColumns = m.NbColumns;

            int[,] copiedMatrix = new int[this.NbLines, this.NbColumns];
            for (int i = 0; i < this.NbLines; i++)
            {
                for (int j = 0; j < this.NbColumns; j++)
                {
                    copiedMatrix[i, j] = m[i, j];
                }
            }
            this.Matrix = copiedMatrix;
        }
        
        public int this[int i, int j]
        {
            get => this.Matrix[i, j];
            set => this.Matrix[i, j] = value;
        }
        
        public int[,] ToArray2D()
        {
            return this.Matrix;
        }

        public static MatrixInt Identity(int n)
        {
            MatrixInt m = new MatrixInt(n, n);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j) m[i, j] = 1;
                    else m[i, j] = 0;
                }
            }
            
            return m;
        }

        public bool IsIdentity()
        {
            if (this.NbLines != this.NbColumns) return false;
            
            for (int i = 0; i < this.NbLines; i++)
            {
                for (int j = 0; j < this.NbColumns; j++)
                {
                    if ((i == j && this.Matrix[i, j] != 1) || (i != j && this.Matrix[i, j] != 0))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void Multiply(int value)
        {
            for (int i = 0; i < this.NbLines; i++)
            {
                for (int j = 0; j < this.NbColumns; j++)
                {
                    this.Matrix[i, j] = value * this.Matrix[i, j];
                }
            }
        }
        
        public MatrixInt Multiply(MatrixInt m2)
        {
            if (this.NbColumns != m2.NbLines)
            {
                throw new MatrixMultiplyException("Matrix sizes do not match for multiplication.");
            }
            
            MatrixInt m = new MatrixInt(this.NbLines, m2.NbColumns);
            
            for (int i = 0; i < this.NbLines; i++)
            {
                for (int j = 0; j < m2.NbColumns; j++)
                {
                    int value = 0;
                    
                    for (int k = 0; k < this.NbColumns; k++)
                    {
                        value += this.Matrix[i, k] * m2[k, j];
                    }
                    m[i, j] = value;
                }
            }
            
            return m;
        }
        
        public static MatrixInt Multiply(MatrixInt matrixInt, int value)
        {
            MatrixInt newMatrix = new MatrixInt(matrixInt);

            newMatrix.Multiply(value);

            return newMatrix;
        }
        
        public static MatrixInt Multiply(MatrixInt m1, MatrixInt m2)
        {
            return m1.Multiply(m2);
        }
        
        public static MatrixInt operator *(MatrixInt matrixInt, int value)
        {
            return MatrixInt.Multiply(matrixInt, value);
        }
        
        public static MatrixInt operator *(MatrixInt m1, MatrixInt m2)
        {
            return m1.Multiply(m2);
        }
        
        public static MatrixInt operator *(int value, MatrixInt matrixInt)
        {
            return MatrixInt.Multiply(matrixInt, value);
        }
        
        public static MatrixInt operator -(MatrixInt matrixInt)
        {
            return MatrixInt.Multiply(matrixInt, -1);
        }

        public void Add(MatrixInt m2)
        {
            if (this.NbLines != m2.NbLines || this.NbColumns != m2.NbColumns)
            {
                throw new MatrixSumException("Matrix sizes do not match.");
            }
            
            for (int i = 0; i < this.NbLines; i++)
            {
                for (int j = 0; j < this.NbColumns; j++)
                {
                    this.Matrix[i, j] += m2[i, j];
                }
            }
        }

        public static MatrixInt Add(MatrixInt m1, MatrixInt m2)
        {
            MatrixInt newMatrix = new MatrixInt(m1);
            newMatrix.Add(m2);
            return newMatrix;
        }
        
        public static MatrixInt operator +(MatrixInt m1, MatrixInt m2)
        {
            return Add(m1, m2);
        }
        
        public static MatrixInt operator -(MatrixInt m1, MatrixInt m2)
        {
            return Add(m1, -m2);
        }
    }

public class MatrixSumException : Exception
{
    public MatrixSumException() {}

    public MatrixSumException(string message) : base(message) {}

    public MatrixSumException(string message, Exception inner) : base(message, inner) {}
}

public class MatrixMultiplyException : Exception
{
    public MatrixMultiplyException() {}

    public MatrixMultiplyException(string message) : base(message) {}

    public MatrixMultiplyException(string message, Exception inner) : base(message, inner) {}
}

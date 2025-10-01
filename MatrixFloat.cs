// ----- MANON WIMMER ----- //

namespace UnitTestMaths3DWimmer;

public class MatrixFloat
{
    public int NbLines;
    public int NbColumns;
    public float[,] Matrix;
    
    public MatrixFloat(int n, int m)
    {
        this.NbLines = n;
        this.NbColumns = m;
            
        this.Matrix = new float[this.NbLines, this.NbColumns];
    }
        
    public MatrixFloat(float[,] m)
    {
        this.NbLines = m.GetLength(0);
        this.NbColumns = m.GetLength(1);
            
        this.Matrix = m;
    }
        
    public MatrixFloat(MatrixFloat m)
    {
        this.NbLines = m.NbLines;
        this.NbColumns = m.NbColumns;

        float[,] copiedMatrix = new float[this.NbLines, this.NbColumns];
        for (int i = 0; i < this.NbLines; i++)
        {
            for (int j = 0; j < this.NbColumns; j++)
            {
                copiedMatrix[i, j] = m[i, j];
            }
        }
        
        this.Matrix = copiedMatrix;
    }
        
    public float this[int i, int j]
    {
        get => this.Matrix[i, j];
        set => this.Matrix[i, j] = value;
    }
    
    public float[,] ToArray2D()
    {
        return Matrix;
    }
    
    public static MatrixFloat Identity(int n)
    {
        MatrixFloat m = new MatrixFloat(n, n);

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
                    if ((i == j && this.Matrix[i, j] != 1.0f) || (i != j && this.Matrix[i, j] != 0))
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
        
        public MatrixFloat Multiply(MatrixFloat m2)
        {
            if (this.NbColumns != m2.NbLines)
            {
                throw new MatrixMultiplyException("Matrix sizes do not match for multiplication.");
            }
            
            MatrixFloat m = new MatrixFloat(this.NbLines, m2.NbColumns);
            
            for (int i = 0; i < this.NbLines; i++)
            {
                for (int j = 0; j < m2.NbColumns; j++)
                {
                    float value = 0f;
                    
                    for (int k = 0; k < this.NbColumns; k++)
                    {
                        value += this.Matrix[i, k] * m2[k, j];
                    }
                    m[i, j] = value;
                }
            }
            
            return m;
        }
        
        public static MatrixFloat Multiply(MatrixFloat matrixInt, int value)
        {
            MatrixFloat newMatrix = new MatrixFloat(matrixInt);

            newMatrix.Multiply(value);

            return newMatrix;
        }
        
        public static MatrixFloat Multiply(MatrixFloat m1, MatrixFloat m2)
        {
            return m1.Multiply(m2);
        }
        
        public static MatrixFloat operator *(MatrixFloat matrixFloat, int value)
        {
            return MatrixFloat.Multiply(matrixFloat, value);
        }
        
        public static MatrixFloat operator *(MatrixFloat m1, MatrixFloat m2)
        {
            return m1.Multiply(m2);
        }
        
        public static MatrixFloat operator *(int value, MatrixFloat matrixFloat)
        {
            return MatrixFloat.Multiply(matrixFloat, value);
        }
        
        public static MatrixFloat operator -(MatrixFloat matrixFloat)
        {
            return MatrixFloat.Multiply(matrixFloat, -1);
        }

        public void Add(MatrixFloat m2)
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

        public static MatrixFloat Add(MatrixFloat m1, MatrixFloat m2)
        {
            MatrixFloat newMatrix = new MatrixFloat(m1);
            newMatrix.Add(m2);
            return newMatrix;
        }
        
        public static MatrixFloat operator +(MatrixFloat m1, MatrixFloat m2)
        {
            return Add(m1, m2);
        }
        
        public static MatrixFloat operator -(MatrixFloat m1, MatrixFloat m2)
        {
            return Add(m1, -m2);
        }

        public MatrixFloat Transpose()
        {
            MatrixFloat newMatrix = new MatrixFloat(this.NbColumns, this.NbLines);

            for (int i = 0; i < this.NbLines; i++)
            {
                for (int j = 0; j < this.NbColumns; j++)
                {
                    newMatrix.Matrix[j, i] = this.Matrix[i, j];
                }
            }
            
            return newMatrix;
        }
        
        public static MatrixFloat Transpose(MatrixFloat matrixFloat)
        {
            return matrixFloat.Transpose();
        }
    
    public static MatrixFloat GenerateAugmentedMatrix(MatrixFloat m1, MatrixFloat m2)
    {
        if (m1.NbLines != m2.NbLines)
            throw new ArgumentException("M1 and M2 must have the same number of lines.");
    
        MatrixFloat augmentedMatrix = new MatrixFloat(m1.NbLines, m1.NbColumns + m2.NbColumns);
        
        for (int i = 0; i < m1.NbLines; i++)
        {
            for (int j = 0; j < m1.NbColumns; j++)
            {
                augmentedMatrix[i, j] = m1[i, j];
            }

            for (int j = 0; j < m2.NbColumns; j++)
            {
                augmentedMatrix[i, j + m1.NbColumns] = m2[i, j];
            }
        }

        return augmentedMatrix;
    }

    public (MatrixFloat m1, MatrixFloat m2) Split(int splitValue)
    {
        MatrixFloat m1 = new MatrixFloat(this.NbLines, splitValue + 1);
        MatrixFloat m2 = new MatrixFloat(this.NbLines, this.NbColumns - splitValue - 1);

        for (int i = 0; i < this.NbLines; i++)
        {
            for (int j = 0; j < this.NbColumns; j++)
            {
                if (j <= m1.NbColumns - 1)
                {
                    m1[i, j] = this.Matrix[i, j];
                }
                else
                {
                    m2[i, j  - m1.NbColumns] = this.Matrix[i, j];
                }
            }
        }
            
        return (m1, m2);
    }

    public MatrixFloat InvertByRowReduction()
    {
        if (this.NbLines != this.NbColumns)
        {
            throw new MatrixInvertException("Matrix must be square to invert.");
        }

        MatrixFloat identity = MatrixFloat.Identity(this.NbLines);
        
        var (left, right) = MatrixRowReductionAlgorithm.Apply(new MatrixFloat(this), identity);
        
        if (!left.IsIdentity())
        {
            throw new MatrixInvertException("Matrix is not invertible (singular matrix).");
        }

        return right;
    }
    
    public static MatrixFloat InvertByRowReduction(MatrixFloat matrixFloat)
    {
        return matrixFloat.InvertByRowReduction();
    }

    public MatrixFloat SubMatrix(int rowToRemove, int columnToRemove)
    {
        if (NbLines != NbColumns)
            throw new InvalidOperationException("SubMatrix is only defined for square matrices.");

        // New matrix with new size
        int newSize = NbLines - 1;
        MatrixFloat result = new MatrixFloat(newSize, newSize);

        int rowIndex = 0;

        // Fill matrix (expect row index == row to remove & same for col)
        for (int i = 0; i < NbLines; i++)
        {
            if (i == rowToRemove) continue;
            int colIndex = 0;

            for (int j = 0; j < NbColumns; j++)
            {
                if (j == columnToRemove) continue;

                result[rowIndex, colIndex] = this[i, j];
                colIndex++;
            }

            rowIndex++;
        }

        return result;
    }
    
    public static MatrixFloat SubMatrix(MatrixFloat matrixFloat, int rowToRemove, int colToRemove)
    {
        return matrixFloat.SubMatrix(rowToRemove, colToRemove);
    }

    public static float Determinant(MatrixFloat matrix)
    {
        int size = matrix.NbLines;
        
        if (matrix.NbLines != matrix.NbColumns)
            throw new InvalidOperationException("Determinant is only defined for square matrices.");

        // Matrix 1x1
        if (size == 1)
            return matrix[0, 0];

        // Matrix 2x2
        if (size == 2)
            return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

        // det(M) = somme sur j de [ (-1)^(j) * M[0][j] * det(M_0j) ] -> (-1)^(j) = signe
        float totalDeterminant = 0f;
        
        for (int j = 0; j < size; j++)
        {
            MatrixFloat sub = matrix.SubMatrix(0, j);
            float subDet = Determinant(sub);
            
            float sign = (j % 2 == 0) ? 1f : -1f;
            
            totalDeterminant += sign * matrix[0, j] * subDet;
        }

        return totalDeterminant;
    }

    public MatrixFloat Adjugate()
    {
        int size = NbLines;
        MatrixFloat result = new MatrixFloat(size, size);

        // Cᵢⱼ = (-1)^(i+j) × det(Mᵢⱼ)

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                MatrixFloat minor = SubMatrix(row, col);
                float det = Determinant(minor);
                
                float sign = ((row + col) % 2 == 0) ? 1f : -1f;
                
                result[col, row] = sign * det;
            }
        }

        return result; 
    }

    
    public static MatrixFloat Adjugate(MatrixFloat matrixFloat)
    {
        return matrixFloat.Adjugate();  
    }
}

public class MatrixInvertException : Exception
{
    public MatrixInvertException() {}

    public MatrixInvertException(string message) : base(message) {}

    public MatrixInvertException(string message, Exception inner) : base(message, inner) {}
}
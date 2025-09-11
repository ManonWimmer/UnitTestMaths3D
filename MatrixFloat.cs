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
        MatrixFloat augmentedMatrix = new MatrixFloat(m1.NbLines, m1.NbColumns + 1);
        int m2Count = 0;
            
        for (int i = 0; i < augmentedMatrix.NbLines; i++)
        {
            for (int j = 0; j < augmentedMatrix.NbColumns; j++)
            {
                if (j <= m1.NbColumns - 1)
                {
                    augmentedMatrix.Matrix[i, j] = m1[i, j];
                }
                else
                {
                    augmentedMatrix.Matrix[i, j] = m2[m2Count, 0];
                    m2Count++;
                }
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
}
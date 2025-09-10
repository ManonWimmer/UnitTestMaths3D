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
    }
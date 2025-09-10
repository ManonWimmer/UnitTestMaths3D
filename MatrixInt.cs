// ----- MANON WIMMER ----- //

namespace UnitTestMaths3DWimmer;

public struct MatrixInt
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
// ----- MANON WIMMER ----- //

using System.Diagnostics;

namespace UnitTestMaths3DWimmer;

public class MatrixRowReductionAlgorithm
{
    public static (MatrixFloat m1, MatrixFloat m2) Apply(MatrixFloat m1, MatrixFloat m2)
    {
        MatrixFloat augmentedMatrix = MatrixFloat.GenerateAugmentedMatrix(m1, m2);
        
        // i = 1 (se placer sur la première ligne de la matrice).
        int i = 0;
        
        // j = 1 (se placer sur la première colonne de la matrice).
        int j = 0;

        while (j < m1.NbColumns)
        {
            //Console.WriteLine($"i : {i}, j : {j}");
            
            // Trouver la ligne k où k >= i et M(kj) est la plus grande valeur.
            float biggestValue = 0;
            int biggestK = -1;
            for (int k = i; k < augmentedMatrix.NbLines; k++)
            {
                if (biggestValue < Math.Abs(augmentedMatrix[k, j]))
                {
                    biggestValue = Math.Abs(augmentedMatrix[k, j]);
                    if (k >= i)
                    {
                        biggestK = k;
                    }
                }
            }
        
            // Si toutes les valeurs sont nulles, passer directement à l’étape H.
            if (biggestValue == 0 || biggestK == -1)
            {
                j++;
                continue;
            }
            
            // Si k!=i échanger les lignes k et i.
            if (biggestK != -1)
            {
                //Console.WriteLine($"Swap Lines i {i}, k {biggestK}");
                MatrixElementaryOperations.SwapLines(augmentedMatrix, i, biggestK);
            }
            
            // Multiplier la ligne i par 1/M(ij).
            MatrixElementaryOperations.MultiplyLine(augmentedMatrix, i, (1/augmentedMatrix[i, j]));
            
            // Pour chaque ligne r où i!=k. Ajouter -M(rj)xL(i) à L(r).
            for (int r = 0; r < augmentedMatrix.NbLines; r++)
            {
                if (r != i)  
                {
                    // dans la boucle, soustraire m1[r, j] * ligne i à la ligne r.
                    float factor = -augmentedMatrix[r, j];  
                    MatrixElementaryOperations.AddMultipleOfLine(augmentedMatrix, i, r, factor);
                }
            }
            
            // Incrémenter i
            i++;
            
            //Incrémenter j. Retourner à l’étape C s’il reste des colonnes à traiter.
            j++;
        
            //PrintMatrix(augmentedMatrix);
            
        }
        
        return (augmentedMatrix.Split(m1.NbColumns - 1));
    }

    private static void PrintMatrix(MatrixFloat matrix)
    {
        for (int y = 0; y < matrix.NbLines; y++)
        {
            string toPrint = $"{y} : ";
            for (int u = 0; u < matrix.NbColumns; u++)
            {
                toPrint += $"{matrix[y, u]} | ";
            }

            Console.WriteLine(toPrint);
        }
        
        Console.WriteLine("");
    }
}
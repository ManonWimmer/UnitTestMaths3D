// ----- MANON WIMMER ----- //

using System.Diagnostics;
using System.Numerics;

namespace UnitTestMaths3DWimmer;

public class Vector4
{
    public float x;
    public float y;
    public float z;
    public float w;

    public Vector4(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
    
    public Vector4(Vector4 vector)
    {
        this.x = vector.x;
        this.y = vector.y;
        this.z = vector.z;
        this.w = vector.w;
    }
    
    
    public static Vector4 operator *(MatrixFloat matrix, Vector4 vector4)
    {
        MatrixFloat vMatrix = new MatrixFloat(4, 1);
        vMatrix[0, 0] = vector4.x;
        vMatrix[1, 0] = vector4.y;
        vMatrix[2, 0] = vector4.z;
        vMatrix[3, 0] = vector4.w;
        
        MatrixFloat multipliedMatrix = matrix * vMatrix;

        Vector4 resultVector = new Vector4(vector4); // w remis
        resultVector.x = multipliedMatrix[0, 0];
        resultVector.y = multipliedMatrix[1, 0];
        resultVector.z = multipliedMatrix[2, 0];
        
        return resultVector; 
    }

    public static Vector4 operator *(Vector4 vector4, MatrixFloat matrix)
    {
        return matrix * vector4;
    }
}
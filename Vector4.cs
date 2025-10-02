// ----- MANON WIMMER ----- //
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
    
    public Vector4(MatrixFloat matrix)
    {
        this.x = matrix[0, 0];
        this.y = matrix[1, 0];
        this.z = matrix[2, 0];
        this.w = 1f;
    }
    
    
    public static Vector4 operator *(MatrixFloat matrix, Vector4 vector4)
    {
        MatrixFloat vMatrix = new MatrixFloat(vector4);
        
        MatrixFloat multipliedMatrix = matrix * vMatrix;
        
        Vector4 resultVector = new Vector4(multipliedMatrix);
        resultVector.w = vector4.w;
        
        return resultVector; 
    }

    public static Vector4 operator *(Vector4 vector4, MatrixFloat matrix)
    {
        return matrix * vector4;
    }
    
    public Vector4(Vector3 vector3, float w = 0f)
    {
        this.x = vector3.x;
        this.y = vector3.y;
        this.z = vector3.z;
        this.w = w;
    }
    
    public static implicit operator Vector4(Vector3 v)
    {
        return new Vector4(v, 1f); 
    }
}
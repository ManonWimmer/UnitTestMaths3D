// ----- MANON WIMMER ----- //
namespace UnitTestMaths3DWimmer;

public class Vector3
{
    public float x;
    public float y;
    public float z;

    public Vector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    
    public static implicit operator Vector3(Vector4 vector4)
    {
        return new Vector3(vector4.x, vector4.y, vector4.z);
    }
}
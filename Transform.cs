// ----- MANON WIMMER ----- //
using UnitTestMaths3DWimmer;
// ReSharper disable All

public class Transform
{
    public Vector4 LocalPosition
    {
        get => _localPosition;
        set
        {
            _localPosition = value;
            RecalculateMatrix();
        }
    }
    
    private Vector4 _localPosition;
   
    public MatrixFloat LocalTranslationMatrix;
    
    public Transform(float x, float y, float z, float w)
    {
        this._localPosition = new Vector4(x, y, z, w);
        RecalculateMatrix();
    }

    public Transform()
    {
        this._localPosition = new Vector4(0f, 0f, 0f, 1f);
        RecalculateMatrix();
    }

    public void RecalculateMatrix()
    {
        this.LocalTranslationMatrix = MatrixFloat.Identity(4);
        this.LocalTranslationMatrix[0, 3] = this._localPosition.x;
        this.LocalTranslationMatrix[1, 3] = this._localPosition.y;
        this.LocalTranslationMatrix[2, 3] = this._localPosition.z;
        this.LocalTranslationMatrix[3, 3] = this._localPosition.w;
    }
}
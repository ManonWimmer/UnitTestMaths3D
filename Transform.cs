// ----- MANON WIMMER ----- //
using UnitTestMaths3DWimmer;
// ReSharper disable All

public class Transform
{
    public Transform(float x, float y, float z, float w)
    {
        this._localPosition = new Vector4(x, y, z, w);
        RecalculateLocalTranslationMatrix();

        this._localRotation = new Vector4(0, 0, 0, 1);
        InitLocalRotationMatrix();

        this._localScale = new Vector4(1, 1, 1, 0);
        InitLocalScaleMatrix();

        InitWorldPosition();

        InitLocalToWorldMatrix();
        InitWorldToLocalMatrix();
    }

    public Transform()
    {
        this._localPosition = new Vector4(0f, 0f, 0f, 1f);
        RecalculateLocalTranslationMatrix();

        this._localRotation = new Vector4(0, 0, 0, 1);
        InitLocalRotationMatrix();

        this._localScale = new Vector4(1, 1, 1, 0);
        InitLocalScaleMatrix();

        InitWorldPosition();

        InitLocalToWorldMatrix();
        InitWorldToLocalMatrix();
    }

    // ----- LOCAL POSITION ----- //
    public Vector4 LocalPosition
    {
        get => _localPosition;
        set
        {
            _localPosition = value;
            RecalculateLocalTranslationMatrix();
        }
    }
    
    private Vector4 _localPosition;
    // ----- LOCAL POSITION ----- //

    // ----- LOCAL ROTATION ----- //
    public Vector4 LocalRotation
    {
        get => _localRotation;
        set
        {
            _localRotation = value;
            RecalculateLocalRotationMatrix();
        }
    }
    
    private Vector4 _localRotation;
    
    public MatrixFloat LocalTranslationMatrix;
    
    public MatrixFloat LocalRotationMatrix;
    public MatrixFloat LocalRotationXMatrix;
    public MatrixFloat LocalRotationYMatrix;
    public MatrixFloat LocalRotationZMatrix;

    public Quaternion LocalRotationQuaternion
    {
        get
        {
            return Quaternion.Euler(LocalRotation.x, LocalRotation.y, LocalRotation.z);
        }
        set
        {
            Vector3 eulerAngles = value.EulerAngles;
            LocalRotation = new Vector4(eulerAngles.x, eulerAngles.y, eulerAngles.z, 1f);
            RecalculateLocalRotationMatrix();
        }
    }
    // ----- LOCAL ROTATION ----- //
    
    // ----- LOCAL SCALE ----- //
    public Vector4 LocalScale
    {
        get => _localScale;
        set
        {
            _localScale = value;
            RecalculateLocalScaleMatrix();
        }
    }
    
    private Vector4 _localScale;
    
    public MatrixFloat LocalScaleMatrix;
    // ----- LOCAL SCALE ----- //
    
    // ----- LOCAL TO WORLD ----- //
    public MatrixFloat LocalToWorldMatrix
    {
        get 
        {
            RecalculateLocalToWorldMatrix();
            return _localToWorldMatrix;
        }
        set
        {
            _localToWorldMatrix = value;
        }
    }

    private MatrixFloat _localToWorldMatrix;
    // ----- LOCAL TO WORLD ----- //
    
    // ----- WORLD TO LOCAL ----- //
    public MatrixFloat WorldToLocalMatrix
    {
        get 
        {
            RecalculateWorldToLocalMatrix();
            return _worldToLocalMatrix;
        }
        set
        {
            _worldToLocalMatrix = value;
        }
    }

    private MatrixFloat _worldToLocalMatrix;
    // ----- WORLD TO LOCAL ----- //

    // ----- WORLD POSITION ----- //
    public Vector4 WorldPosition
    {
        get 
        {
            RecalculateWorldPosition();
            return _worldPosition;
        }
        set
        {
            _worldPosition = value;

            if (_parent == null)
            {
                // Local = World
                LocalPosition = _worldPosition;
            }
            else
            {
                // Local = Parent to local
                Vector4 world = new Vector4(value.x, value.y, value.z, 1f);

                MatrixFloat parentWorldToLocal = Parent.WorldToLocalMatrix;
                Vector4 local = parentWorldToLocal * world;

                LocalPosition = new Vector4(local.x, local.y, local.z, 1f);
            }
        }
    }

    private Vector4 _worldPosition;

    public void InitWorldPosition()
    {
        _worldPosition = new Vector4(0f, 0f, 0f, 1f);
    }

    public void RecalculateWorldPosition()
    {
        _worldPosition.x = _localToWorldMatrix[0, 3];
        _worldPosition.y = _localToWorldMatrix[1, 3];
        _worldPosition.z = _localToWorldMatrix[2, 3];
        _worldPosition.w = _localToWorldMatrix[3, 3];
    }
    // ----- WORLD POSITION ----- //

    // ----- PARENT ----- //
    public Transform Parent
    {
        get => _parent;
        set
        {
            _parent = value;
        }
    }

    private Transform _parent;
    public void SetParent(Transform tParent)
    {
        _parent = tParent;
    }
    // ----- PARENT ----- //

    // ----- MATRIX ----- //
    public void RecalculateLocalTranslationMatrix()
    {
        this.LocalTranslationMatrix = MatrixFloat.Identity(4);
        this.LocalTranslationMatrix[0, 3] = this._localPosition.x;
        this.LocalTranslationMatrix[1, 3] = this._localPosition.y;
        this.LocalTranslationMatrix[2, 3] = this._localPosition.z;
        this.LocalTranslationMatrix[3, 3] = this._localPosition.w;
    }

    public void InitLocalRotationMatrix()
    {
        this.LocalRotationMatrix = MatrixFloat.Identity(4);
        this.LocalRotationXMatrix = MatrixFloat.Identity(4);
        this.LocalRotationYMatrix = MatrixFloat.Identity(4);
        this.LocalRotationZMatrix = MatrixFloat.Identity(4);
    }
    
    public void RecalculateLocalRotationMatrix()
    {
        InitLocalRotationMatrix();
        
        // Rotation X
        float radianX = MathF.PI / 180f * _localRotation.x;
        if (radianX != 0)
        {
            LocalRotationXMatrix[1, 1] = MathF.Cos(radianX); 
            LocalRotationXMatrix[1, 2] = -MathF.Sin(radianX);
            LocalRotationXMatrix[2, 1] = MathF.Sin(radianX);
            LocalRotationXMatrix[2, 2] = MathF.Cos(radianX);
        }
        
        // Rotation Y
        float radianY = MathF.PI / 180f * _localRotation.y;
        if (radianY != 0)
        {
            LocalRotationYMatrix[0, 0] = MathF.Cos(radianY); 
            LocalRotationYMatrix[0, 2] = MathF.Sin(radianY);
            LocalRotationYMatrix[2, 0] = -MathF.Sin(radianY);
            LocalRotationYMatrix[2, 2] = MathF.Cos(radianY);
        }

        // Rotation Z
        float radianZ = MathF.PI / 180f * _localRotation.z;
        if (radianZ != 0)
        {
            LocalRotationZMatrix[0, 0] = MathF.Cos(radianZ); 
            LocalRotationZMatrix[0, 1] = -MathF.Sin(radianZ);
            LocalRotationZMatrix[1, 0] = MathF.Sin(radianZ);
            LocalRotationZMatrix[1, 1] = MathF.Cos(radianZ);
        }
        
        LocalRotationMatrix = LocalRotationYMatrix * LocalRotationXMatrix * LocalRotationZMatrix;
    }
    
    public void InitLocalScaleMatrix()
    {
        this.LocalScaleMatrix = MatrixFloat.Identity(4);
    }

    public void RecalculateLocalScaleMatrix()
    {
        InitLocalScaleMatrix();
        
        // Scale X
        if (_localScale.x != 1)
        {
            LocalScaleMatrix[0,0] = _localScale.x;
        }
        
        // Scale Y
        if (_localScale.y != 1)
        {
            LocalScaleMatrix[1, 1] = _localScale.y;
        }
        
        // Scale Z
        if (_localScale.z != 1)
        {
            LocalScaleMatrix[2, 2] = _localScale.z;
        }
    }

    public void InitLocalToWorldMatrix()
    {
        _localToWorldMatrix = MatrixFloat.Identity(4);
    }

    public void RecalculateLocalToWorldMatrix()
    {
        MatrixFloat localMatrix = LocalTranslationMatrix * LocalRotationMatrix * LocalScaleMatrix;
        
        if (_parent != null)
        {
            _localToWorldMatrix = _parent.LocalToWorldMatrix * localMatrix;
        }
        else
        {
            _localToWorldMatrix = localMatrix;
        }
    }
    
    public void InitWorldToLocalMatrix()
    {
        _worldToLocalMatrix = MatrixFloat.Identity(4);
    }
    
    public void RecalculateWorldToLocalMatrix()
    {
        _worldToLocalMatrix = MatrixFloat.InvertByDeterminant(LocalToWorldMatrix);

        Console.WriteLine($"WorldToLocalMatrix 0,0 : {_worldToLocalMatrix[0,0]}");
    }
    // ----- MATRIX ----- //
}
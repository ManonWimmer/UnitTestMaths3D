// ----- MANON WIMMER ----- //
using UnitTestMaths3DWimmer;
// ReSharper disable All

public struct Quaternion // Struct pour copie au lieu de ref quand q1 = q2
{
    public float x;
    public float y;
    public float z;
    public float w;

    public Quaternion(float x, float y, float z, float w)    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public static Quaternion Identity => new Quaternion(0f, 0f, 0f, 1f);

    public static Quaternion AngleAxis(float angle, Vector3 axis)
    {
        // q = cos(angle/2) + (axis.x * sin(angle/2), axis.y * sin(angle/2), axis.z * sin(angle/2)

        // Normaliser axe
        float length = MathF.Sqrt(axis.x * axis.x + axis.y * axis.y + axis.z * axis.z);
        if (length == 0f) throw new ArgumentException("Axis must be non-zero");

        float normalizedX = axis.x / length;
        float normalizedY = axis.y / length;
        float normalizedZ = axis.z / length;

        float radians = angle * MathF.PI / 180f;
        float halfSin = MathF.Sin(radians / 2);
        float halfCos = MathF.Cos(radians / 2);

        return new Quaternion(
            normalizedX * halfSin,
            normalizedY * halfSin,
            normalizedZ * halfSin,
            halfCos
            );
    }

    public static Quaternion operator *(Quaternion q1, Quaternion q2)
    {
        // q.x = w1 * w2 + x1 * w2 + y1 * z2 - z1 * y2
        // q.y = w1 * y2 - x1 * z2 + y1 * w2 + z1 * x2
        // y.z = w1 * z2 + x1 * y2 - y1 * x2 + z1 * w2
        // q.w = w1 * w2 - x1 * x2 - y1 * y2 - z1 * z2

        return new Quaternion(
            q1.w * q2.x + q1.x * q2.w + q1.y * q2.z - q1.z * q2.y,
            q1.w * q2.y - q1.x * q2.z + q1.y * q2.w + q1.z * q2.x,
            q1.w * q2.z + q1.x * q2.y - q1.y * q2.x + q1.z * q2.w,
            q1.w * q2.w - q1.x * q2.x - q1.y * q2.y - q1.z * q2.z
            );
    }

    public Quaternion Conjugate()
    {
        // q^-1 = -x, -y, -z, w
        return new Quaternion(-x, -y, -z, w);
    }

    public Vector3 Rotate(Vector3 point)
    {
        // v' = q * p * q^-1

        Quaternion p = new Quaternion(point.x, point.y, point.z, 0f);

        Quaternion result = this * p * this.Conjugate();

        return new Vector3(result.x, result.y, result.z);
    }

    public static Vector3 operator *(Quaternion q, Vector3 v)
    {
        return q.Rotate(v);
    }

    public MatrixFloat Matrix
    {
        get
        {
            float[,] m = new float[4, 4];

            // | 1 - 2y^2 - 2z^2 | 2xy - 2wz | 2xz + 2wy |
            m[0, 0] = 1 - 2 * y * y - 2 * z * z;
            m[0, 1] = 2 * x * y - 2 * w * z;
            m[0, 2] = 2 * x * z + 2 * w * y;
            m[0, 3] = 0;

            // | 2xy + 2wz | 1 - 2x^2 - 2z^2 | 2yz - 2wx |
            m[1, 0] = 2 * x* y + 2 * w * z;
            m[1, 1] = 1 - 2 * x * x - 2 * z * z;
            m[1, 2] = 2 * y * z - 2 * w * x;
            m[1, 3] = 0;

            // | 2xz - 2wy | 2yz + 2wx | 1 - 2x^2 - 2y^2 |
            m[2, 0] = 2 * x * z - 2 * w * y;
            m[2, 1] = 2 * y * z + 2 * w * x;
            m[2, 2] = 1 - 2 * x * x - 2 * y * y;
            m[2, 3] = 0;

            m[3, 0] = 0;
            m[3, 1] = 0;
            m[3, 2] = 0;
            m[3, 3] = 1;

            return new MatrixFloat(m);
        }
    }

    public static Quaternion Euler(float xDegrees, float yDegrees, float zDegrees)
    {
        // q = qRY * qRX * qRZ
        Quaternion qRX = AngleAxis(xDegrees, new Vector3(1, 0, 0));
        Quaternion qRY = AngleAxis(yDegrees, new Vector3(0, 1, 0));
        Quaternion qRZ = AngleAxis(zDegrees, new Vector3(0, 0, 1));

        return qRY * qRX * qRZ;
    }


    public Vector3 EulerAngles
    {
        get
        {
            float m11 = 1 - 2 * y * y - 2 * z * z;
            float m12 = 2 * x * y - 2 * w * z;
            float m13 = 2 * x * z + 2 * w * y;

            float m21 = 2 * x * y + 2 * w * z;
            float m22 = 1 - 2 * x * x - 2 * z * z;
            float m23 = 2 * y * z - 2 * w * x;

            float m31 = 2 * x * z - 2 * w * y;
            float m32 = 2 * y * z + 2 * w * x;
            float m33 = 1 - 2 * x * x - 2 * y * y;

            float pitch = MathF.Asin(Math.Clamp(-m23, -1, 1));
            float heading;
            float bank;

            if (MathF.Abs(MathF.Cos(pitch)) > 1e-6f)
            {
                heading = MathF.Atan2(m13, m33);
                bank = MathF.Atan2(m21, m22);
            }
            else
            {
                heading = MathF.Atan2(-m31, m11);
                bank = 0f;
            }

            const float Rad2Deg = 180f / MathF.PI;
            return new Vector3(pitch * Rad2Deg, heading * Rad2Deg, bank * Rad2Deg);
        }
    }

}
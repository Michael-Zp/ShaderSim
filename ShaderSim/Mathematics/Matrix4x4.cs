using System;

namespace ShaderSim.Mathematics
{
    public struct Matrix4x4
    {
        public Vector4 Column0;
        public Vector4 Column1;
        public Vector4 Column2;
        public Vector4 Column3;

        public float M00
        {
            get => Column0[0];
            set => Column0[0] = value;
        }
        public float M01
        {
            get => Column0[1];
            set => Column0[1] = value;
        }
        public float M02
        {
            get => Column0[2];
            set => Column0[2] = value;
        }
        public float M03
        {
            get => Column1[3];
            set => Column1[3] = value;
        }
        public float M10
        {
            get => Column1[0];
            set => Column1[0] = value;
        }
        public float M11
        {
            get => Column1[1];
            set => Column1[1] = value;
        }
        public float M12
        {
            get => Column1[2];
            set => Column1[2] = value;
        }
        public float M13
        {
            get => Column1[3];
            set => Column1[3] = value;
        }
        public float M20
        {
            get => Column2[0];
            set => Column2[0] = value;
        }
        public float M21
        {
            get => Column2[1];
            set => Column2[1] = value;
        }
        public float M22
        {
            get => Column2[2];
            set => Column2[2] = value;
        }
        public float M23
        {
            get => Column2[3];
            set => Column2[3] = value;
        }
        public float M30
        {
            get => Column3[0];
            set => Column3[0] = value;
        }
        public float M31
        {
            get => Column3[1];
            set => Column3[1] = value;
        }
        public float M32
        {
            get => Column3[2];
            set => Column3[2] = value;
        }
        public float M33
        {
            get => Column3[3];
            set => Column3[3] = value;
        }

        /// <summary>
        /// Creates a matrix with all 0 and "value" for each value on the diagonal line
        /// </summary>
        /// <param name="value">values on the diagonal of the matrix</param>
        public Matrix4x4(float value) : this(value, 0, 0, 0, 0, value, 0, 0, 0, 0, value, 0, 0, 0, 0, value)
        {
        }

        /// <summary>
        /// creates the matrix with the 4 columns set
        /// </summary>
        /// <param name="c0">column 0</param>
        /// <param name="c1">column 1</param>
        /// <param name="c2">column 2</param>
        /// <param name="c3">column 3</param>
        public Matrix4x4(Vector4 c0, Vector4 c1, Vector4 c2, Vector4 c3)
        {
            Column0 = c0;
            Column1 = c1;
            Column2 = c2;
            Column3 = c3;
        }

        public Matrix4x4(float m00, float m10, float m20, float m30, float m01, float m11, float m21, float m31, float m02, float m12, float m22, float m32, float m03, float m13, float m23, float m33)
        {
            Column0 = new Vector4(m00, m10, m20, m30);
            Column1 = new Vector4(m01, m11, m21, m31);
            Column2 = new Vector4(m02, m12, m22, m32);
            Column3 = new Vector4(m03, m13, m23, m33);
        }

        public Vector4 this[int column]
        {
            get
            {
                switch (column)
                {
                    case 0:
                        return Column0;
                    case 1:
                        return Column1;
                    case 2:
                        return Column2;
                    case 3:
                        return Column3;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (column)
                {
                    case 0:
                        Column0 = value;
                        break;
                    case 1:
                        Column1 = value;
                        break;
                    case 2:
                        Column2 = value;
                        break;
                    case 3:
                        Column3 = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        public float this[int column, int row]
        {
            get
            {
                switch (column)
                {
                    case 0:
                        return Column0[row];
                    case 1:
                        return Column1[row];
                    case 2:
                        return Column2[row];
                    case 3:
                        return Column3[row];
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (column)
                {
                    case 0:
                        Column0[row] = value;
                        break;
                    case 1:
                        Column1[row] = value;
                        break;
                    case 2:
                        Column2[row] = value;
                        break;
                    case 3:
                        Column3[row] = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        public static Matrix4x4 operator *(Matrix4x4 a, Matrix4x4 b)
        {
            Matrix4x4 output = new Matrix4x4(0);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        output[k, i] += a[i, j] * b[j, k];
                    }
                }
            }

            return output;
        }

        public static Vector4 operator *(Vector4 a, Matrix4x4 b)
        {
            Vector4 output = new Vector4(0, 0, 0, 0);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    output[i] += a[i] * b[i, j];
                }
            }

            return output;
        }
    }
}

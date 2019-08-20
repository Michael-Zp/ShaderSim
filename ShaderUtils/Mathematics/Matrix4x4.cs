using System;
using ShaderUtils.Attributes;

namespace ShaderUtils.Mathematics
{
    [Translation("mat4")]
    public struct Matrix4x4
    {
        public Vector4 Column0;
        public Vector4 Column1;
        public Vector4 Column2;
        public Vector4 Column3;

        public float M11
        {
            get => Column0[0];
            set => Column0[0] = value;
        }
        public float M12
        {
            get => Column0[1];
            set => Column0[1] = value;
        }
        public float M13
        {
            get => Column0[2];
            set => Column0[2] = value;
        }
        public float M14
        {
            get => Column0[3];
            set => Column0[3] = value;
        }
        public float M21
        {
            get => Column1[0];
            set => Column1[0] = value;
        }
        public float M22
        {
            get => Column1[1];
            set => Column1[1] = value;
        }
        public float M23
        {
            get => Column1[2];
            set => Column1[2] = value;
        }
        public float M24
        {
            get => Column1[3];
            set => Column1[3] = value;
        }
        public float M31
        {
            get => Column2[0];
            set => Column2[0] = value;
        }
        public float M32
        {
            get => Column2[1];
            set => Column2[1] = value;
        }
        public float M33
        {
            get => Column2[2];
            set => Column2[2] = value;
        }
        public float M34
        {
            get => Column2[3];
            set => Column2[3] = value;
        }
        public float M41
        {
            get => Column3[0];
            set => Column3[0] = value;
        }
        public float M42
        {
            get => Column3[1];
            set => Column3[1] = value;
        }
        public float M43
        {
            get => Column3[2];
            set => Column3[2] = value;
        }
        public float M44
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
                        output[i, j] += a[i, k] * b[k, j];
                    }
                }
            }

            return output;
        }

        public static Vector4 operator *(Matrix4x4 a, Vector4 b)
        {
            Vector4 output = new Vector4(0, 0, 0, 0);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    output[j] += b[i] * a[j, i];
                }
            }

            return output;
        }

        public static implicit operator System.Numerics.Matrix4x4(Matrix4x4 mat)
        {
            return new System.Numerics.Matrix4x4(mat.M11, mat.M12, mat.M13, mat.M14, mat.M21, mat.M22, mat.M23, mat.M24, mat.M31, mat.M32, mat.M33, mat.M34, mat.M41, mat.M42, mat.M43, mat.M44);
        }

        public static implicit operator Matrix4x4(System.Numerics.Matrix4x4 mat)
        {
            Matrix4x4 result = new Matrix4x4(mat.M11, mat.M12, mat.M13, mat.M14, mat.M21, mat.M22, mat.M23, mat.M24, mat.M31, mat.M32, mat.M33, mat.M34, mat.M41, mat.M42, mat.M43, mat.M44);
            return result;
        }

        public float[] ToArray()
        {
            int num1 = 0;
            float[] numArray = new float[16];
            int index1 = num1;
            int num2 = index1 + 1;
            numArray[index1] = M11;
            int index2 = num2;
            int num3 = index2 + 1;
            numArray[index2] = M21;
            int index3 = num3;
            int num4 = index3 + 1;
            numArray[index3] = M31;
            int index4 = num4;
            int num5 = index4 + 1;
            numArray[index4] = M41;
            int index5 = num5;
            int num6 = index5 + 1;
            numArray[index5] = M12;
            int index6 = num6;
            int num7 = index6 + 1;
            numArray[index6] = M22;
            int index7 = num7;
            int num8 = index7 + 1;
            numArray[index7] = M32;
            int index8 = num8;
            int num9 = index8 + 1;
            numArray[index8] = M42;
            int index9 = num9;
            int num10 = index9 + 1;
            numArray[index9] = M13;
            int index10 = num10;
            int num11 = index10 + 1;
            numArray[index10] = M23;
            int index11 = num11;
            int num12 = index11 + 1;
            numArray[index11] = M33;
            int index12 = num12;
            int num13 = index12 + 1;
            numArray[index12] = M43;
            int index13 = num13;
            int num14 = index13 + 1;
            numArray[index13] = M14;
            int index14 = num14;
            int num15 = index14 + 1;
            numArray[index14] = M24;
            int index15 = num15;
            int num16 = index15 + 1;
            numArray[index15] = M34;
            int index16 = num16;
            int num17 = index16 + 1;
            numArray[index16] = M44;
            return numArray;
        }
    }
}

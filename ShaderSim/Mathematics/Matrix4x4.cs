using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

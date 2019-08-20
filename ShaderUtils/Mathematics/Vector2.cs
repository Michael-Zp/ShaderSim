using System;
using ShaderUtils.Attributes;

namespace ShaderUtils.Mathematics
{
    [Translation("vec2")]
    public struct Vector2
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return X;
                    case 1:
                        return Y;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        public float R
        {
            get { return X; }
            set { X = value; }
        }

        public float G
        {
            get { return Y; }
            set { Y = value; }
        }

        public Vector2 XX => new Vector2(X);
        public Vector2 YY => new Vector2(Y);
        public Vector2 XY
        {
            get { return new Vector2(X, Y); }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        public Vector2 YX
        {
            get { return new Vector2(Y, X); }
            set
            {
                Y = value.X;
                X = value.Y;
            }
        }

        public Vector2 RR => new Vector2(X);
        public Vector2 GG => new Vector2(Y);
        public Vector2 RG
        {
            get { return new Vector2(X, Y); }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        public Vector2 GR
        {
            get { return new Vector2(Y, X); }
            set
            {
                Y = value.X;
                X = value.Y;
            }
        }

        public Vector2(float value)
        {
            X = value;
            Y = value;
        }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2()
            {
                X = a.X + b.X,
                Y = a.Y + b.Y,
            };
        }
        public static Vector2 operator +(Vector2 a, int b)
        {
            return new Vector2()
            {
                X = (float)(a.X + b),
                Y = (float)(a.Y + b),
            };
        }

        public static Vector2 operator +(int a, Vector2 b)
        {
            return new Vector2()
            {
                X = (float)(a + b.X),
                Y = (float)(a + b.Y),
            };
        }
        public static Vector2 operator +(Vector2 a, float b)
        {
            return new Vector2()
            {
                X = (float)(a.X + b),
                Y = (float)(a.Y + b),
            };
        }

        public static Vector2 operator +(float a, Vector2 b)
        {
            return new Vector2()
            {
                X = (a + b.X),
                Y = (a + b.Y),
            };
        }
        public static Vector2 operator +(Vector2 a, double b)
        {
            return new Vector2()
            {
                X = (float)(a.X + b),
                Y = (float)(a.Y + b),
            };
        }

        public static Vector2 operator +(double a, Vector2 b)
        {
            return new Vector2()
            {
                X = (float)(a + b.X),
                Y = (float)(a + b.Y),
            };
        }
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2()
            {
                X = a.X - b.X,
                Y = a.Y - b.Y,
            };
        }
        public static Vector2 operator -(Vector2 a, int b)
        {
            return new Vector2()
            {
                X = (float)(a.X - b),
                Y = (float)(a.Y - b),
            };
        }

        public static Vector2 operator -(int a, Vector2 b)
        {
            return new Vector2()
            {
                X = (float)(a - b.X),
                Y = (float)(a - b.Y),
            };
        }

        public static Vector2 operator -(Vector2 a, float b)
        {
            return new Vector2()
            {
                X = (float)(a.X - b),
                Y = (float)(a.Y - b),
            };
        }

        public static Vector2 operator -(float a, Vector2 b)
        {
            return new Vector2()
            {
                X = (a - b.X),
                Y = (a - b.Y),
            };
        }
        public static Vector2 operator -(Vector2 a, double b)
        {
            return new Vector2()
            {
                X = (float)(a.X - b),
                Y = (float)(a.Y - b),
            };
        }

        public static Vector2 operator -(double a, Vector2 b)
        {
            return new Vector2()
            {
                X = (float)(a - b.X),
                Y = (float)(a - b.Y),
            };
        }
        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return new Vector2()
            {
                X = a.X * b.X,
                Y = a.Y * b.Y,
            };
        }
        public static Vector2 operator *(Vector2 a, int b)
        {
            return new Vector2()
            {
                X = (float)(a.X * b),
                Y = (float)(a.Y * b),
            };
        }

        public static Vector2 operator *(int a, Vector2 b)
        {
            return new Vector2()
            {
                X = (float)(a * b.X),
                Y = (float)(a * b.Y),
            };
        }
        public static Vector2 operator *(Vector2 a, float b)
        {
            return new Vector2()
            {
                X = (float)(a.X * b),
                Y = (float)(a.Y * b),
            };
        }

        public static Vector2 operator *(float a, Vector2 b)
        {
            return new Vector2()
            {
                X = (a * b.X),
                Y = (a * b.Y),
            };
        }
        public static Vector2 operator *(Vector2 a, double b)
        {
            return new Vector2()
            {
                X = (float)(a.X * b),
                Y = (float)(a.Y * b),
            };
        }

        public static Vector2 operator *(double a, Vector2 b)
        {
            return new Vector2()
            {
                X = (float)(a * b.X),
                Y = (float)(a * b.Y),
            };
        }
        public static Vector2 operator /(Vector2 a, Vector2 b)
        {
            return new Vector2()
            {
                X = a.X / b.X,
                Y = a.Y / b.Y,
            };
        }
        public static Vector2 operator /(Vector2 a, int b)
        {
            return new Vector2()
            {
                X = (float)(a.X / b),
                Y = (float)(a.Y / b),
            };
        }

        public static Vector2 operator /(int a, Vector2 b)
        {
            return new Vector2()
            {
                X = (float)(a / b.X),
                Y = (float)(a / b.Y),
            };
        }
        public static Vector2 operator /(Vector2 a, float b)
        {
            return new Vector2()
            {
                X = (float)(a.X / b),
                Y = (float)(a.Y / b),
            };
        }

        public static Vector2 operator /(float a, Vector2 b)
        {
            return new Vector2()
            {
                X = (a / b.X),
                Y = (a / b.Y),
            };
        }
        public static Vector2 operator /(Vector2 a, double b)
        {
            return new Vector2()
            {
                X = (float)(a.X / b),
                Y = (float)(a.Y / b),
            };
        }

        public static Vector2 operator /(double a, Vector2 b)
        {
            return new Vector2()
            {
                X = (float)(a / b.X),
                Y = (float)(a / b.Y),
            };
        }

        public static Vector2 operator -(Vector2 a)
        {
            return new Vector2()
            {
                X = -a.X,
                Y = -a.Y,
            };
        }

        public static implicit operator System.Numerics.Vector2(Vector2 vec)
        {
            return new System.Numerics.Vector2(vec.X, vec.Y);
        }

        public static implicit operator Vector2(System.Numerics.Vector2 vec)
        {
            return new Vector2(vec.X, vec.Y);
        }
    }


}



using System;

namespace ShaderSim.Mathematics
{
    public struct Vector3
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

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
                    case 2:
                        return Z;
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
                    case 2:
                        Z = value;
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

        public float B
        {
            get { return Z; }
            set { Z = value; }
        }

        public Vector2 XX => new Vector2(X);
        public Vector2 YY => new Vector2(Y);
        public Vector2 ZZ => new Vector2(Z);

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
        public Vector2 XZ
        {
            get { return new Vector2(X, Z); }
            set
            {
                X = value.X;
                Z = value.Y;
            }
        }
        public Vector2 ZX
        {
            get { return new Vector2(Z, X); }
            set
            {
                Z = value.X;
                X = value.Y;
            }
        }
        public Vector2 YZ
        {
            get { return new Vector2(Y, Z); }
            set
            {
                Y = value.X;
                Z = value.Y;
            }
        }
        public Vector2 ZY
        {
            get { return new Vector2(Z, Y); }
            set
            {
                Z = value.X;
                Y = value.Y;
            }
        }

        public Vector2 RR => new Vector2(X);
        public Vector2 GG => new Vector2(Y);
        public Vector2 BB => new Vector2(Z);
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
        public Vector2 RB
        {
            get { return new Vector2(X, Z); }
            set
            {
                X = value.X;
                Z = value.Y;
            }
        }
        public Vector2 BR
        {
            get { return new Vector2(Z, X); }
            set
            {
                Z = value.X;
                X = value.Y;
            }
        }
        public Vector2 GB
        {
            get { return new Vector2(Y, Z); }
            set
            {
                Y = value.X;
                Z = value.Y;
            }
        }
        public Vector2 BG
        {
            get { return new Vector2(Z, Y); }
            set
            {
                Z = value.X;
                Y = value.Y;
            }
        }

        public Vector3 XXX => new Vector3(X, X, X);
        public Vector3 XXY => new Vector3(X, X, Y);
        public Vector3 XXZ => new Vector3(X, X, Z);
        public Vector3 XYX => new Vector3(X, Y, X);
        public Vector3 XYY => new Vector3(X, Y, Y);
        public Vector3 XYZ
        {
            get { return new Vector3(X, Y, Z); }
            set
            {
                X = value.X;
                Y = value.Y;
                Z = value.Z;
            }
        }
        public Vector3 XZX => new Vector3(X, Z, X);
        public Vector3 XZY
        {
            get { return new Vector3(X, Z, Y); }
            set
            {
                X = value.X;
                Z = value.Y;
                Y = value.Z;
            }
        }
        public Vector3 XZZ => new Vector3(X, Z, Z);
        public Vector3 YXX => new Vector3(Y, X, X);
        public Vector3 YXY => new Vector3(Y, X, Y);
        public Vector3 YXZ
        {
            get { return new Vector3(Y, X, Z); }
            set
            {
                Y = value.X;
                X = value.Y;
                Z = value.Z;
            }
        }
        public Vector3 YYX => new Vector3(Y, Y, X);
        public Vector3 YYY => new Vector3(Y, Y, Y);
        public Vector3 YYZ => new Vector3(Y, Y, Z);
        public Vector3 YZX
        {
            get { return new Vector3(Y, Z, X); }
            set
            {
                Y = value.X;
                Z = value.Y;
                X = value.Z;
            }
        }
        public Vector3 YZY => new Vector3(Y, Z, Y);
        public Vector3 YZZ => new Vector3(Y, Z, Z);
        public Vector3 ZXX => new Vector3(Z, X, X);
        public Vector3 ZXY
        {
            get { return new Vector3(Z, X, Y); }
            set
            {
                Z = value.X;
                X = value.Y;
                Y = value.Z;
            }
        }
        public Vector3 ZXZ => new Vector3(Z, X, Z);
        public Vector3 ZYX
        {
            get { return new Vector3(Z, Y, X); }
            set
            {
                Z = value.X;
                Y = value.Y;
                X = value.Z;
            }
        }
        public Vector3 ZYY => new Vector3(Z, Y, Y);
        public Vector3 ZYZ => new Vector3(Z, Y, Z);
        public Vector3 ZZX => new Vector3(Z, Z, X);
        public Vector3 ZZY => new Vector3(Z, Z, Y);
        public Vector3 ZZZ => new Vector3(Z, Z, Z);

        public Vector3 RRR => new Vector3(X, X, X);
        public Vector3 RRG => new Vector3(X, X, Y);
        public Vector3 RRB => new Vector3(X, X, Z);
        public Vector3 RGR => new Vector3(X, Y, X);
        public Vector3 RGG => new Vector3(X, Y, Y);
        public Vector3 RGB
        {
            get { return new Vector3(X, Y, Z); }
            set
            {
                X = value.X;
                Y = value.Y;
                Z = value.Z;
            }
        }
        public Vector3 RBR => new Vector3(X, Z, X);
        public Vector3 RBG
        {
            get { return new Vector3(X, Z, Y); }
            set
            {
                X = value.X;
                Z = value.Y;
                Y = value.Z;
            }
        }
        public Vector3 RBB => new Vector3(X, Z, Z);
        public Vector3 GRR => new Vector3(Y, X, X);
        public Vector3 GRG => new Vector3(Y, X, Y);
        public Vector3 GRB
        {
            get { return new Vector3(Y, X, Z); }
            set
            {
                Y = value.X;
                X = value.Y;
                Z = value.Z;
            }
        }
        public Vector3 GGR => new Vector3(Y, Y, X);
        public Vector3 GGG => new Vector3(Y, Y, Y);
        public Vector3 GGB => new Vector3(Y, Y, Z);
        public Vector3 GBR
        {
            get { return new Vector3(Y, Z, X); }
            set
            {
                Y = value.X;
                Z = value.Y;
                X = value.Z;
            }
        }
        public Vector3 GBG => new Vector3(Y, Z, Y);
        public Vector3 GBB => new Vector3(Y, Z, Z);
        public Vector3 BRR => new Vector3(Z, X, X);
        public Vector3 BRG
        {
            get { return new Vector3(Z, X, Y); }
            set
            {
                Z = value.X;
                X = value.Y;
                Y = value.Z;
            }
        }
        public Vector3 BRB => new Vector3(Z, X, Z);
        public Vector3 BGR
        {
            get { return new Vector3(Z, Y, X); }
            set
            {
                Z = value.X;
                Y = value.Y;
                X = value.Z;
            }
        }
        public Vector3 BGG => new Vector3(Z, Y, Y);
        public Vector3 BGB => new Vector3(Z, Y, Z);
        public Vector3 BBR => new Vector3(Z, Z, X);
        public Vector3 BBG => new Vector3(Z, Z, Y);
        public Vector3 BBB => new Vector3(Z, Z, Z);



        public Vector3(float value)
        {
            X = value;
            Y = value;
            Z = value;
        }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3(float x, Vector2 vec2)
        {
            X = x;
            Y = vec2.X;
            Z = vec2.Y;
        }

        public Vector3(Vector2 vec2, float z)
        {
            X = vec2.X;
            Y = vec2.Y;
            Z = z;
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3()
            {
                X = a.X + b.X,
                Y = a.Y + b.Y,
                Z = a.Z + b.Z,
            };
        }
        public static Vector3 operator +(Vector3 a, int b)
        {
            return new Vector3()
            {
                X = (float)(a.X + b),
                Y = (float)(a.Y + b),
                Z = (float)(a.Z + b),
            };
        }

        public static Vector3 operator +(int a, Vector3 b)
        {
            return new Vector3()
            {
                X = (float)(a + b.X),
                Y = (float)(a + b.Y),
                Z = (float)(a + b.Z),
            };
        }
        public static Vector3 operator +(Vector3 a, float b)
        {
            return new Vector3()
            {
                X = (float)(a.X + b),
                Y = (float)(a.Y + b),
                Z = (float)(a.Z + b),
            };
        }

        public static Vector3 operator +(float a, Vector3 b)
        {
            return new Vector3()
            {
                X = (a + b.X),
                Y = (a + b.Y),
                Z = (a + b.Z),
            };
        }
        public static Vector3 operator +(Vector3 a, double b)
        {
            return new Vector3()
            {
                X = (float)(a.X + b),
                Y = (float)(a.Y + b),
                Z = (float)(a.Z + b),
            };
        }

        public static Vector3 operator +(double a, Vector3 b)
        {
            return new Vector3()
            {
                X = (float)(a + b.X),
                Y = (float)(a + b.Y),
                Z = (float)(a + b.Z),
            };
        }
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3()
            {
                X = a.X - b.X,
                Y = a.Y - b.Y,
                Z = a.Z - b.Z,
            };
        }
        public static Vector3 operator -(Vector3 a, int b)
        {
            return new Vector3()
            {
                X = (float)(a.X - b),
                Y = (float)(a.Y - b),
                Z = (float)(a.Z - b),
            };
        }

        public static Vector3 operator -(int a, Vector3 b)
        {
            return new Vector3()
            {
                X = (float)(a - b.X),
                Y = (float)(a - b.Y),
                Z = (float)(a - b.Z),
            };
        }
        public static Vector3 operator -(Vector3 a, float b)
        {
            return new Vector3()
            {
                X = (float)(a.X - b),
                Y = (float)(a.Y - b),
                Z = (float)(a.Z - b),
            };
        }

        public static Vector3 operator -(float a, Vector3 b)
        {
            return new Vector3()
            {
                X = (a - b.X),
                Y = (a - b.Y),
                Z = (a - b.Z),
            };
        }
        public static Vector3 operator -(Vector3 a, double b)
        {
            return new Vector3()
            {
                X = (float)(a.X - b),
                Y = (float)(a.Y - b),
                Z = (float)(a.Z - b),
            };
        }

        public static Vector3 operator -(double a, Vector3 b)
        {
            return new Vector3()
            {
                X = (float)(a - b.X),
                Y = (float)(a - b.Y),
                Z = (float)(a - b.Z),
            };
        }
        public static Vector3 operator *(Vector3 a, Vector3 b)
        {
            return new Vector3()
            {
                X = a.X * b.X,
                Y = a.Y * b.Y,
                Z = a.Z * b.Z,
            };
        }
        public static Vector3 operator *(Vector3 a, int b)
        {
            return new Vector3()
            {
                X = (float)(a.X * b),
                Y = (float)(a.Y * b),
                Z = (float)(a.Z * b),
            };
        }

        public static Vector3 operator *(int a, Vector3 b)
        {
            return new Vector3()
            {
                X = (float)(a * b.X),
                Y = (float)(a * b.Y),
                Z = (float)(a * b.Z),
            };
        }
        public static Vector3 operator *(Vector3 a, float b)
        {
            return new Vector3()
            {
                X = (float)(a.X * b),
                Y = (float)(a.Y * b),
                Z = (float)(a.Z * b),
            };
        }

        public static Vector3 operator *(float a, Vector3 b)
        {
            return new Vector3()
            {
                X = (a * b.X),
                Y = (a * b.Y),
                Z = (a * b.Z),
            };
        }
        public static Vector3 operator *(Vector3 a, double b)
        {
            return new Vector3()
            {
                X = (float)(a.X * b),
                Y = (float)(a.Y * b),
                Z = (float)(a.Z * b),
            };
        }

        public static Vector3 operator *(double a, Vector3 b)
        {
            return new Vector3()
            {
                X = (float)(a * b.X),
                Y = (float)(a * b.Y),
                Z = (float)(a * b.Z),
            };
        }
        public static Vector3 operator /(Vector3 a, Vector3 b)
        {
            return new Vector3()
            {
                X = a.X / b.X,
                Y = a.Y / b.Y,
                Z = a.Z / b.Z,
            };
        }
        public static Vector3 operator /(Vector3 a, int b)
        {
            return new Vector3()
            {
                X = (float)(a.X / b),
                Y = (float)(a.Y / b),
                Z = (float)(a.Z / b),
            };
        }

        public static Vector3 operator /(int a, Vector3 b)
        {
            return new Vector3()
            {
                X = (float)(a / b.X),
                Y = (float)(a / b.Y),
                Z = (float)(a / b.Z),
            };
        }
        public static Vector3 operator /(Vector3 a, float b)
        {
            return new Vector3()
            {
                X = (float)(a.X / b),
                Y = (float)(a.Y / b),
                Z = (float)(a.Z / b),
            };
        }

        public static Vector3 operator /(float a, Vector3 b)
        {
            return new Vector3()
            {
                X = (a / b.X),
                Y = (a / b.Y),
                Z = (a / b.Z),
            };
        }
        public static Vector3 operator /(Vector3 a, double b)
        {
            return new Vector3()
            {
                X = (float)(a.X / b),
                Y = (float)(a.Y / b),
                Z = (float)(a.Z / b),
            };
        }

        public static Vector3 operator /(double a, Vector3 b)
        {
            return new Vector3()
            {
                X = (float)(a / b.X),
                Y = (float)(a / b.Y),
                Z = (float)(a / b.Z),
            };
        }
    }
}

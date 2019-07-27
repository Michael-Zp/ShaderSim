using System;

namespace ShaderSim.Mathematics
{
    public struct Vector4
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public float W { get; set; }

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
                    case 3:
                        return W;
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
                    case 3:
                        W = value;
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

        public float A
        {
            get { return W; }
            set { W = value; }
        }

        public Vector2 XX => new Vector2(X, X);
        public Vector2 XY
        {
            get { return new Vector2(X, Y); }
            set
            {
                X = value.X;
                Y = value.Y;
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
        public Vector2 XW
        {
            get { return new Vector2(X, W); }
            set
            {
                X = value.X;
                W = value.Y;
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
        public Vector2 YY => new Vector2(Y, Y);
        public Vector2 YZ
        {
            get { return new Vector2(Y, Z); }
            set
            {
                Y = value.X;
                Z = value.Y;
            }
        }
        public Vector2 YW
        {
            get { return new Vector2(Y, W); }
            set
            {
                Y = value.X;
                W = value.Y;
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
        public Vector2 ZY
        {
            get { return new Vector2(Z, Y); }
            set
            {
                Z = value.X;
                Y = value.Y;
            }
        }
        public Vector2 ZZ => new Vector2(Z, Z);
        public Vector2 ZW
        {
            get { return new Vector2(Z, W); }
            set
            {
                Z = value.X;
                W = value.Y;
            }
        }
        public Vector2 WX
        {
            get { return new Vector2(W, X); }
            set
            {
                W = value.X;
                X = value.Y;
            }
        }
        public Vector2 WY
        {
            get { return new Vector2(W, Y); }
            set
            {
                W = value.X;
                Y = value.Y;
            }
        }
        public Vector2 WZ
        {
            get { return new Vector2(W, Z); }
            set
            {
                W = value.X;
                Z = value.Y;
            }
        }
        public Vector2 WW => new Vector2(W, W);

        public Vector2 RR => new Vector2(X, X);
        public Vector2 RG
        {
            get { return new Vector2(X, Y); }
            set
            {
                X = value.X;
                Y = value.Y;
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
        public Vector2 RA
        {
            get { return new Vector2(X, W); }
            set
            {
                X = value.X;
                W = value.Y;
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
        public Vector2 GG => new Vector2(Y, Y);
        public Vector2 GB
        {
            get { return new Vector2(Y, Z); }
            set
            {
                Y = value.X;
                Z = value.Y;
            }
        }
        public Vector2 GA
        {
            get { return new Vector2(Y, W); }
            set
            {
                Y = value.X;
                W = value.Y;
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
        public Vector2 BG
        {
            get { return new Vector2(Z, Y); }
            set
            {
                Z = value.X;
                Y = value.Y;
            }
        }
        public Vector2 BB => new Vector2(Z, Z);
        public Vector2 BA
        {
            get { return new Vector2(Z, W); }
            set
            {
                Z = value.X;
                W = value.Y;
            }
        }
        public Vector2 AR
        {
            get { return new Vector2(W, X); }
            set
            {
                W = value.X;
                X = value.Y;
            }
        }
        public Vector2 AG
        {
            get { return new Vector2(W, Y); }
            set
            {
                W = value.X;
                Y = value.Y;
            }
        }
        public Vector2 AB
        {
            get { return new Vector2(W, Z); }
            set
            {
                W = value.X;
                Z = value.Y;
            }
        }
        public Vector2 AA => new Vector2(W, W);


        public Vector3 XXX => new Vector3(X, X, X);
        public Vector3 XXY => new Vector3(X, X, Y);
        public Vector3 XXZ => new Vector3(X, X, Z);
        public Vector3 XXW => new Vector3(X, X, W);
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
        public Vector3 XYW
        {
            get { return new Vector3(X, Y, W); }
            set
            {
                X = value.X;
                Y = value.Y;
                W = value.Z;
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
        public Vector3 XZW
        {
            get { return new Vector3(X, Z, W); }
            set
            {
                X = value.X;
                Z = value.Y;
                W = value.Z;
            }
        }
        public Vector3 XWX => new Vector3(X, W, X);
        public Vector3 XWY
        {
            get { return new Vector3(X, W, Y); }
            set
            {
                X = value.X;
                W = value.Y;
                Y = value.Z;
            }
        }
        public Vector3 XWZ
        {
            get { return new Vector3(X, W, Z); }
            set
            {
                X = value.X;
                W = value.Y;
                Z = value.Z;
            }
        }
        public Vector3 XWW => new Vector3(X, W, W);
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
        public Vector3 YXW
        {
            get { return new Vector3(Y, X, W); }
            set
            {
                Y = value.X;
                X = value.Y;
                W = value.Z;
            }
        }
        public Vector3 YYX => new Vector3(Y, Y, X);
        public Vector3 YYY => new Vector3(Y, Y, Y);
        public Vector3 YYZ => new Vector3(Y, Y, Z);
        public Vector3 YYW => new Vector3(Y, Y, W);
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
        public Vector3 YZW
        {
            get { return new Vector3(Y, Z, W); }
            set
            {
                Y = value.X;
                Z = value.Y;
                W = value.Z;
            }
        }
        public Vector3 YWX
        {
            get { return new Vector3(Y, W, X); }
            set
            {
                Y = value.X;
                W = value.Y;
                X = value.Z;
            }
        }
        public Vector3 YWY => new Vector3(Y, W, Y);
        public Vector3 YWZ
        {
            get { return new Vector3(Y, W, Z); }
            set
            {
                Y = value.X;
                W = value.Y;
                Z = value.Z;
            }
        }
        public Vector3 YWW => new Vector3(Y, W, W);
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
        public Vector3 ZXW
        {
            get { return new Vector3(Z, X, W); }
            set
            {
                Z = value.X;
                X = value.Y;
                W = value.Z;
            }
        }
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
        public Vector3 ZYW
        {
            get { return new Vector3(Z, Y, W); }
            set
            {
                Z = value.X;
                Y = value.Y;
                W = value.Z;
            }
        }
        public Vector3 ZZX => new Vector3(Z, Z, X);
        public Vector3 ZZY => new Vector3(Z, Z, Y);
        public Vector3 ZZZ => new Vector3(Z, Z, Z);
        public Vector3 ZZW => new Vector3(Z, Z, W);
        public Vector3 ZWX
        {
            get { return new Vector3(Z, W, X); }
            set
            {
                Z = value.X;
                W = value.Y;
                X = value.Z;
            }
        }
        public Vector3 ZWY
        {
            get { return new Vector3(Z, W, Y); }
            set
            {
                Z = value.X;
                W = value.Y;
                Y = value.Z;
            }
        }
        public Vector3 ZWZ => new Vector3(Z, W, Z);
        public Vector3 ZWW => new Vector3(Z, W, W);
        public Vector3 WXX => new Vector3(W, X, X);
        public Vector3 WXY
        {
            get { return new Vector3(W, X, Y); }
            set
            {
                W = value.X;
                X = value.Y;
                Y = value.Z;
            }
        }
        public Vector3 WXZ
        {
            get { return new Vector3(W, X, Z); }
            set
            {
                W = value.X;
                X = value.Y;
                Z = value.Z;
            }
        }
        public Vector3 WXW => new Vector3(W, X, W);
        public Vector3 WYX
        {
            get { return new Vector3(W, Y, X); }
            set
            {
                W = value.X;
                Y = value.Y;
                X = value.Z;
            }
        }
        public Vector3 WYY => new Vector3(W, Y, Y);
        public Vector3 WYZ
        {
            get { return new Vector3(W, Y, Z); }
            set
            {
                W = value.X;
                Y = value.Y;
                Z = value.Z;
            }
        }
        public Vector3 WYW => new Vector3(W, Y, W);
        public Vector3 WZX
        {
            get { return new Vector3(W, Z, X); }
            set
            {
                W = value.X;
                Z = value.Y;
                X = value.Z;
            }
        }
        public Vector3 WZY
        {
            get { return new Vector3(W, Z, Y); }
            set
            {
                W = value.X;
                Z = value.Y;
                Y = value.Z;
            }
        }
        public Vector3 WZZ => new Vector3(W, Z, Z);
        public Vector3 WZW => new Vector3(W, Z, W);
        public Vector3 WWX => new Vector3(W, W, X);
        public Vector3 WWY => new Vector3(W, W, Y);
        public Vector3 WWZ => new Vector3(W, W, Z);
        public Vector3 WWW => new Vector3(W, W, W);

        public Vector3 RRR => new Vector3(X, X, X);
        public Vector3 RRG => new Vector3(X, X, Y);
        public Vector3 RRB => new Vector3(X, X, Z);
        public Vector3 RRA => new Vector3(X, X, W);
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
        public Vector3 RGA
        {
            get { return new Vector3(X, Y, W); }
            set
            {
                X = value.X;
                Y = value.Y;
                W = value.Z;
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
        public Vector3 RBA
        {
            get { return new Vector3(X, Z, W); }
            set
            {
                X = value.X;
                Z = value.Y;
                W = value.Z;
            }
        }
        public Vector3 RAR => new Vector3(X, W, X);
        public Vector3 RAG
        {
            get { return new Vector3(X, W, Y); }
            set
            {
                X = value.X;
                W = value.Y;
                Y = value.Z;
            }
        }
        public Vector3 RAB
        {
            get { return new Vector3(X, W, Z); }
            set
            {
                X = value.X;
                W = value.Y;
                Z = value.Z;
            }
        }
        public Vector3 RAA => new Vector3(X, W, W);
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
        public Vector3 GRA
        {
            get { return new Vector3(Y, X, W); }
            set
            {
                Y = value.X;
                X = value.Y;
                W = value.Z;
            }
        }
        public Vector3 GGR => new Vector3(Y, Y, X);
        public Vector3 GGG => new Vector3(Y, Y, Y);
        public Vector3 GGB => new Vector3(Y, Y, Z);
        public Vector3 GGA => new Vector3(Y, Y, W);
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
        public Vector3 GBA
        {
            get { return new Vector3(Y, Z, W); }
            set
            {
                Y = value.X;
                Z = value.Y;
                W = value.Z;
            }
        }
        public Vector3 GAR
        {
            get { return new Vector3(Y, W, X); }
            set
            {
                Y = value.X;
                W = value.Y;
                X = value.Z;
            }
        }
        public Vector3 GAG => new Vector3(Y, W, Y);
        public Vector3 GAB
        {
            get { return new Vector3(Y, W, Z); }
            set
            {
                Y = value.X;
                W = value.Y;
                Z = value.Z;
            }
        }
        public Vector3 GAA => new Vector3(Y, W, W);
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
        public Vector3 BRA
        {
            get { return new Vector3(Z, X, W); }
            set
            {
                Z = value.X;
                X = value.Y;
                W = value.Z;
            }
        }
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
        public Vector3 BGA
        {
            get { return new Vector3(Z, Y, W); }
            set
            {
                Z = value.X;
                Y = value.Y;
                W = value.Z;
            }
        }
        public Vector3 BBR => new Vector3(Z, Z, X);
        public Vector3 BBG => new Vector3(Z, Z, Y);
        public Vector3 BBB => new Vector3(Z, Z, Z);
        public Vector3 BBA => new Vector3(Z, Z, W);
        public Vector3 BAR
        {
            get { return new Vector3(Z, W, X); }
            set
            {
                Z = value.X;
                W = value.Y;
                X = value.Z;
            }
        }
        public Vector3 BAG
        {
            get { return new Vector3(Z, W, Y); }
            set
            {
                Z = value.X;
                W = value.Y;
                Y = value.Z;
            }
        }
        public Vector3 BAB => new Vector3(Z, W, Z);
        public Vector3 BAA => new Vector3(Z, W, W);
        public Vector3 ARR => new Vector3(W, X, X);
        public Vector3 ARG
        {
            get { return new Vector3(W, X, Y); }
            set
            {
                W = value.X;
                X = value.Y;
                Y = value.Z;
            }
        }
        public Vector3 ARB
        {
            get { return new Vector3(W, X, Z); }
            set
            {
                W = value.X;
                X = value.Y;
                Z = value.Z;
            }
        }
        public Vector3 ARA => new Vector3(W, X, W);
        public Vector3 AGR
        {
            get { return new Vector3(W, Y, X); }
            set
            {
                W = value.X;
                Y = value.Y;
                X = value.Z;
            }
        }
        public Vector3 AGG => new Vector3(W, Y, Y);
        public Vector3 AGB
        {
            get { return new Vector3(W, Y, Z); }
            set
            {
                W = value.X;
                Y = value.Y;
                Z = value.Z;
            }
        }
        public Vector3 AGA => new Vector3(W, Y, W);
        public Vector3 ABR
        {
            get { return new Vector3(W, Z, X); }
            set
            {
                W = value.X;
                Z = value.Y;
                X = value.Z;
            }
        }
        public Vector3 ABG
        {
            get { return new Vector3(W, Z, Y); }
            set
            {
                W = value.X;
                Z = value.Y;
                Y = value.Z;
            }
        }
        public Vector3 ABB => new Vector3(W, Z, Z);
        public Vector3 ABA => new Vector3(W, Z, W);
        public Vector3 AAR => new Vector3(W, W, X);
        public Vector3 AAG => new Vector3(W, W, Y);
        public Vector3 AAB => new Vector3(W, W, Z);
        public Vector3 AAA => new Vector3(W, W, W);

        public Vector4 XXXX => new Vector4(X, X, X, X);
        public Vector4 XXXY => new Vector4(X, X, X, Y);
        public Vector4 XXXZ => new Vector4(X, X, X, Z);
        public Vector4 XXXW => new Vector4(X, X, X, W);
        public Vector4 XXYX => new Vector4(X, X, Y, X);
        public Vector4 XXYY => new Vector4(X, X, Y, Y);
        public Vector4 XXYZ => new Vector4(X, X, Y, Z);
        public Vector4 XXYW => new Vector4(X, X, Y, W);
        public Vector4 XXZX => new Vector4(X, X, Z, X);
        public Vector4 XXZY => new Vector4(X, X, Z, Y);
        public Vector4 XXZZ => new Vector4(X, X, Z, Z);
        public Vector4 XXZW => new Vector4(X, X, Z, W);
        public Vector4 XXWX => new Vector4(X, X, W, X);
        public Vector4 XXWY => new Vector4(X, X, W, Y);
        public Vector4 XXWZ => new Vector4(X, X, W, Z);
        public Vector4 XXWW => new Vector4(X, X, W, W);
        public Vector4 XYXX => new Vector4(X, Y, X, X);
        public Vector4 XYXY => new Vector4(X, Y, X, Y);
        public Vector4 XYXZ => new Vector4(X, Y, X, Z);
        public Vector4 XYXW => new Vector4(X, Y, X, W);
        public Vector4 XYYX => new Vector4(X, Y, Y, X);
        public Vector4 XYYY => new Vector4(X, Y, Y, Y);
        public Vector4 XYYZ => new Vector4(X, Y, Y, Z);
        public Vector4 XYYW => new Vector4(X, Y, Y, W);
        public Vector4 XYZX => new Vector4(X, Y, Z, X);
        public Vector4 XYZY => new Vector4(X, Y, Z, Y);
        public Vector4 XYZZ => new Vector4(X, Y, Z, Z);
        public Vector4 XYZW
        {
            get { return new Vector4(X, Y, Z, W); }
            set
            {
                X = value.X;
                Y = value.Y;
                Z = value.Z;
                W = value.W;
            }
        }
        public Vector4 XYWX => new Vector4(X, Y, W, X);
        public Vector4 XYWY => new Vector4(X, Y, W, Y);
        public Vector4 XYWZ
        {
            get { return new Vector4(X, Y, W, Z); }
            set
            {
                X = value.X;
                Y = value.Y;
                W = value.Z;
                Z = value.W;
            }
        }
        public Vector4 XYWW => new Vector4(X, Y, W, W);
        public Vector4 XZXX => new Vector4(X, Z, X, X);
        public Vector4 XZXY => new Vector4(X, Z, X, Y);
        public Vector4 XZXZ => new Vector4(X, Z, X, Z);
        public Vector4 XZXW => new Vector4(X, Z, X, W);
        public Vector4 XZYX => new Vector4(X, Z, Y, X);
        public Vector4 XZYY => new Vector4(X, Z, Y, Y);
        public Vector4 XZYZ => new Vector4(X, Z, Y, Z);
        public Vector4 XZYW
        {
            get { return new Vector4(X, Z, Y, W); }
            set
            {
                X = value.X;
                Z = value.Y;
                Y = value.Z;
                W = value.W;
            }
        }
        public Vector4 XZZX => new Vector4(X, Z, Z, X);
        public Vector4 XZZY => new Vector4(X, Z, Z, Y);
        public Vector4 XZZZ => new Vector4(X, Z, Z, Z);
        public Vector4 XZZW => new Vector4(X, Z, Z, W);
        public Vector4 XZWX => new Vector4(X, Z, W, X);
        public Vector4 XZWY
        {
            get { return new Vector4(X, Z, W, Y); }
            set
            {
                X = value.X;
                Z = value.Y;
                W = value.Z;
                Y = value.W;
            }
        }
        public Vector4 XZWZ => new Vector4(X, Z, W, Z);
        public Vector4 XZWW => new Vector4(X, Z, W, W);
        public Vector4 XWXX => new Vector4(X, W, X, X);
        public Vector4 XWXY => new Vector4(X, W, X, Y);
        public Vector4 XWXZ => new Vector4(X, W, X, Z);
        public Vector4 XWXW => new Vector4(X, W, X, W);
        public Vector4 XWYX => new Vector4(X, W, Y, X);
        public Vector4 XWYY => new Vector4(X, W, Y, Y);
        public Vector4 XWYZ
        {
            get { return new Vector4(X, W, Y, Z); }
            set
            {
                X = value.X;
                W = value.Y;
                Y = value.Z;
                Z = value.W;
            }
        }
        public Vector4 XWYW => new Vector4(X, W, Y, W);
        public Vector4 XWZX => new Vector4(X, W, Z, X);
        public Vector4 XWZY
        {
            get { return new Vector4(X, W, Z, Y); }
            set
            {
                X = value.X;
                W = value.Y;
                Z = value.Z;
                Y = value.W;
            }
        }
        public Vector4 XWZZ => new Vector4(X, W, Z, Z);
        public Vector4 XWZW => new Vector4(X, W, Z, W);
        public Vector4 XWWX => new Vector4(X, W, W, X);
        public Vector4 XWWY => new Vector4(X, W, W, Y);
        public Vector4 XWWZ => new Vector4(X, W, W, Z);
        public Vector4 XWWW => new Vector4(X, W, W, W);
        public Vector4 YXXX => new Vector4(Y, X, X, X);
        public Vector4 YXXY => new Vector4(Y, X, X, Y);
        public Vector4 YXXZ => new Vector4(Y, X, X, Z);
        public Vector4 YXXW => new Vector4(Y, X, X, W);
        public Vector4 YXYX => new Vector4(Y, X, Y, X);
        public Vector4 YXYY => new Vector4(Y, X, Y, Y);
        public Vector4 YXYZ => new Vector4(Y, X, Y, Z);
        public Vector4 YXYW => new Vector4(Y, X, Y, W);
        public Vector4 YXZX => new Vector4(Y, X, Z, X);
        public Vector4 YXZY => new Vector4(Y, X, Z, Y);
        public Vector4 YXZZ => new Vector4(Y, X, Z, Z);
        public Vector4 YXZW
        {
            get { return new Vector4(Y, X, Z, W); }
            set
            {
                Y = value.X;
                X = value.Y;
                Z = value.Z;
                W = value.W;
            }
        }
        public Vector4 YXWX => new Vector4(Y, X, W, X);
        public Vector4 YXWY => new Vector4(Y, X, W, Y);
        public Vector4 YXWZ
        {
            get { return new Vector4(Y, X, W, Z); }
            set
            {
                Y = value.X;
                X = value.Y;
                W = value.Z;
                Z = value.W;
            }
        }
        public Vector4 YXWW => new Vector4(Y, X, W, W);
        public Vector4 YYXX => new Vector4(Y, Y, X, X);
        public Vector4 YYXY => new Vector4(Y, Y, X, Y);
        public Vector4 YYXZ => new Vector4(Y, Y, X, Z);
        public Vector4 YYXW => new Vector4(Y, Y, X, W);
        public Vector4 YYYX => new Vector4(Y, Y, Y, X);
        public Vector4 YYYY => new Vector4(Y, Y, Y, Y);
        public Vector4 YYYZ => new Vector4(Y, Y, Y, Z);
        public Vector4 YYYW => new Vector4(Y, Y, Y, W);
        public Vector4 YYZX => new Vector4(Y, Y, Z, X);
        public Vector4 YYZY => new Vector4(Y, Y, Z, Y);
        public Vector4 YYZZ => new Vector4(Y, Y, Z, Z);
        public Vector4 YYZW => new Vector4(Y, Y, Z, W);
        public Vector4 YYWX => new Vector4(Y, Y, W, X);
        public Vector4 YYWY => new Vector4(Y, Y, W, Y);
        public Vector4 YYWZ => new Vector4(Y, Y, W, Z);
        public Vector4 YYWW => new Vector4(Y, Y, W, W);
        public Vector4 YZXX => new Vector4(Y, Z, X, X);
        public Vector4 YZXY => new Vector4(Y, Z, X, Y);
        public Vector4 YZXZ => new Vector4(Y, Z, X, Z);
        public Vector4 YZXW
        {
            get { return new Vector4(Y, Z, X, W); }
            set
            {
                Y = value.X;
                Z = value.Y;
                X = value.Z;
                W = value.W;
            }
        }
        public Vector4 YZYX => new Vector4(Y, Z, Y, X);
        public Vector4 YZYY => new Vector4(Y, Z, Y, Y);
        public Vector4 YZYZ => new Vector4(Y, Z, Y, Z);
        public Vector4 YZYW => new Vector4(Y, Z, Y, W);
        public Vector4 YZZX => new Vector4(Y, Z, Z, X);
        public Vector4 YZZY => new Vector4(Y, Z, Z, Y);
        public Vector4 YZZZ => new Vector4(Y, Z, Z, Z);
        public Vector4 YZZW => new Vector4(Y, Z, Z, W);
        public Vector4 YZWX
        {
            get { return new Vector4(Y, Z, W, X); }
            set
            {
                Y = value.X;
                Z = value.Y;
                W = value.Z;
                X = value.W;
            }
        }
        public Vector4 YZWY => new Vector4(Y, Z, W, Y);
        public Vector4 YZWZ => new Vector4(Y, Z, W, Z);
        public Vector4 YZWW => new Vector4(Y, Z, W, W);
        public Vector4 YWXX => new Vector4(Y, W, X, X);
        public Vector4 YWXY => new Vector4(Y, W, X, Y);
        public Vector4 YWXZ
        {
            get { return new Vector4(Y, W, X, Z); }
            set
            {
                Y = value.X;
                W = value.Y;
                X = value.Z;
                Z = value.W;
            }
        }
        public Vector4 YWXW => new Vector4(Y, W, X, W);
        public Vector4 YWYX => new Vector4(Y, W, Y, X);
        public Vector4 YWYY => new Vector4(Y, W, Y, Y);
        public Vector4 YWYZ => new Vector4(Y, W, Y, Z);
        public Vector4 YWYW => new Vector4(Y, W, Y, W);
        public Vector4 YWZX
        {
            get { return new Vector4(Y, W, Z, X); }
            set
            {
                Y = value.X;
                W = value.Y;
                Z = value.Z;
                X = value.W;
            }
        }
        public Vector4 YWZY => new Vector4(Y, W, Z, Y);
        public Vector4 YWZZ => new Vector4(Y, W, Z, Z);
        public Vector4 YWZW => new Vector4(Y, W, Z, W);
        public Vector4 YWWX => new Vector4(Y, W, W, X);
        public Vector4 YWWY => new Vector4(Y, W, W, Y);
        public Vector4 YWWZ => new Vector4(Y, W, W, Z);
        public Vector4 YWWW => new Vector4(Y, W, W, W);
        public Vector4 ZXXX => new Vector4(Z, X, X, X);
        public Vector4 ZXXY => new Vector4(Z, X, X, Y);
        public Vector4 ZXXZ => new Vector4(Z, X, X, Z);
        public Vector4 ZXXW => new Vector4(Z, X, X, W);
        public Vector4 ZXYX => new Vector4(Z, X, Y, X);
        public Vector4 ZXYY => new Vector4(Z, X, Y, Y);
        public Vector4 ZXYZ => new Vector4(Z, X, Y, Z);
        public Vector4 ZXYW
        {
            get { return new Vector4(Z, X, Y, W); }
            set
            {
                Z = value.X;
                X = value.Y;
                Y = value.Z;
                W = value.W;
            }
        }
        public Vector4 ZXZX => new Vector4(Z, X, Z, X);
        public Vector4 ZXZY => new Vector4(Z, X, Z, Y);
        public Vector4 ZXZZ => new Vector4(Z, X, Z, Z);
        public Vector4 ZXZW => new Vector4(Z, X, Z, W);
        public Vector4 ZXWX => new Vector4(Z, X, W, X);
        public Vector4 ZXWY
        {
            get { return new Vector4(Z, X, W, Y); }
            set
            {
                Z = value.X;
                X = value.Y;
                W = value.Z;
                Y = value.W;
            }
        }
        public Vector4 ZXWZ => new Vector4(Z, X, W, Z);
        public Vector4 ZXWW => new Vector4(Z, X, W, W);
        public Vector4 ZYXX => new Vector4(Z, Y, X, X);
        public Vector4 ZYXY => new Vector4(Z, Y, X, Y);
        public Vector4 ZYXZ => new Vector4(Z, Y, X, Z);
        public Vector4 ZYXW
        {
            get { return new Vector4(Z, Y, X, W); }
            set
            {
                Z = value.X;
                Y = value.Y;
                X = value.Z;
                W = value.W;
            }
        }
        public Vector4 ZYYX => new Vector4(Z, Y, Y, X);
        public Vector4 ZYYY => new Vector4(Z, Y, Y, Y);
        public Vector4 ZYYZ => new Vector4(Z, Y, Y, Z);
        public Vector4 ZYYW => new Vector4(Z, Y, Y, W);
        public Vector4 ZYZX => new Vector4(Z, Y, Z, X);
        public Vector4 ZYZY => new Vector4(Z, Y, Z, Y);
        public Vector4 ZYZZ => new Vector4(Z, Y, Z, Z);
        public Vector4 ZYZW => new Vector4(Z, Y, Z, W);
        public Vector4 ZYWX
        {
            get { return new Vector4(Z, Y, W, X); }
            set
            {
                Z = value.X;
                Y = value.Y;
                W = value.Z;
                X = value.W;
            }
        }
        public Vector4 ZYWY => new Vector4(Z, Y, W, Y);
        public Vector4 ZYWZ => new Vector4(Z, Y, W, Z);
        public Vector4 ZYWW => new Vector4(Z, Y, W, W);
        public Vector4 ZZXX => new Vector4(Z, Z, X, X);
        public Vector4 ZZXY => new Vector4(Z, Z, X, Y);
        public Vector4 ZZXZ => new Vector4(Z, Z, X, Z);
        public Vector4 ZZXW => new Vector4(Z, Z, X, W);
        public Vector4 ZZYX => new Vector4(Z, Z, Y, X);
        public Vector4 ZZYY => new Vector4(Z, Z, Y, Y);
        public Vector4 ZZYZ => new Vector4(Z, Z, Y, Z);
        public Vector4 ZZYW => new Vector4(Z, Z, Y, W);
        public Vector4 ZZZX => new Vector4(Z, Z, Z, X);
        public Vector4 ZZZY => new Vector4(Z, Z, Z, Y);
        public Vector4 ZZZZ => new Vector4(Z, Z, Z, Z);
        public Vector4 ZZZW => new Vector4(Z, Z, Z, W);
        public Vector4 ZZWX => new Vector4(Z, Z, W, X);
        public Vector4 ZZWY => new Vector4(Z, Z, W, Y);
        public Vector4 ZZWZ => new Vector4(Z, Z, W, Z);
        public Vector4 ZZWW => new Vector4(Z, Z, W, W);
        public Vector4 ZWXX => new Vector4(Z, W, X, X);
        public Vector4 ZWXY
        {
            get { return new Vector4(Z, W, X, Y); }
            set
            {
                Z = value.X;
                W = value.Y;
                X = value.Z;
                Y = value.W;
            }
        }
        public Vector4 ZWXZ => new Vector4(Z, W, X, Z);
        public Vector4 ZWXW => new Vector4(Z, W, X, W);
        public Vector4 ZWYX
        {
            get { return new Vector4(Z, W, Y, X); }
            set
            {
                Z = value.X;
                W = value.Y;
                Y = value.Z;
                X = value.W;
            }
        }
        public Vector4 ZWYY => new Vector4(Z, W, Y, Y);
        public Vector4 ZWYZ => new Vector4(Z, W, Y, Z);
        public Vector4 ZWYW => new Vector4(Z, W, Y, W);
        public Vector4 ZWZX => new Vector4(Z, W, Z, X);
        public Vector4 ZWZY => new Vector4(Z, W, Z, Y);
        public Vector4 ZWZZ => new Vector4(Z, W, Z, Z);
        public Vector4 ZWZW => new Vector4(Z, W, Z, W);
        public Vector4 ZWWX => new Vector4(Z, W, W, X);
        public Vector4 ZWWY => new Vector4(Z, W, W, Y);
        public Vector4 ZWWZ => new Vector4(Z, W, W, Z);
        public Vector4 ZWWW => new Vector4(Z, W, W, W);
        public Vector4 WXXX => new Vector4(W, X, X, X);
        public Vector4 WXXY => new Vector4(W, X, X, Y);
        public Vector4 WXXZ => new Vector4(W, X, X, Z);
        public Vector4 WXXW => new Vector4(W, X, X, W);
        public Vector4 WXYX => new Vector4(W, X, Y, X);
        public Vector4 WXYY => new Vector4(W, X, Y, Y);
        public Vector4 WXYZ
        {
            get { return new Vector4(W, X, Y, Z); }
            set
            {
                W = value.X;
                X = value.Y;
                Y = value.Z;
                Z = value.W;
            }
        }
        public Vector4 WXYW => new Vector4(W, X, Y, W);
        public Vector4 WXZX => new Vector4(W, X, Z, X);
        public Vector4 WXZY
        {
            get { return new Vector4(W, X, Z, Y); }
            set
            {
                W = value.X;
                X = value.Y;
                Z = value.Z;
                Y = value.W;
            }
        }
        public Vector4 WXZZ => new Vector4(W, X, Z, Z);
        public Vector4 WXZW => new Vector4(W, X, Z, W);
        public Vector4 WXWX => new Vector4(W, X, W, X);
        public Vector4 WXWY => new Vector4(W, X, W, Y);
        public Vector4 WXWZ => new Vector4(W, X, W, Z);
        public Vector4 WXWW => new Vector4(W, X, W, W);
        public Vector4 WYXX => new Vector4(W, Y, X, X);
        public Vector4 WYXY => new Vector4(W, Y, X, Y);
        public Vector4 WYXZ
        {
            get { return new Vector4(W, Y, X, Z); }
            set
            {
                W = value.X;
                Y = value.Y;
                X = value.Z;
                Z = value.W;
            }
        }
        public Vector4 WYXW => new Vector4(W, Y, X, W);
        public Vector4 WYYX => new Vector4(W, Y, Y, X);
        public Vector4 WYYY => new Vector4(W, Y, Y, Y);
        public Vector4 WYYZ => new Vector4(W, Y, Y, Z);
        public Vector4 WYYW => new Vector4(W, Y, Y, W);
        public Vector4 WYZX
        {
            get { return new Vector4(W, Y, Z, X); }
            set
            {
                W = value.X;
                Y = value.Y;
                Z = value.Z;
                X = value.W;
            }
        }
        public Vector4 WYZY => new Vector4(W, Y, Z, Y);
        public Vector4 WYZZ => new Vector4(W, Y, Z, Z);
        public Vector4 WYZW => new Vector4(W, Y, Z, W);
        public Vector4 WYWX => new Vector4(W, Y, W, X);
        public Vector4 WYWY => new Vector4(W, Y, W, Y);
        public Vector4 WYWZ => new Vector4(W, Y, W, Z);
        public Vector4 WYWW => new Vector4(W, Y, W, W);
        public Vector4 WZXX => new Vector4(W, Z, X, X);
        public Vector4 WZXY
        {
            get { return new Vector4(W, Z, X, Y); }
            set
            {
                W = value.X;
                Z = value.Y;
                X = value.Z;
                Y = value.W;
            }
        }
        public Vector4 WZXZ => new Vector4(W, Z, X, Z);
        public Vector4 WZXW => new Vector4(W, Z, X, W);
        public Vector4 WZYX
        {
            get { return new Vector4(W, Z, Y, X); }
            set
            {
                W = value.X;
                Z = value.Y;
                Y = value.Z;
                X = value.W;
            }
        }
        public Vector4 WZYY => new Vector4(W, Z, Y, Y);
        public Vector4 WZYZ => new Vector4(W, Z, Y, Z);
        public Vector4 WZYW => new Vector4(W, Z, Y, W);
        public Vector4 WZZX => new Vector4(W, Z, Z, X);
        public Vector4 WZZY => new Vector4(W, Z, Z, Y);
        public Vector4 WZZZ => new Vector4(W, Z, Z, Z);
        public Vector4 WZZW => new Vector4(W, Z, Z, W);
        public Vector4 WZWX => new Vector4(W, Z, W, X);
        public Vector4 WZWY => new Vector4(W, Z, W, Y);
        public Vector4 WZWZ => new Vector4(W, Z, W, Z);
        public Vector4 WZWW => new Vector4(W, Z, W, W);
        public Vector4 WWXX => new Vector4(W, W, X, X);
        public Vector4 WWXY => new Vector4(W, W, X, Y);
        public Vector4 WWXZ => new Vector4(W, W, X, Z);
        public Vector4 WWXW => new Vector4(W, W, X, W);
        public Vector4 WWYX => new Vector4(W, W, Y, X);
        public Vector4 WWYY => new Vector4(W, W, Y, Y);
        public Vector4 WWYZ => new Vector4(W, W, Y, Z);
        public Vector4 WWYW => new Vector4(W, W, Y, W);
        public Vector4 WWZX => new Vector4(W, W, Z, X);
        public Vector4 WWZY => new Vector4(W, W, Z, Y);
        public Vector4 WWZZ => new Vector4(W, W, Z, Z);
        public Vector4 WWZW => new Vector4(W, W, Z, W);
        public Vector4 WWWX => new Vector4(W, W, W, X);
        public Vector4 WWWY => new Vector4(W, W, W, Y);
        public Vector4 WWWZ => new Vector4(W, W, W, Z);
        public Vector4 WWWW => new Vector4(W, W, W, W);

        public Vector4 RRRR => new Vector4(X, X, X, R);
        public Vector4 RRRG => new Vector4(X, X, X, G);
        public Vector4 RRRB => new Vector4(X, X, X, B);
        public Vector4 RRRA => new Vector4(X, X, X, A);
        public Vector4 RRGR => new Vector4(X, X, Y, R);
        public Vector4 RRGG => new Vector4(X, X, Y, G);
        public Vector4 RRGB => new Vector4(X, X, Y, B);
        public Vector4 RRGA => new Vector4(X, X, Y, A);
        public Vector4 RRBR => new Vector4(X, X, Z, R);
        public Vector4 RRBG => new Vector4(X, X, Z, G);
        public Vector4 RRBB => new Vector4(X, X, Z, B);
        public Vector4 RRBA => new Vector4(X, X, Z, A);
        public Vector4 RRAR => new Vector4(X, X, W, R);
        public Vector4 RRAG => new Vector4(X, X, W, G);
        public Vector4 RRAB => new Vector4(X, X, W, B);
        public Vector4 RRAA => new Vector4(X, X, W, A);
        public Vector4 RGRR => new Vector4(X, Y, X, R);
        public Vector4 RGRG => new Vector4(X, Y, X, G);
        public Vector4 RGRB => new Vector4(X, Y, X, B);
        public Vector4 RGRA => new Vector4(X, Y, X, A);
        public Vector4 RGGR => new Vector4(X, Y, Y, R);
        public Vector4 RGGG => new Vector4(X, Y, Y, G);
        public Vector4 RGGB => new Vector4(X, Y, Y, B);
        public Vector4 RGGA => new Vector4(X, Y, Y, A);
        public Vector4 RGBR => new Vector4(X, Y, Z, R);
        public Vector4 RGBG => new Vector4(X, Y, Z, G);
        public Vector4 RGBB => new Vector4(X, Y, Z, B);
        public Vector4 RGBA
        {
            get { return new Vector4(X, Y, Z, A); }
            set
            {
                X = value.X;
                Y = value.Y;
                Z = value.Z;
                W = value.W;
            }
        }
        public Vector4 RGAR => new Vector4(X, Y, W, R);
        public Vector4 RGAG => new Vector4(X, Y, W, G);
        public Vector4 RGAB
        {
            get { return new Vector4(X, Y, W, B); }
            set
            {
                X = value.X;
                Y = value.Y;
                W = value.Z;
                Z = value.W;
            }
        }
        public Vector4 RGAA => new Vector4(X, Y, W, A);
        public Vector4 RBRR => new Vector4(X, Z, X, R);
        public Vector4 RBRG => new Vector4(X, Z, X, G);
        public Vector4 RBRB => new Vector4(X, Z, X, B);
        public Vector4 RBRA => new Vector4(X, Z, X, A);
        public Vector4 RBGR => new Vector4(X, Z, Y, R);
        public Vector4 RBGG => new Vector4(X, Z, Y, G);
        public Vector4 RBGB => new Vector4(X, Z, Y, B);
        public Vector4 RBGA
        {
            get { return new Vector4(X, Z, Y, A); }
            set
            {
                X = value.X;
                Z = value.Y;
                Y = value.Z;
                W = value.W;
            }
        }
        public Vector4 RBBR => new Vector4(X, Z, Z, R);
        public Vector4 RBBG => new Vector4(X, Z, Z, G);
        public Vector4 RBBB => new Vector4(X, Z, Z, B);
        public Vector4 RBBA => new Vector4(X, Z, Z, A);
        public Vector4 RBAR => new Vector4(X, Z, W, R);
        public Vector4 RBAG
        {
            get { return new Vector4(X, Z, W, G); }
            set
            {
                X = value.X;
                Z = value.Y;
                W = value.Z;
                Y = value.W;
            }
        }
        public Vector4 RBAB => new Vector4(X, Z, W, B);
        public Vector4 RBAA => new Vector4(X, Z, W, A);
        public Vector4 RARR => new Vector4(X, W, X, R);
        public Vector4 RARG => new Vector4(X, W, X, G);
        public Vector4 RARB => new Vector4(X, W, X, B);
        public Vector4 RARA => new Vector4(X, W, X, A);
        public Vector4 RAGR => new Vector4(X, W, Y, R);
        public Vector4 RAGG => new Vector4(X, W, Y, G);
        public Vector4 RAGB
        {
            get { return new Vector4(X, W, Y, B); }
            set
            {
                X = value.X;
                W = value.Y;
                Y = value.Z;
                Z = value.W;
            }
        }
        public Vector4 RAGA => new Vector4(X, W, Y, A);
        public Vector4 RABR => new Vector4(X, W, Z, R);
        public Vector4 RABG
        {
            get { return new Vector4(X, W, Z, G); }
            set
            {
                X = value.X;
                W = value.Y;
                Z = value.Z;
                Y = value.W;
            }
        }
        public Vector4 RABB => new Vector4(X, W, Z, B);
        public Vector4 RABA => new Vector4(X, W, Z, A);
        public Vector4 RAAR => new Vector4(X, W, W, R);
        public Vector4 RAAG => new Vector4(X, W, W, G);
        public Vector4 RAAB => new Vector4(X, W, W, B);
        public Vector4 RAAA => new Vector4(X, W, W, A);
        public Vector4 GRRR => new Vector4(Y, X, X, R);
        public Vector4 GRRG => new Vector4(Y, X, X, G);
        public Vector4 GRRB => new Vector4(Y, X, X, B);
        public Vector4 GRRA => new Vector4(Y, X, X, A);
        public Vector4 GRGR => new Vector4(Y, X, Y, R);
        public Vector4 GRGG => new Vector4(Y, X, Y, G);
        public Vector4 GRGB => new Vector4(Y, X, Y, B);
        public Vector4 GRGA => new Vector4(Y, X, Y, A);
        public Vector4 GRBR => new Vector4(Y, X, Z, R);
        public Vector4 GRBG => new Vector4(Y, X, Z, G);
        public Vector4 GRBB => new Vector4(Y, X, Z, B);
        public Vector4 GRBA
        {
            get { return new Vector4(Y, X, Z, A); }
            set
            {
                Y = value.X;
                X = value.Y;
                Z = value.Z;
                W = value.W;
            }
        }
        public Vector4 GRAR => new Vector4(Y, X, W, R);
        public Vector4 GRAG => new Vector4(Y, X, W, G);
        public Vector4 GRAB
        {
            get { return new Vector4(Y, X, W, B); }
            set
            {
                Y = value.X;
                X = value.Y;
                W = value.Z;
                Z = value.W;
            }
        }
        public Vector4 GRAA => new Vector4(Y, X, W, A);
        public Vector4 GGRR => new Vector4(Y, Y, X, R);
        public Vector4 GGRG => new Vector4(Y, Y, X, G);
        public Vector4 GGRB => new Vector4(Y, Y, X, B);
        public Vector4 GGRA => new Vector4(Y, Y, X, A);
        public Vector4 GGGR => new Vector4(Y, Y, Y, R);
        public Vector4 GGGG => new Vector4(Y, Y, Y, G);
        public Vector4 GGGB => new Vector4(Y, Y, Y, B);
        public Vector4 GGGA => new Vector4(Y, Y, Y, A);
        public Vector4 GGBR => new Vector4(Y, Y, Z, R);
        public Vector4 GGBG => new Vector4(Y, Y, Z, G);
        public Vector4 GGBB => new Vector4(Y, Y, Z, B);
        public Vector4 GGBA => new Vector4(Y, Y, Z, A);
        public Vector4 GGAR => new Vector4(Y, Y, W, R);
        public Vector4 GGAG => new Vector4(Y, Y, W, G);
        public Vector4 GGAB => new Vector4(Y, Y, W, B);
        public Vector4 GGAA => new Vector4(Y, Y, W, A);
        public Vector4 GBRR => new Vector4(Y, Z, X, R);
        public Vector4 GBRG => new Vector4(Y, Z, X, G);
        public Vector4 GBRB => new Vector4(Y, Z, X, B);
        public Vector4 GBRA
        {
            get { return new Vector4(Y, Z, X, A); }
            set
            {
                Y = value.X;
                Z = value.Y;
                X = value.Z;
                W = value.W;
            }
        }
        public Vector4 GBGR => new Vector4(Y, Z, Y, R);
        public Vector4 GBGG => new Vector4(Y, Z, Y, G);
        public Vector4 GBGB => new Vector4(Y, Z, Y, B);
        public Vector4 GBGA => new Vector4(Y, Z, Y, A);
        public Vector4 GBBR => new Vector4(Y, Z, Z, R);
        public Vector4 GBBG => new Vector4(Y, Z, Z, G);
        public Vector4 GBBB => new Vector4(Y, Z, Z, B);
        public Vector4 GBBA => new Vector4(Y, Z, Z, A);
        public Vector4 GBAR
        {
            get { return new Vector4(Y, Z, W, R); }
            set
            {
                Y = value.X;
                Z = value.Y;
                W = value.Z;
                X = value.W;
            }
        }
        public Vector4 GBAG => new Vector4(Y, Z, W, G);
        public Vector4 GBAB => new Vector4(Y, Z, W, B);
        public Vector4 GBAA => new Vector4(Y, Z, W, A);
        public Vector4 GARR => new Vector4(Y, W, X, R);
        public Vector4 GARG => new Vector4(Y, W, X, G);
        public Vector4 GARB
        {
            get { return new Vector4(Y, W, X, B); }
            set
            {
                Y = value.X;
                W = value.Y;
                X = value.Z;
                Z = value.W;
            }
        }
        public Vector4 GARA => new Vector4(Y, W, X, A);
        public Vector4 GAGR => new Vector4(Y, W, Y, R);
        public Vector4 GAGG => new Vector4(Y, W, Y, G);
        public Vector4 GAGB => new Vector4(Y, W, Y, B);
        public Vector4 GAGA => new Vector4(Y, W, Y, A);
        public Vector4 GABR
        {
            get { return new Vector4(Y, W, Z, R); }
            set
            {
                Y = value.X;
                W = value.Y;
                Z = value.Z;
                X = value.W;
            }
        }
        public Vector4 GABG => new Vector4(Y, W, Z, G);
        public Vector4 GABB => new Vector4(Y, W, Z, B);
        public Vector4 GABA => new Vector4(Y, W, Z, A);
        public Vector4 GAAR => new Vector4(Y, W, W, R);
        public Vector4 GAAG => new Vector4(Y, W, W, G);
        public Vector4 GAAB => new Vector4(Y, W, W, B);
        public Vector4 GAAA => new Vector4(Y, W, W, A);
        public Vector4 BRRR => new Vector4(Z, X, X, R);
        public Vector4 BRRG => new Vector4(Z, X, X, G);
        public Vector4 BRRB => new Vector4(Z, X, X, B);
        public Vector4 BRRA => new Vector4(Z, X, X, A);
        public Vector4 BRGR => new Vector4(Z, X, Y, R);
        public Vector4 BRGG => new Vector4(Z, X, Y, G);
        public Vector4 BRGB => new Vector4(Z, X, Y, B);
        public Vector4 BRGA
        {
            get { return new Vector4(Z, X, Y, A); }
            set
            {
                Z = value.X;
                X = value.Y;
                Y = value.Z;
                W = value.W;
            }
        }
        public Vector4 BRBR => new Vector4(Z, X, Z, R);
        public Vector4 BRBG => new Vector4(Z, X, Z, G);
        public Vector4 BRBB => new Vector4(Z, X, Z, B);
        public Vector4 BRBA => new Vector4(Z, X, Z, A);
        public Vector4 BRAR => new Vector4(Z, X, W, R);
        public Vector4 BRAG
        {
            get { return new Vector4(Z, X, W, G); }
            set
            {
                Z = value.X;
                X = value.Y;
                W = value.Z;
                Y = value.W;
            }
        }
        public Vector4 BRAB => new Vector4(Z, X, W, B);
        public Vector4 BRAA => new Vector4(Z, X, W, A);
        public Vector4 BGRR => new Vector4(Z, Y, X, R);
        public Vector4 BGRG => new Vector4(Z, Y, X, G);
        public Vector4 BGRB => new Vector4(Z, Y, X, B);
        public Vector4 BGRA
        {
            get { return new Vector4(Z, Y, X, A); }
            set
            {
                Z = value.X;
                Y = value.Y;
                X = value.Z;
                W = value.W;
            }
        }
        public Vector4 BGGR => new Vector4(Z, Y, Y, R);
        public Vector4 BGGG => new Vector4(Z, Y, Y, G);
        public Vector4 BGGB => new Vector4(Z, Y, Y, B);
        public Vector4 BGGA => new Vector4(Z, Y, Y, A);
        public Vector4 BGBR => new Vector4(Z, Y, Z, R);
        public Vector4 BGBG => new Vector4(Z, Y, Z, G);
        public Vector4 BGBB => new Vector4(Z, Y, Z, B);
        public Vector4 BGBA => new Vector4(Z, Y, Z, A);
        public Vector4 BGAR
        {
            get { return new Vector4(Z, Y, W, R); }
            set
            {
                Z = value.X;
                Y = value.Y;
                W = value.Z;
                X = value.W;
            }
        }
        public Vector4 BGAG => new Vector4(Z, Y, W, G);
        public Vector4 BGAB => new Vector4(Z, Y, W, B);
        public Vector4 BGAA => new Vector4(Z, Y, W, A);
        public Vector4 BBRR => new Vector4(Z, Z, X, R);
        public Vector4 BBRG => new Vector4(Z, Z, X, G);
        public Vector4 BBRB => new Vector4(Z, Z, X, B);
        public Vector4 BBRA => new Vector4(Z, Z, X, A);
        public Vector4 BBGR => new Vector4(Z, Z, Y, R);
        public Vector4 BBGG => new Vector4(Z, Z, Y, G);
        public Vector4 BBGB => new Vector4(Z, Z, Y, B);
        public Vector4 BBGA => new Vector4(Z, Z, Y, A);
        public Vector4 BBBR => new Vector4(Z, Z, Z, R);
        public Vector4 BBBG => new Vector4(Z, Z, Z, G);
        public Vector4 BBBB => new Vector4(Z, Z, Z, B);
        public Vector4 BBBA => new Vector4(Z, Z, Z, A);
        public Vector4 BBAR => new Vector4(Z, Z, W, R);
        public Vector4 BBAG => new Vector4(Z, Z, W, G);
        public Vector4 BBAB => new Vector4(Z, Z, W, B);
        public Vector4 BBAA => new Vector4(Z, Z, W, A);
        public Vector4 BARR => new Vector4(Z, W, X, R);
        public Vector4 BARG
        {
            get { return new Vector4(Z, W, X, G); }
            set
            {
                Z = value.X;
                W = value.Y;
                X = value.Z;
                Y = value.W;
            }
        }
        public Vector4 BARB => new Vector4(Z, W, X, B);
        public Vector4 BARA => new Vector4(Z, W, X, A);
        public Vector4 BAGR
        {
            get { return new Vector4(Z, W, Y, R); }
            set
            {
                Z = value.X;
                W = value.Y;
                Y = value.Z;
                X = value.W;
            }
        }
        public Vector4 BAGG => new Vector4(Z, W, Y, G);
        public Vector4 BAGB => new Vector4(Z, W, Y, B);
        public Vector4 BAGA => new Vector4(Z, W, Y, A);
        public Vector4 BABR => new Vector4(Z, W, Z, R);
        public Vector4 BABG => new Vector4(Z, W, Z, G);
        public Vector4 BABB => new Vector4(Z, W, Z, B);
        public Vector4 BABA => new Vector4(Z, W, Z, A);
        public Vector4 BAAR => new Vector4(Z, W, W, R);
        public Vector4 BAAG => new Vector4(Z, W, W, G);
        public Vector4 BAAB => new Vector4(Z, W, W, B);
        public Vector4 BAAA => new Vector4(Z, W, W, A);
        public Vector4 ARRR => new Vector4(W, X, X, R);
        public Vector4 ARRG => new Vector4(W, X, X, G);
        public Vector4 ARRB => new Vector4(W, X, X, B);
        public Vector4 ARRA => new Vector4(W, X, X, A);
        public Vector4 ARGR => new Vector4(W, X, Y, R);
        public Vector4 ARGG => new Vector4(W, X, Y, G);
        public Vector4 ARGB
        {
            get { return new Vector4(W, X, Y, B); }
            set
            {
                W = value.X;
                X = value.Y;
                Y = value.Z;
                Z = value.W;
            }
        }
        public Vector4 ARGA => new Vector4(W, X, Y, A);
        public Vector4 ARBR => new Vector4(W, X, Z, R);
        public Vector4 ARBG
        {
            get { return new Vector4(W, X, Z, G); }
            set
            {
                W = value.X;
                X = value.Y;
                Z = value.Z;
                Y = value.W;
            }
        }
        public Vector4 ARBB => new Vector4(W, X, Z, B);
        public Vector4 ARBA => new Vector4(W, X, Z, A);
        public Vector4 ARAR => new Vector4(W, X, W, R);
        public Vector4 ARAG => new Vector4(W, X, W, G);
        public Vector4 ARAB => new Vector4(W, X, W, B);
        public Vector4 ARAA => new Vector4(W, X, W, A);
        public Vector4 AGRR => new Vector4(W, Y, X, R);
        public Vector4 AGRG => new Vector4(W, Y, X, G);
        public Vector4 AGRB
        {
            get { return new Vector4(W, Y, X, B); }
            set
            {
                W = value.X;
                Y = value.Y;
                X = value.Z;
                Z = value.W;
            }
        }
        public Vector4 AGRA => new Vector4(W, Y, X, A);
        public Vector4 AGGR => new Vector4(W, Y, Y, R);
        public Vector4 AGGG => new Vector4(W, Y, Y, G);
        public Vector4 AGGB => new Vector4(W, Y, Y, B);
        public Vector4 AGGA => new Vector4(W, Y, Y, A);
        public Vector4 AGBR
        {
            get { return new Vector4(W, Y, Z, R); }
            set
            {
                W = value.X;
                Y = value.Y;
                Z = value.Z;
                X = value.W;
            }
        }
        public Vector4 AGBG => new Vector4(W, Y, Z, G);
        public Vector4 AGBB => new Vector4(W, Y, Z, B);
        public Vector4 AGBA => new Vector4(W, Y, Z, A);
        public Vector4 AGAR => new Vector4(W, Y, W, R);
        public Vector4 AGAG => new Vector4(W, Y, W, G);
        public Vector4 AGAB => new Vector4(W, Y, W, B);
        public Vector4 AGAA => new Vector4(W, Y, W, A);
        public Vector4 ABRR => new Vector4(W, Z, X, R);
        public Vector4 ABRG
        {
            get { return new Vector4(W, Z, X, G); }
            set
            {
                W = value.X;
                Z = value.Y;
                X = value.Z;
                Y = value.W;
            }
        }
        public Vector4 ABRB => new Vector4(W, Z, X, B);
        public Vector4 ABRA => new Vector4(W, Z, X, A);
        public Vector4 ABGR
        {
            get { return new Vector4(W, Z, Y, R); }
            set
            {
                W = value.X;
                Z = value.Y;
                Y = value.Z;
                X = value.W;
            }
        }
        public Vector4 ABGG => new Vector4(W, Z, Y, G);
        public Vector4 ABGB => new Vector4(W, Z, Y, B);
        public Vector4 ABGA => new Vector4(W, Z, Y, A);
        public Vector4 ABBR => new Vector4(W, Z, Z, R);
        public Vector4 ABBG => new Vector4(W, Z, Z, G);
        public Vector4 ABBB => new Vector4(W, Z, Z, B);
        public Vector4 ABBA => new Vector4(W, Z, Z, A);
        public Vector4 ABAR => new Vector4(W, Z, W, R);
        public Vector4 ABAG => new Vector4(W, Z, W, G);
        public Vector4 ABAB => new Vector4(W, Z, W, B);
        public Vector4 ABAA => new Vector4(W, Z, W, A);
        public Vector4 AARR => new Vector4(W, W, X, R);
        public Vector4 AARG => new Vector4(W, W, X, G);
        public Vector4 AARB => new Vector4(W, W, X, B);
        public Vector4 AARA => new Vector4(W, W, X, A);
        public Vector4 AAGR => new Vector4(W, W, Y, R);
        public Vector4 AAGG => new Vector4(W, W, Y, G);
        public Vector4 AAGB => new Vector4(W, W, Y, B);
        public Vector4 AAGA => new Vector4(W, W, Y, A);
        public Vector4 AABR => new Vector4(W, W, Z, R);
        public Vector4 AABG => new Vector4(W, W, Z, G);
        public Vector4 AABB => new Vector4(W, W, Z, B);
        public Vector4 AABA => new Vector4(W, W, Z, A);
        public Vector4 AAAR => new Vector4(W, W, W, R);
        public Vector4 AAAG => new Vector4(W, W, W, G);
        public Vector4 AAAB => new Vector4(W, W, W, B);
        public Vector4 AAAA => new Vector4(W, W, W, A);

        public Vector4(float value)
        {
            X = value;
            Y = value;
            Z = value;
            W = value;
        }

        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Vector4(float x, float y, Vector2 vec2)
        {
            X = x;
            Y = y;
            Z = vec2.X;
            W = vec2.Y;
        }
        public Vector4(float x, Vector2 vec2, float w)
        {
            X = x;
            Y = vec2.X;
            Z = vec2.Y;
            W = w;
        }

        public Vector4(Vector2 vec2, float z, float w)
        {
            X = vec2.X;
            Y = vec2.Y;
            Z = z;
            W = w;
        }

        public Vector4(Vector2 vec21, Vector2 vec22)
        {
            X = vec21.X;
            Y = vec21.Y;
            Z = vec22.X;
            W = vec22.Y;
        }

        public Vector4(float x, Vector3 vec3)
        {
            X = x;
            Y = vec3.X;
            Z = vec3.Y;
            W = vec3.Z;
        }

        public Vector4(Vector3 vec3, float w)
        {
            X = vec3.X;
            Y = vec3.Y;
            Z = vec3.Z;
            W = w;
        }

        public static Vector4 operator +(Vector4 a, Vector4 b)
        {
            return new Vector4()
            {
                X = a.X + b.X,
                Y = a.Y + b.Y,
                Z = a.Z + b.Z,
                W = a.W + b.W,
            };
        }
        public static Vector4 operator +(Vector4 a, int b)
        {
            return new Vector4()
            {
                X = (float)(a.X + b),
                Y = (float)(a.Y + b),
                Z = (float)(a.Z + b),
                W = (float)(a.W + b),
            };
        }

        public static Vector4 operator +(int a, Vector4 b)
        {
            return new Vector4()
            {
                X = (float)(a + b.X),
                Y = (float)(a + b.Y),
                Z = (float)(a + b.Z),
                W = (float)(a + b.W),
            };
        }
        public static Vector4 operator +(Vector4 a, float b)
        {
            return new Vector4()
            {
                X = (float)(a.X + b),
                Y = (float)(a.Y + b),
                Z = (float)(a.Z + b),
                W = (float)(a.W + b),
            };
        }

        public static Vector4 operator +(float a, Vector4 b)
        {
            return new Vector4()
            {
                X = (a + b.X),
                Y = (a + b.Y),
                Z = (a + b.Z),
                W = (a + b.W),
            };
        }
        public static Vector4 operator +(Vector4 a, double b)
        {
            return new Vector4()
            {
                X = (float)(a.X + b),
                Y = (float)(a.Y + b),
                Z = (float)(a.Z + b),
                W = (float)(a.W + b),
            };
        }

        public static Vector4 operator +(double a, Vector4 b)
        {
            return new Vector4()
            {
                X = (float)(a + b.X),
                Y = (float)(a + b.Y),
                Z = (float)(a + b.Z),
                W = (float)(a + b.W),
            };
        }
        public static Vector4 operator -(Vector4 a, Vector4 b)
        {
            return new Vector4()
            {
                X = a.X - b.X,
                Y = a.Y - b.Y,
                Z = a.Z - b.Z,
                W = a.W - b.W,
            };
        }
        public static Vector4 operator -(Vector4 a, int b)
        {
            return new Vector4()
            {
                X = (float)(a.X - b),
                Y = (float)(a.Y - b),
                Z = (float)(a.Z - b),
                W = (float)(a.W - b),
            };
        }

        public static Vector4 operator -(int a, Vector4 b)
        {
            return new Vector4()
            {
                X = (float)(a - b.X),
                Y = (float)(a - b.Y),
                Z = (float)(a - b.Z),
                W = (float)(a - b.W),
            };
        }
        public static Vector4 operator -(Vector4 a, float b)
        {
            return new Vector4()
            {
                X = (float)(a.X - b),
                Y = (float)(a.Y - b),
                Z = (float)(a.Z - b),
                W = (float)(a.W - b),
            };
        }

        public static Vector4 operator -(float a, Vector4 b)
        {
            return new Vector4()
            {
                X = (a - b.X),
                Y = (a - b.Y),
                Z = (a - b.Z),
                W = (a - b.W),
            };
        }
        public static Vector4 operator -(Vector4 a, double b)
        {
            return new Vector4()
            {
                X = (float)(a.X - b),
                Y = (float)(a.Y - b),
                Z = (float)(a.Z - b),
                W = (float)(a.W - b),
            };
        }

        public static Vector4 operator -(double a, Vector4 b)
        {
            return new Vector4()
            {
                X = (float)(a - b.X),
                Y = (float)(a - b.Y),
                Z = (float)(a - b.Z),
                W = (float)(a - b.W),
            };
        }
        public static Vector4 operator *(Vector4 a, Vector4 b)
        {
            return new Vector4()
            {
                X = a.X * b.X,
                Y = a.Y * b.Y,
                Z = a.Z * b.Z,
                W = a.W * b.W,
            };
        }
        public static Vector4 operator *(Vector4 a, int b)
        {
            return new Vector4()
            {
                X = (float)(a.X * b),
                Y = (float)(a.Y * b),
                Z = (float)(a.Z * b),
                W = (float)(a.W * b),
            };
        }

        public static Vector4 operator *(int a, Vector4 b)
        {
            return new Vector4()
            {
                X = (float)(a * b.X),
                Y = (float)(a * b.Y),
                Z = (float)(a * b.Z),
                W = (float)(a * b.W),
            };
        }
        public static Vector4 operator *(Vector4 a, float b)
        {
            return new Vector4()
            {
                X = (float)(a.X * b),
                Y = (float)(a.Y * b),
                Z = (float)(a.Z * b),
                W = (float)(a.W * b),
            };
        }

        public static Vector4 operator *(float a, Vector4 b)
        {
            return new Vector4()
            {
                X = (a * b.X),
                Y = (a * b.Y),
                Z = (a * b.Z),
                W = (a * b.W),
            };
        }
        public static Vector4 operator *(Vector4 a, double b)
        {
            return new Vector4()
            {
                X = (float)(a.X * b),
                Y = (float)(a.Y * b),
                Z = (float)(a.Z * b),
                W = (float)(a.W * b),
            };
        }

        public static Vector4 operator *(double a, Vector4 b)
        {
            return new Vector4()
            {
                X = (float)(a * b.X),
                Y = (float)(a * b.Y),
                Z = (float)(a * b.Z),
                W = (float)(a * b.W),
            };
        }
        public static Vector4 operator /(Vector4 a, Vector4 b)
        {
            return new Vector4()
            {
                X = a.X / b.X,
                Y = a.Y / b.Y,
                Z = a.Z / b.Z,
                W = a.W / b.W,
            };
        }
        public static Vector4 operator /(Vector4 a, int b)
        {
            return new Vector4()
            {
                X = (float)(a.X / b),
                Y = (float)(a.Y / b),
                Z = (float)(a.Z / b),
                W = (float)(a.W / b),
            };
        }

        public static Vector4 operator /(int a, Vector4 b)
        {
            return new Vector4()
            {
                X = (float)(a / b.X),
                Y = (float)(a / b.Y),
                Z = (float)(a / b.Z),
                W = (float)(a / b.W),
            };
        }
        public static Vector4 operator /(Vector4 a, float b)
        {
            return new Vector4()
            {
                X = (float)(a.X / b),
                Y = (float)(a.Y / b),
                Z = (float)(a.Z / b),
                W = (float)(a.W / b),
            };
        }

        public static Vector4 operator /(float a, Vector4 b)
        {
            return new Vector4()
            {
                X = (a / b.X),
                Y = (a / b.Y),
                Z = (a / b.Z),
                W = (a / b.W),
            };
        }
        public static Vector4 operator /(Vector4 a, double b)
        {
            return new Vector4()
            {
                X = (float)(a.X / b),
                Y = (float)(a.Y / b),
                Z = (float)(a.Z / b),
                W = (float)(a.W / b),
            };
        }

        public static Vector4 operator /(double a, Vector4 b)
        {
            return new Vector4()
            {
                X = (float)(a / b.X),
                Y = (float)(a / b.Y),
                Z = (float)(a / b.Z),
                W = (float)(a / b.W),
            };
        }

        public static implicit operator System.Numerics.Vector4(Vector4 vec)
        {
            return new System.Numerics.Vector4(vec.X, vec.Y, vec.Z, vec.W);
        }

        public static explicit operator Vector4(System.Numerics.Vector4 vec)
        {
            return new Vector4(vec.X, vec.Y, vec.Z, vec.W);
        }
    }
}

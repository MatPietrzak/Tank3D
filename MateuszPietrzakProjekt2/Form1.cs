using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MateuszPietrzakProjekt2
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        int width, height;
        Camera camera;
        List<Point> StaticPoints;
        //Środek ciężkości figury
        Point MidP;
        Point Light,StaticLight;
        double k; 
        List<Point> DynamicPoints;
        List<Triangle> DynamicTriangle;
        private void Form1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)Keys.D)
            {
                camera.MoveM[0,3] += -5;

            }
            if (e.KeyValue == (int)Keys.A)
            {
                camera.MoveM[0,3] += 5;

            }
            if (e.KeyValue == (int)Keys.S)
            {
                camera.MoveM[1,3] += -5;

            }
            if (e.KeyValue == (int)Keys.W)
            {
                camera.MoveM[1,3] += 5;

            }
            if (e.KeyValue == (int)Keys.Q)
            {
                camera.MoveM[2,3] += +5;

            }
            if (e.KeyValue == (int)Keys.E)
            {
                camera.MoveM[2,3] += -5;

            }
            if (e.KeyValue == (int)Keys.U)
            {
                camera.Rot.Z += (float)(Math.PI / 90);

            }
            if (e.KeyValue == (int)Keys.O)
            {
                camera.Rot.Z += -(float)(Math.PI / 90);

            }
            if (e.KeyValue == (int)Keys.L)
            {
                camera.Rot.X += -(float)(Math.PI / 90);

            }
            if (e.KeyValue == (int)Keys.J)
            {
                camera.Rot.X += (float)(Math.PI / 90);

            }
            if (e.KeyValue == (int)Keys.K)
            {
                camera.Rot.Y += (float)(Math.PI / 90);

            }
            if (e.KeyValue == (int)Keys.I)
            {
                camera.Rot.Y += -(float)(Math.PI / 90);

            }
            if (e.KeyValue == (int)Keys.H)
            {
                camera.MoveL[0, 3] += -5;

            }
            if (e.KeyValue == (int)Keys.F)
            {
                camera.MoveL[0, 3] += 5;

            }
            if (e.KeyValue == (int)Keys.G)
            {
                camera.MoveL[1, 3] += -5;

            }
            if (e.KeyValue == (int)Keys.T)
            {
                camera.MoveL[1, 3] += 5;

            }
            if (e.KeyValue == (int)Keys.R)
            {
                camera.MoveL[2, 3] += +5;

            }
            if (e.KeyValue == (int)Keys.Y)
            {
                camera.MoveL[2, 3] += -5;

            }
            label2.Text = "Środek ciężkości: " + (camera.MoveM * MidP).ToString();
            Light = camera.MoveL * StaticLight;
            //label3.Text = "Źródło światła: " + (Light).ToString();
            DynamicPoints = StaticToDynamic();

            camera.Rotation(DynamicPoints, StaticPoints,MidP);
            camera.Przesuniecie(DynamicPoints, StaticPoints);

            camera.RzutowaniePerspektywiczne(DynamicPoints);
            DynamicTriangle = MakeTriangle(DynamicPoints);
            DynamicTriangle.Sort();
            DrawObject(bmp, DynamicPoints);
        }
        public Form1()
        {

            
            StaticPoints = MakeObject();
            MidP = MidPoint(StaticPoints);
            DynamicPoints = StaticToDynamic();
            DynamicTriangle = MakeTriangle(DynamicPoints);
            InitializeComponent();

            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            width = pictureBox.Width;
            height = pictureBox.Height;
            Light = new Point(100, 100, 100);
            StaticLight = new Point(100, 100, 100);
            k =0.7;
            camera = new Camera((float) width / height);
            label1.Text = "Sterowanie\n W,S,A,D,Q,E - Poruszanie kamerą\n J,K,L,U,O - Obrót obiektu\n F,G,H,T,R,Y - Poruszanie światła";
            label2.Text = "Środek ciężkości: " + (camera.MoveM * MidP).ToString();
            //           label3.Text = "Źródło światła: " + (Light).ToString();

            DynamicPoints = StaticToDynamic();

            camera.Rotation(DynamicPoints, StaticPoints, MidP);
            camera.Przesuniecie(DynamicPoints, StaticPoints);

            camera.RzutowaniePerspektywiczne(DynamicPoints);
            DynamicTriangle = MakeTriangle(DynamicPoints);
            DynamicTriangle.Sort();
            DrawObject(bmp, DynamicPoints);
        }
        public List<Point> MakeObject()
        {
            List<Point> p = new List<Point>();
            p.Add(new Point(-50, -20, 400));
            p.Add(new Point(-50, 20, 400));
            p.Add(new Point(50,-20, 400));
            p.Add(new Point(50, 20,400));
            p.Add(new Point(50, 20, 500));
            p.Add(new Point(-50, 20, 500));
            p.Add(new Point(50, -20, 500));
            p.Add(new Point(-50, -20, 500));

            p.Add(new Point(-30, 20, 420));
            p.Add(new Point(-30, 40, 420));
            p.Add(new Point(30, 20, 420));
            p.Add(new Point(30, 40, 420));
            p.Add(new Point(30, 40, 480));
            p.Add(new Point(-30, 40, 480));
            p.Add(new Point(30, 20, 480));
            p.Add(new Point(-30, 20, 480));

            p.Add(new Point(30, 25, 445));
            p.Add(new Point(30, 35, 445));
            p.Add(new Point(70, 25, 445));
            p.Add(new Point(70, 35, 445));
            p.Add(new Point(70, 35, 455));
            p.Add(new Point(30, 35, 455));
            p.Add(new Point(70, 25, 455));
            p.Add(new Point(30, 25, 455));
            return p;
        }
        public List<Triangle> MakeTriangle(List<Point> p)
        {
            List<Triangle> T = new List<Triangle>();
            T.Add(new Triangle( p[0], p[1], p[2]));
            T.Add(new Triangle(p[3], p[1], p[2]));
            T.Add(new Triangle(p[4], p[5], p[6]));
            T.Add(new Triangle(p[7], p[5], p[6]));
            T.Add(new Triangle(p[0], p[1], p[5]));
            T.Add(new Triangle(p[0], p[7], p[5]));
            T.Add(new Triangle(p[2], p[3], p[4]));
            T.Add(new Triangle(p[2], p[4], p[6]));
            T.Add(new Triangle(p[1], p[3], p[5]));
            T.Add(new Triangle(p[5], p[3], p[4]));
            T.Add(new Triangle(p[0], p[2], p[6]));
            T.Add(new Triangle(p[0], p[7], p[6]));

            T.Add(new Triangle(p[8], p[9], p[10]));
            T.Add(new Triangle(p[11], p[8], p[10]));
            T.Add(new Triangle(p[12], p[13], p[14]));
            T.Add(new Triangle(p[15], p[13], p[14]));
            T.Add(new Triangle(p[8], p[9], p[13]));
            T.Add(new Triangle(p[8], p[15], p[13]));
            T.Add(new Triangle(p[10], p[11], p[12]));
            T.Add(new Triangle(p[10], p[12], p[14]));
            T.Add(new Triangle(p[9], p[11], p[13]));
            T.Add(new Triangle(p[13], p[11], p[12]));
            T.Add(new Triangle(p[8], p[10], p[14]));
            T.Add(new Triangle(p[8], p[15], p[14]));

            T.Add(new Triangle(p[16], p[17], p[18]));
            T.Add(new Triangle(p[19], p[16], p[18]));
            T.Add(new Triangle(p[20], p[21], p[22]));
            T.Add(new Triangle(p[23], p[21], p[22]));
            T.Add(new Triangle(p[16], p[17], p[21]));
            T.Add(new Triangle(p[16], p[23], p[21]));
            T.Add(new Triangle(p[18], p[19], p[20]));
            T.Add(new Triangle(p[18], p[20], p[22]));
            T.Add(new Triangle(p[17], p[19], p[21]));
            T.Add(new Triangle(p[21], p[19], p[20]));
            T.Add(new Triangle(p[8], p[18], p[22]));
            T.Add(new Triangle(p[8], p[23], p[22]));
            return T;
        }
        public void DrawObject(Bitmap bmp,List<Point> Points)
        {
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            for (int i = 0; i < Points.Count; i++)
            {
                Points[i].X *= bmp.Width;
                Points[i].Y *= bmp.Height;
                Points[i].X += bmp.Width/2;
                Points[i].Y += bmp.Height/2;
            }

            foreach (Triangle i in DynamicTriangle)
            {
                Point VLight = new Point();

                VLight = i.a - Light;

                i.NormalV();
                VLight.Normalizuj();


                float cos = Math.Abs(VLight.X*i.Normalny.X+ VLight.Y * i.Normalny.Y+ VLight.Z * i.Normalny.Z);
                double Col = 255 *cos*k;

                Color Kolor = new Color();
                Kolor = Color.FromArgb((byte)Col, 0, 100);
                if (i.a.Y<=i.b.Y && i.b.Y<=i.c.Y) FillTriangle(bmp, i.a, i.b, i.c,Kolor);
                if (i.b.Y <= i.a.Y && i.a.Y <= i.c.Y) FillTriangle(bmp, i.b, i.a, i.c, Kolor);
                if (i.a.Y <= i.c.Y && i.c.Y <= i.b.Y) FillTriangle(bmp, i.a, i.c, i.b, Kolor);
                if (i.c.Y <= i.a.Y && i.a.Y <= i.b.Y) FillTriangle(bmp, i.c, i.a, i.b, Kolor);
                if (i.c.Y <= i.b.Y && i.b.Y <= i.a.Y) FillTriangle(bmp, i.c, i.b, i.a, Kolor);
                if (i.b.Y <= i.c.Y && i.c.Y <= i.a.Y) FillTriangle(bmp, i.b, i.c, i.a, Kolor);
            }
            pictureBox.Image = bmp;



        }
        public void FillTriangle(Bitmap bmp,Point A,Point B,Point C,Color Kolor)
        {
            float dX1, dX2, dX3;

            if (B.Y - A.Y > 0) dX1 = (B.X - A.X) / (B.Y - A.Y);
            else dX1 = 0;
            if (C.Y - A.Y > 0) dX2 = (C.X - A.X) / (C.Y - A.Y);
            else dX2 = 0;
            if (C.Y - B.Y > 0) dX3 = (C.X - B.X) / (C.Y - B.Y);
            else dX3 = 0;

            Point S, E;
            S = new Point(A.X, A.Y, A.Z);
            E = new Point(A.X, A.Y, A.Z);
            if (dX1 > dX2)
            {
                while (S.Y <= B.Y)
                {
                    DrawLine(bmp, S, E,Kolor);
                    S.Y++; E.Y++; S.X += dX2; E.X += dX1;

                }
                E = new Point(B.X,B.Y,B.Z);
                while (S.Y <= C.Y)
                { 
                    DrawLine(bmp,S, E, Kolor);
                    S.Y++; E.Y++; S.X += dX2; E.X += dX3;
                }
            }
            else
            {
                while (S.Y <=B.Y)
                {
                    DrawLine(bmp, S, E, Kolor);
                    S.Y++; E.Y++; S.X += dX1; E.X += dX2;
                }
                S = new Point(B.X,B.Y,B.Z);
                while (S.Y <= C.Y)
                {
                    DrawLine(bmp, S, E, Kolor);
                    S.Y++; E.Y++; S.X += dX3; E.X += dX2;
                }
            }
        }

        public void DrawLine(Bitmap bmp,Point p1,Point p2,Color Col)
        {

            float dy, dx, m;
            dx = p2.X - p1.X;
            dy = p2.Y - p1.Y;
            m = dy / dx;
            if (Math.Abs(m) < 1)
            {
                if (dx < 0) SwapPoints(ref p1,ref p2);
                float y = p1.Y;
                for (int x = (int)p1.X; x < p2.X; x++)
                {
                    if(x<bmp.Width && x>0 && y<bmp.Height && y>0)
                    {
                        bmp.SetPixel(x, (int)(Math.Floor(y)+0.5), Col);
                        y += m;
                    }

                }
            }
            else
            {
                if (dy < 0) SwapPoints(ref p1,ref p2);
                float x = p1.X;
                for (int y = (int)p1.Y; y < p2.Y; y++)
                {
                    if (x < bmp.Width && x > 0 && y < bmp.Height && y > 0)
                    {
                        bmp.SetPixel((int)(Math.Floor(x)+0.5), y, Col);
                        x += 1 / m;
                    }
                }
            }
        }
        public void SwapPoints(ref Point p1,ref Point p2)
        {
            Point p3 = p1;
            p1 = p2;
            p2 = p3;
        }
        public List<Point> StaticToDynamic()
        {
            List<Point> p = new List<Point>();
            foreach (Point point in StaticPoints)
            {
                p.Add(new Point(point.X,point.Y,point.Z));
            }
            return p;
        }
        public Point MidPoint(List<Point> P)
        {
            Point Mid = new Point(0, 0, 0);
            foreach (Point p in P)
            {
                Mid.X += p.X;
                Mid.Y += p.Y;
                Mid.Z += p.Z;
            }
            Mid.X /= P.Count;
            Mid.Y /= P.Count;
            Mid.Z /= P.Count;
            return Mid;
        }


    }
    public class Point
    {
        public float X;
        public float Y;
        public float Z;
        public float W;
        public Point()
        {
            X = 0;
            Y = 0;
            Z = 0;
            W = 1;
        }
        public Point(float x, float y,float z)
        {
            X = x;
            Y = y;
            Z = z;
            W = 1;
        }
        public static Point operator/(Point P,float x)
        {
            Point b = new Point();
            b.X = P.X / x;
            b.Y = P.Y / x;
            b.Z = P.Z / x;
            b.W = P.W / x;
            return b;
        }
        public static Point operator-(Point a,Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static Point operator *(Point a, Point b)
        {
            return new Point(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }
        public static Point IloczynWektorowy(Point a,Point b)
        {
            return new Point(a.Y * b.Z - b.Y * a.Z, b.X * a.Z - a.X * b.Z, a.X * b.Y - b.X * a.Y);
        }
        public void Normalizuj()
        {
            float Length = (float)Math.Sqrt(X * X + Y * Y + Z * Z);
            X /= Length;
            Y /= Length;
            Z /= Length;
        }
        public Point Floor(Point a)
        {
            return new Point((float)Math.Floor(a.X), (float)Math.Floor(a.Y), (float)Math.Floor(a.Z));
        }
        public override string ToString()
        {
            return "X" + X + " Y" + Y + " Z" + Z;
        }
    }
    public class Triangle:IComparable
    {
        public List<Point> p = new List<Point>();
        public Point a;
        public Point b;
        public Point c;
        public Point Mid;
        public Point Normalny;
        public int length;
        public Triangle(Point a,Point b,Point c)
        {
            this.a=a;
            this.b=b;
            this.c=c;
            Mid = new Point(a.X + b.X + c.X / 3, a.Y + b.Y + c.Y / 3, a.Z + b.Z + c.Z / 3);
            length = 3;
            Normalny = new Point();
        }

        public int CompareTo(Object obj)
        {
            Triangle T = (Triangle) obj;
            if (this.Mid.Z > T.Mid.Z) return -1;
            if (this.Mid.Z < T.Mid.Z) return 1;
            if (this.Mid.X > T.Mid.X) return -1;
            if (this.Mid.X < T.Mid.X) return 1;
            if (this.Mid.Y > T.Mid.Y) return -1;
            if (this.Mid.Y < T.Mid.Y) return 1;
            return 0;
        }
        public void NormalV()
        {
            Point V1 = new Point();
            V1 = a - b;
            Point V2 = new Point();
            V2 = a - c;
            Point Temp = new Point();
            Temp = Point.IloczynWektorowy(V1, V2);
            Temp.Normalizuj();
            Normalny = Temp;
        }
    }
    public class Camera
    {
        public Point Rot;
        float Znear,Zfar,Zrange;
        float AspectRatio;
        float FOV;


        public Matrix PerspectiveM;
        public Matrix MoveM, MoveL;
        public Matrix RotX,RotY,RotZ;
        public Camera(float ar)
        {
            this.Rot = (new Point(0, 0, 0));
            Znear = 200;
            Zfar = 1000;
            Zrange = Znear - Zfar;
            AspectRatio = ar;
            FOV = (float)Math.Tan(40) / 2;

            //Macierz perspektywy
            PerspectiveM = new Matrix(4, 4);
            PerspectiveM[0, 0] = 1/(FOV*AspectRatio);
            PerspectiveM[1, 1] = 1/FOV;
            PerspectiveM[2, 2] = (-Znear-Zfar)/Zrange;
            PerspectiveM[2, 3] = 2 * Zfar * Znear / Zrange;
            PerspectiveM[3, 2] = 1;
            //Macierz przesunięcia
            MoveM = new Matrix(4, 4);
            MoveM[0, 0] = 1;
            MoveM[1, 1] = 1;
            MoveM[2, 2] = 1;
            MoveM[3, 3] = 1;
            //Macierze obrotu
            RotX = new Matrix(4, 4);
            RotY = new Matrix(4, 4);
            RotZ = new Matrix(4, 4);
            //Macierz przesunięcia światła
            MoveL = new Matrix(4, 4);
            MoveL[0, 0] = 1;
            MoveL[1, 1] = 1;
            MoveL[2, 2] = 1;
            MoveL[3, 3] = 1;



        }
        public void RzutowaniePerspektywiczne(List<Point> DP)
        {
            for (int i = 0; i < DP.Count; i++)
            {
                DP[i] = PerspectiveM * DP[i];
                DP[i] /= DP[i].W;
            }
        }
        public void Przesuniecie(List<Point> DP,List<Point> SP)
        {
            for (int i = 0; i < DP.Count; i++)
            {
                DP[i] = MoveM * DP[i];

            }
 
        }

        public void Rotation(List<Point> DP, List<Point> SP,Point MidP)
        {

            RotZ[0, 0] = (float)Math.Cos(Rot.Z);
            RotZ[0, 1] = -(float)Math.Sin(Rot.Z);
            RotZ[1, 0] = (float)Math.Sin(Rot.Z);
            RotZ[1, 1] = (float)Math.Cos(Rot.Z);
            RotZ[2, 2] = 1;
            RotZ[3, 3] = 1;

            RotX[1, 1] = (float)Math.Cos(Rot.X);
            RotX[1, 2] = -(float)Math.Sin(Rot.X);
            RotX[2, 1] = (float)Math.Sin(Rot.X);
            RotX[2, 2] = (float)Math.Cos(Rot.X);
            RotX[0, 0] = 1;
            RotX[3, 3] = 1;


            RotY[0, 0] = (float)Math.Cos(Rot.Y);
            RotY[0, 2] = -(float)Math.Sin(Rot.Y);
            RotY[2, 0] = (float)Math.Sin(Rot.Y);
            RotY[2, 2] = (float)Math.Cos(Rot.Y);
            RotY[1, 1] = 1;
            RotY[3, 3] = 1;

            for (int i = 0; i < DP.Count; i++)
            {
                DP[i] -= MidP;
                DP[i] = RotZ * RotY * RotX * DP[i];
                DP[i] += MidP;
            }
        }
    }
    public class Matrix
    {
        float[,] T;
        int w;
        int k;
        public Matrix(int w, int k)
        {
            T = new float[w, k];
            this.w = w;
            this.k = k;
        }
        public float this[int w,int k]
        {
            get { return T[w, k]; }
            set { T[w, k] = value; }
        }
        public static Point operator*(Matrix A,Point p)
        {
            Point b = new Point();
            b.X = p.X * A[0, 0] + p.Y * A[0, 1] + p.Z * A[0, 2] + p.W * A[0, 3];
            b.Y = p.X * A[1, 0] + p.Y * A[1, 1] + p.Z * A[1, 2] + p.W * A[1, 3];
            b.Z = p.X * A[2, 0] + p.Y * A[2, 1] + p.Z * A[2, 2] + p.W * A[2, 3];
            b.W = p.X * A[3, 0] + p.Y * A[3, 1] + p.Z * A[3, 2] + p.W * A[3, 3];
            return b;
        }
        public static Matrix operator*(Matrix A,Matrix B)
        {
            Matrix c = new Matrix(A.w, B.k);
            for (int i = 0; i < A.w; i++)
            {
                for (int j = 0; j < B.k; j++)
                {
                    for (int k = 0; k < A.k; k++)
                    {
                        c[i, j] += A[i, k] * B[j, k];
                    }
                }
            }
            return c;
        }
    }
}

namespace Server.Game;

public class Vector //вектора и математические действия с ними
    {
        double x;
        double y;
        public Vector() //Инициализация
        {
            this.x = 0;
            this.y = 0;
        }
        public Vector(double x, double y) //Перегрузка инициализации
        {
            this.x = x;
            this.y = y;
        }
        public double Average() => Vector.Average(this); //среднее значения вектора(что-то вроде нормализации)
        public double X => x; //Получаем координаты
        public double Y => y;
        public Vector Invert() => Vector.Invert(this);
        public static Vector Invert(Vector a) => new Vector(-a.x, -a.y); //смена направления вектора
        public override string ToString() => $"X = {x}, Y = {y}"; 
        public static Vector operator +(Vector a, Vector b) => new Vector(a.x+b.x,a.y+b.y); //действия с векторами сложения векторов
        public static Vector operator -(Vector a, Vector b) => new Vector(a.x - b.x, a.y - b.y); //вычитание векторов
        public static Vector operator /(Vector a, int b) => new Vector(a.x / b, a.y / b); //деление вектора на целое число
        public static Vector operator /(Vector a, double b) => new Vector((int)(a.x / b), (int)(a.y / b)); //деление вектора на число с запятой
        public static Vector operator *(Vector a, int b) => new Vector(a.x * b, a.y * b); //умножение вектора на число
        public static Vector operator *(Vector a, double b) => new Vector((a.x * b), (a.y * b)); //умножение вектора на число с точкой

        public static bool operator <(Vector a, int b) => a.Average() < 0; //сравнение вектора и числа
        public static bool operator >(Vector a, int b) => a.Average() > 0;
        public static bool operator ==(Vector a, int b) => a.Average() == 0;
        public static bool operator !=(Vector a, int b) => a.Average() != 0;
        public static double Distance(Vector a, Vector b) => Math.Sqrt(Math.Pow(b.x-a.x,2)+Math.Pow(b.y-a.y,2)); //дистанция между векторами
        public static double Average(Vector a) => (a.x+a.y)/2; //усреднённое значения вектора (импровизированная нормализация)   

        public static double Dot(Vector a, Vector b) => a.x * b.x + a.y * b.y;
    }
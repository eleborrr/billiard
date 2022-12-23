using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billiards.Game
{
    class Ball : Transform, IGameObject //наследуем класс трансформ и интерфейс игрового объекта
    {
        Vector speed = new Vector(); //скорость шара
        Brush brush; //цвет шара
        bool main = false; //основной шар или нет
        public bool isMain => main; //возвращаем главный или нет
        public Ball(int x, int y, int sx, int sy, Brush brush) //инициализируем
        {
            this.brush = brush; //задаём цвет
            SetPosition(new Vector(x, y)); //задаём позицию (она наследуется из класса Transform)
            SetSize(new Vector(sx, sy));   //задаём размер (из класса Transform)             
        }
        public Ball(int x, int y, int sx, int sy, Brush brush, bool main) //инициализация для главного шара
        {
            this.brush = brush; //задаём цвет
            SetPosition(new Vector(x, y)); //задаём позицию (она наследуется из класса Transform)
            SetSize(new Vector(sx, sy));   //задаём размер (из класса Transform)   
            this.main = main; //устанавливаем главный шар или нет
        }
        public void SetSpeed(Vector speed) => this.speed = speed; //задаём скорость
        public void SetSpeed(double x, double y) => this.speed = new Vector(x, y); //задаём скорость
        public Vector GetSpeed() => this.speed; //возвращаем скорость шара

        public void AddToSpeed(Vector add) => this.speed += add;


        public void Draw(Graphics g) //рисуем шар
        {
            AddPosition(speed); //двигаем шар по его скорости
            speed = speed * 0.97; //уменьшаем на 3% скорость шара каждый кадр
            if (Math.Abs(speed.Average()) < 1) //если шар движется то применяем к нему скорость
                speed = new Vector();
            g.FillEllipse(brush,
            (int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
        }
    }
}

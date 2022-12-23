namespace Server.Game;

public class Transform //Класс для хранения позиции и тд
{
    Vector position; //позиция        
    Vector size; //размер
    public Vector Position => position; // возвращаем позицию
    public Vector Size => size; //возвращаем размер
    public void SetPosition(Vector vector) => position = vector; //установка позиции
    public void SetPosition(double x, double y) => position = new Vector(x, y);

    public void AddPosition(Vector vector) => position = position + vector; //движение
    public void SetSize(Vector vector) => size = vector; //установка размера объекта
    public Vector GetCenter() => new Vector(size.X/2 + position.X, size.Y/2 + position.Y); //берём центр объекта
    public bool Collision(Vector other) => Vector.Distance(GetCenter(), other) < size.Average(); //проверка на столкновение

    public double Radius() => Vector.Distance(size, GetCenter());
}
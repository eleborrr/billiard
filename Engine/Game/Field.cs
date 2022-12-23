namespace Server.Game;

class Field //игровое поле
{
    Vector startPoint; //начальная точка
    Vector endPoint; //конечная точка

    public Field(Vector startPoint, Vector endPoint) //инициализация поля
    {
        this.startPoint = startPoint;
        this.endPoint = endPoint;
    }
    //проверка на находения внутри поля
    public bool OutCheck(Vector point) => point.X < startPoint.X || point.Y < startPoint.Y || point.X > endPoint.X || point.Y > endPoint.Y;
}
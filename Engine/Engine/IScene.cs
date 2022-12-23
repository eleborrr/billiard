using System.Drawing;

namespace Server.Game;

public interface IScene //интерфейс для сцены
{
    public void DrawBack(Graphics g, int x, int y); //метод отрисовка фона сцены
    public void DrawObjects(Graphics g); //метод отрисовки объектов

}
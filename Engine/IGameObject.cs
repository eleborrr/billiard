using System.Collections.Generic;
using System.Drawing;

namespace Billiards
{
    interface IGameObject //интерфейс для игровых объектов
    {
        void Draw(Graphics g); //метод интерфейса для отрисовки
        bool Collision(Vector other); //метод для проверки столкновений
    }
}

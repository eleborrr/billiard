using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billiards.Engine
{
    interface IScene //интерфейс для сцены
    {
        void DrawBack(Graphics g, int x, int y); //метод отрисовка фона сцены
        void DrawObjects(Graphics g); //метод отрисовки объектов

    }
}

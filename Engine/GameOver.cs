using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billiards.Engine
{
    class GameOver
    {
        static public bool isGameOver = false; //отмечаем окончена ли игра или нет
        static public void DrawGameOverScreen(Graphics g) //вывод экрана окончания
        {
            g.DrawString($"GAME OVER \n Time: { Convert.ToInt32(Time.GetMinutes())}:{Time.GetSeconds()}", new Font("Stencil", 25), //выводим надпись об окончании
                new SolidBrush(Color.Red), (float)(Render.Resolution.X / 2 - 100), (float)(Render.Resolution.Y / 2 - 100));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billiards.Game
{
    class Hole //лунки
    {
        Vector position;
        int range;
        public Hole(Vector position, int range) //инициализация
        {
            this.position = position;
            this.range = range;
        }
        public bool CheckHole(Vector other) => range > Vector.Distance(other, position); //проверка на шар в лунке
    }
}

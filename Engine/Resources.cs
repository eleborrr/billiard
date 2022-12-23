using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using Billiards.Engine;

namespace Billiards
{
    class Resources
    {
        static Dictionary<string, Image> frames = new Dictionary<string, Image>();    //словарь изображений    
        static public void InitializationResources() //метод для загрузки ресурсов игры
        {
            frames = FileSystem.LoadFrames("Res.int"); //загрузка изображений из файла в словарь ресурсов при помощи класса FileSystem
        }
        static public Image GetFrame(string key) => frames[key]; //получаем изображение по названию
    }
}

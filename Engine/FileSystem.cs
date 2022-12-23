using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Media;

namespace Billiards.Engine
{
    class FileSystem
    {
        public static Dictionary<string,Image> LoadFrames(string path) //загружаем изображения
        {
            Dictionary<string, Image> res = new Dictionary<string, Image>(); //помещаем их в словарь
            StreamReader sr = new StreamReader(path); //создаём поток данных по задонному пути
            while(!sr.EndOfStream) //читаем покуда файл не закончиться
            {
                string[] lines = sr.ReadLine().Split('|'); //делим строку по символу |
                res.Add(lines[0], Image.FromFile(lines[1])); //в файле пишется вначале название изображения затем ссылка на него
            }
            sr.Close(); //закрываем поток
            return res; //возвращаем словарь с названиями и спрайтами
        }
    }
}

using System.Drawing;

namespace Server.Game;


public class FileSystem
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

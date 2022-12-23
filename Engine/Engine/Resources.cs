using System.Drawing;

namespace Server.Game;

class Resources
{
    static Dictionary<string, Image> frames = new Dictionary<string, Image>();    //словарь изображений    
    static public void InitializationResources() //метод для загрузки ресурсов игры
    {
        frames = FileSystem.LoadFrames("Res.int"); //загрузка изображений из файла в словарь ресурсов при помощи класса FileSystem
    }
    static public Image GetFrame(string key) => frames[key]; //получаем изображение по названию
}
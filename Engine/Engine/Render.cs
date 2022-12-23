using System.Drawing;

namespace Server.Game;

public class Render
{        
    static Vector resolution; //разрешение экрана
    static IScene scene; //получаем сцену
    public static void SetResolution(int x, int y) => resolution = new Vector(x, y); //установка разрешения
    public static void SetScene(IScene customScene) => scene = customScene; //установка сцены
    public static Image DrawFrame() //рисуем кадр
    {
        Bitmap bitmap = new Bitmap((int)resolution.X, (int)resolution.Y); //создаём изображение кадра
        Graphics g = Graphics.FromImage(bitmap); //получаем графику
        scene.DrawBack(g, (int)resolution.X, (int)resolution.Y); //рисуем задник
        scene.DrawObjects(g); //рисуем объекты сцены
        if (GameOver.isGameOver) //если игра окончена выводим жкран окончания
            GameOver.DrawGameOverScreen(g); //отрисовываем надпись что игра окончена
        return bitmap; //возвращаем нарисованный кадр
    }
    public static Vector Resolution => resolution; //возвращаем разрешение экрана
}
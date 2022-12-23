namespace Server.Game;

public class Time //отслеживаем время
{        
    static int frames = 0; //кол-во кадров прошло с момента начала игры
    static int interval;
    static public void SetInterval(int value) => interval = value; //интервал между кадрами
    static public void Frame_Tick(object sender, EventArgs e) => frames++; //считаем кадры от начала игры
    static public int GetMiliSeconds() => frames * interval; //кол-во милисекунд в игре
    static public double GetSeconds() => GetMiliSeconds()/1000; //секунд в игре
    static public double GetMinutes() => GetSeconds() / 60; //минут в игре
    static public int deltaTime => interval; //получаем время между кадрами
}
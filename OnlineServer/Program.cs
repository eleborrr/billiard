using System.Text.Json;
using System.Text.Json.Nodes;
using Billiards;
using Server.Game;

namespace PaintOnlineServer
{
    internal class Program
    {
        private static System.Windows.Forms.Timer Frame = new System.Windows.Forms.Timer();
        private static System.Windows.Forms.PictureBox renderBox;
        private static Scene scene;
        private static ServerObject server;
        
        static async Task Main(string[] args)
        {
            // ApplicationConfiguration.Initialize();
            // Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new MainWindow());
            server = new ServerObject();// создаем сервер
            await server.ListenAsync(); // запускаем сервер
        }
    }
}
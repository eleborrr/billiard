using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using Server.Game;

namespace PaintOnlineServer
{
    public class ServerObject
    {
        TcpListener tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8888);
        Dictionary<string, ClientObject> clients = new();
        private static Scene scene;

        protected internal void RemoveConnection(string id)
        {
            ClientObject? client = clients[id];
            if (client != null) clients.Remove(id);
            client?.Close();
        }


        protected internal async Task ListenAsync()
        {
            try
            {
                tcpListener.Start();
                Console.WriteLine("Сервер запущен. Ожидание подключений...");
                // await StartGame(this);

                while (true)
                {
                    TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();

                    ClientObject clientObject = new ClientObject(tcpClient, this);
                    clients.Add(clientObject.Id, clientObject);
                    Task.Run(clientObject.ProcessAsync);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("asdsdsddd");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Disconnect();
            }
        }
        
        public async static Task StartGame(ServerObject server) //инициализация компонентов
        {
            Render.SetResolution(1024, 768); //задаём разрешения окна
            Render.SetScene(scene = new Scene()); //создаём сцену и помещаем её в класс который создаёт кадры
            
            // Time.SetInterval(Frame.Interval); //задаём интервалы между кадрами для класса который отслеживает время
            // Frame.Tick += new System.EventHandler(Time.Frame_Tick); //класс времени реагирует на события таймера
            await Frame_Tick(server);

        }

        private async static Task Frame_Tick(ServerObject server) //обновление кадров
        {
            // Console.WriteLine("yeasss!");
            await server.BroadcastMessageAsync(JsonSerializer.Serialize(Render.DrawFrame()), "1");
            // renderBox.BackgroundImage = Render.DrawFrame(); //передаём созданный кадр в окно
            // Frame.Enabled = !GameOver.isGameOver;//останавливаем таймер если игра окончена
        }

        // трансляция сообщения подключенным клиентам
        protected internal async Task BroadcastMessageAsync(string message, string id)
        {
            foreach (var (clientId, client) in clients)
            {   
                await client.Writer.WriteLineAsync(message); //передача данных
                await client.Writer.FlushAsync();
            }
        }

        // отключение всех клиентов
        protected internal void Disconnect()
        {
            foreach (var (id, client) in clients)
            {
                client.Close(); //отключение клиента
            }
            tcpListener.Stop(); //остановка сервера
        }
    }
}

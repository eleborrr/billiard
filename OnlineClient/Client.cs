using System.Net.Sockets;
using System.Text.Json;
using PaintLibrary;

namespace PaintOnlineClient;

public class Client
{
    private string? userName;
    
    public StreamReader? Reader = null;
    public StreamWriter? Writer = null;
    public async Task Work()
    {
        
        string host = "127.0.0.1";
        int port = 8888;
        using TcpClient client = new TcpClient();
        try
        {
            client.Connect(host, port); //подключение клиента
            Reader = new StreamReader(client.GetStream());
            Writer = new StreamWriter(client.GetStream());
            if (Writer is null || Reader is null) return;
            // запускаем новый поток для получения данных
            Task.Run(() => ReceiveMessageAsync(Reader));
            // запускаем ввод сообщений
            await SendMessageAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    async Task GetFrame()
    {
        await ReceiveMessageAsync(Reader);
    }


    public async Task SendMessageAsync()
    {
        // сначала отправляем имя
        await Writer.WriteLineAsync("asddd");
        await Writer.FlushAsync();
        Console.WriteLine("Для отправки сообщений введите сообщение и нажмите Enter");
    
        // while (true)
        // {
            string? message = "jj";
            SendPoint point = new SendPoint
            {
                UserName = userName,
                Message = message
            };
            await Writer.WriteLineAsync(JsonSerializer.Serialize(point));
            await Writer.FlushAsync();
        // }
    }
    
    // получение сообщений
    public async Task ReceiveMessageAsync(StreamReader reader)
    {
        while (true)
        {
            try
            {
                // считываем ответ в виде строки
                string? message = await reader.ReadLineAsync();
                // если пустой ответ, ничего не выводим на консоль
                if (string.IsNullOrEmpty(message)) continue;
                Print(message);//вывод сообщения
            }
            catch
            {
                break;
            }
        }
    }
    // чтобы полученное сообщение не накладывалось на ввод нового сообщения
    void Print(string message)
    {
        if (OperatingSystem.IsWindows()) // если ОС Windows
        {
            var position = Console.GetCursorPosition(); // получаем текущую позицию курсора
            int left = position.Left; // смещение в символах относительно левого края
            int top = position.Top; // смещение в строках относительно верха
            // копируем ранее введенные символы в строке на следующую строку
            Console.MoveBufferArea(0, top, left, 1, 0, top + 1);
            // устанавливаем курсор в начало текущей строки
            Console.SetCursorPosition(0, top);
            // в текущей строке выводит полученное сообщение
            Console.WriteLine(message);
            // переносим курсор на следующую строку
            // и пользователь продолжает ввод уже на следующей строке
            Console.SetCursorPosition(left, top + 1);
        }
        else Console.WriteLine(message);
    }
}
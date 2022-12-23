using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaintOnlineClient;
using Server.Game;
// using Server.Engine;

namespace Billiards
{
    public partial class MainWindow : Form
    {
        Scene scene;
        private Client client;
        public MainWindow() //инициализация компонентов
        {
            InitializeComponent();
            client = new Client();
            client.Work();

            Render.SetResolution(renderBox.Width, renderBox.Height); //задаём разрешения окна
            Render.SetScene(scene = new Scene()); //создаём сцену и помещаем её в класс который создаёт кадры
            
            Time.SetInterval(Frame.Interval); //задаём интервалы между кадрами для класса который отслеживает время
            Frame.Tick += new System.EventHandler(Time.Frame_Tick); //класс времени реагирует на события таймера
        }

        private void Frame_Tick(object sender, EventArgs e) //обновление кадров
        {
            // client.ReceiveMessageAsync(client.Reader);
            renderBox.BackgroundImage = Render.DrawFrame(); //передаём созданный кадр в окно
            // renderBox.BackgroundImage = client; //передаём созданный кадр в окно

            Frame.Enabled = !GameOver.isGameOver;//останавливаем таймер если игра окончена
        }

        private void renderBox_MouseDown(object sender, MouseEventArgs e) => scene.ActionClick(e.X, e.Y); //проверка на нажатия мыши
    }
}

using System.Collections.Generic;
using System.Drawing;
using System;

namespace Server.Game;

public class Scene : IScene //сцена
    {
        Image backGround; //фон
        List<Ball> bolls = new List<Ball>(); //лузы
        List<Hole> holes = new List<Hole>(); //шары
        Field field; //ограничевающее поле
        public Scene() //инициализация сцены
        {            
            Resources.InitializationResources(); //загружаем файлы
            backGround = Resources.GetFrame("Back"); // берём фон из ресурсов
            int bollSize = 20; //размер шаров
            bolls.Add(new Ball(150,(int)Render.Resolution.Y / 2 - bollSize / 2, bollSize, bollSize, Brushes.White, true)); //создаём главный шар    
            int addIndex = 1;
            Random r = new Random();            
            for (int i = 0; i<5;i++) //сложная математическая штука которая выстраивает пирамиду, я сам не понял как её написал
            {
                for (int j = 0; j < addIndex; j++)
                    bolls.Add(new Ball(300 + (addIndex * bollSize), (addIndex % 2 == 0 ? (int)Render.Resolution.Y / 2 : (int)Render.Resolution.Y / 2 - bollSize / 2) + (j - addIndex / 2) * bollSize
                        , bollSize, bollSize, new SolidBrush(Color.FromArgb(255, (byte)r.Next(256), (byte)r.Next(256), (byte)r.Next(256))))); //берём рандомный цвет для шаров
                addIndex++;
            }
            int holeRange = 20; //размер лузы
            holes.Add(new Hole(new Vector(40, 40), holeRange)); //размещаем лузы по местам
            holes.Add(new Hole(new Vector(40, 325), holeRange));

            holes.Add(new Hole(new Vector(300, 325), holeRange));
            holes.Add(new Hole(new Vector(300, 40), holeRange));

            holes.Add(new Hole(new Vector(560, 325), holeRange));
            holes.Add(new Hole(new Vector(560, 40), holeRange));


            field = new Field(new Vector(40,40), new Vector(560, 325)); //задаём размеры поля
        }
        public void DrawBack(Graphics g, int x, int y) => g.DrawImage(backGround, 0, 0, x, y); //рисуем задник
        public void DrawObjects(Graphics g) //рисуем объекты
        {
            CheckBallsCollisons();//проверяем шары на столкновения
            foreach (Ball i in bolls) //перебираем шары
            {
                i.Draw(g); //рисуем шар
                //if (field.OutCheck(i.GetCenter()))
                //    i.SetSpeed(i.GetSpeed().Invert()); //проверяем чтобы шар не вылетал за поле, иначе инвертируем его скорость
                HandleWallCollision(i);
                foreach (Hole hole in holes) //перебираем лузы
                {
                    if(hole.CheckHole(i.Position)) //если шар в лунке то удаляем и перепроверяем шары
                    {
                        if (i.isMain) //если попадает в лузу главный шар - останавливае игру
                        {
                            GameOver.isGameOver = true;
                        }                            
                        else //иначе просто убераем шар и начинаем рисовать объекты заного
                        {
                            bolls.Remove(i);
                            DrawObjects(g);
                            return;
                        }
                    }
                }
            }
        }       
        public void ActionClick(int x, int y) //если нажата мышь то задаём скорость главному шару
        {
            Vector position = bolls[0].GetCenter();
            bolls[0].SetSpeed(DefineSpeed(position.X - x),DefineSpeed(position.Y - y)); //задаём скорость относительно отрезка между шаром и курсором (противоположно курсору)
        }

        private double DefineSpeed(double speed) // ставим границы на скорость перемещения шара
        {
            speed /= 5;
            var maxSpeed = 20;
            var minSpeed = -20;
            if (speed < minSpeed) return minSpeed;
            if (speed > maxSpeed) return maxSpeed;
            return speed;
        }

        public void CheckBallsCollisons() //проверяем шары на столкновения
        {
            for (int i = 0; i<bolls.Count; i++) //перебираем шары 
            {
                for(int j = i+1; j < bolls.Count; j++)
                {
                    if (bolls[i].Collision(bolls[j].GetCenter())) //проверяем их на столкновения друг с другом
                        HandleBallCollision(bolls[i], bolls[j]);
                    HandleWallCollision(bolls[i]);
                }
            }
        }

        public void HandleBallCollision(Ball ball1, Ball ball2) 
        {
            var direction = ball2.Position - ball1.Position;
            var distance = Vector.Distance(ball1.Position, ball2.Position);
            if (distance == 0 || distance > ball1.Radius() + ball2.Radius())
                return;

            direction *= (1/distance); // нормализация вектора столкновения

            var v1 = Vector.Dot(ball1.GetSpeed(), direction);
            var v2 = Vector.Dot(ball2.GetSpeed(), direction);

            var newV1 = (v1 + v2 - (v1 - v2)) / 2;
            var newV2 = (v1 + v2 - (v2 - v1)) / 2;

            ball1.AddToSpeed(direction * (newV1 - v1));
            ball2.AddToSpeed(direction * (newV2 - v2));

            if (ball1.GetSpeed() == 0 && ball2.GetSpeed() == 0)
            {
                ball1.AddToSpeed(new Vector(ball1.Position.X - ball2.Position.X, ball1.Position.Y - ball2.Position.Y) / 5);
                ball2.AddToSpeed(new Vector(ball2.Position.X - ball1.Position.X, ball2.Position.Y - ball1.Position.Y) / 5);
            }
            //if (bolls[i].GetSpeed() != 0) //передаём скорость между шарами если она не равна 0
            //{
            //    bolls[j].SetSpeed(bolls[i].GetSpeed());
            //    bolls[i].SetSpeed(bolls[i].GetSpeed() / 1.2); //причём скорость на 20% ниже
            //}
            //else
            //{
            //    Vector posI = bolls[i].Position;
            //    Vector posJ = bolls[j].Position;
            //    bolls[j].SetSpeed(new Vector(-posI.X + posJ.X, -posI.Y + posJ.Y)); //расталкиваем их если шары пересекают друг друга но не имеют скорости
            //}
        }

        public void HandleWallCollision(Ball ball)
        {
            var maxY = Render.Resolution.Y;
            var maxX = Render.Resolution.X;
            if (ball.Position.X < ball.Size.X)
            {
                ball.SetPosition(ball.Size.X, ball.Position.Y);
                ball.SetSpeed(-ball.GetSpeed().X, ball.GetSpeed().Y);
            }

            if (ball.Position.X > maxX - ball.Size.X)
            {
                ball.SetPosition(maxX - ball.Size.X, ball.Position.Y);
                ball.SetSpeed(-ball.GetSpeed().X, ball.GetSpeed().Y);
            }

            if (ball.Position.Y < ball.Size.Y)
            {
                ball.SetPosition(ball.Position.X, ball.Size.Y);
                ball.SetSpeed(ball.GetSpeed().X, -ball.GetSpeed().Y);
            }

            if (ball.Position.Y > maxY - ball.Size.Y)
            {
                ball.SetPosition(ball.Position.X, maxY - ball.Size.Y);
                ball.SetSpeed(-ball.GetSpeed().X, -ball.GetSpeed().Y);
            }
        }
    }
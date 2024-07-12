using System;
using System.Drawing;
using System.Windows.Forms;
using RobotMVC.Models;

namespace RobotMVC.Controllers
{
    public class GameController
    {
        private GameModel model;
        private GameView view;
        private int tact = 0; // вспомогательная переменная отслеживающая номер кадра

        public GameController(GameModel model, GameView view)
        {
            this.model = model;
            this.view = view;
            this.view.SetModel(model);
            this.view.Paint += OnPaint;
            this.view.MouseClick += OnMouseClick;
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (tact >= model.FrameByFramePeriod)
            {
                if (model.StageOfGame == 1)
                {
                    AIRobot();
                    if (model.Map[model.RobotPos.X, model.RobotPos.Y] == 3)
                    {
                        EndGame(1);
                    }
                }
                tact = 0;
            }
            tact++;
            view.UpdateView();
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && model.StageOfGame == 0)
            {
                if (GameModel.TopLeftPoint.X <= e.X && e.X <= (GameModel.TopLeftPoint.X + GameModel.SizeCell * (model.Map.GetLength(0))) &&
                    GameModel.TopLeftPoint.Y <= e.Y && e.Y <= (GameModel.TopLeftPoint.Y + GameModel.SizeCell * (model.Map.GetLength(1))))
                {
                    int x = (int)((e.X - GameModel.TopLeftPoint.X) / GameModel.SizeCell);
                    int y = (int)((e.Y - GameModel.TopLeftPoint.Y) / GameModel.SizeCell);

                    if (model.DrawnElement == 1)
                    {
                        if (model.Map[x, y] == 0) model.Map[x, y] = 1;
                        else if (model.Map[x, y] == 1) model.Map[x, y] = 0;
                        else if (model.Map[x, y] == 2)
                        {
                            model.Map[x, y] = 1;
                            model.ThereIsStart = false;
                        }
                        else if (model.Map[x, y] == 3)
                        {
                            model.Map[x, y] = 1;
                            model.ThereIsEnd = false;
                        }
                    }
                    else if (model.DrawnElement == 2)
                    {
                        if (model.ThereIsStart && model.Map[x, y] != 2)
                        {
                            MessageBox.Show("На поле уже расположен старт", "Ошибка", MessageBoxButtons.OK);
                        }
                        else
                        {
                            if (model.Map[x, y] == 0 || model.Map[x, y] == 1)
                            {
                                model.RobotPos = new Point(x, y);
                                model.Map[x, y] = 2;
                                model.ThereIsStart = true;
                            }
                            else if (model.Map[x, y] == 2)
                            {
                                model.Map[x, y] = 0;
                                model.ThereIsStart = false;
                            }
                            else if (model.Map[x, y] == 3)
                            {
                                model.RobotPos = new Point(x, y);
                                model.Map[x, y] = 2;
                                model.ThereIsStart = true;
                                model.ThereIsEnd = false;
                            }
                        }
                    }
                    else if (model.DrawnElement == 3)
                    {
                        if (model.ThereIsEnd && model.Map[x, y] != 3)
                        {
                            MessageBox.Show("На поле уже расположен финиш", "Ошибка", MessageBoxButtons.OK);
                        }
                        else
                        {
                            if (model.Map[x, y] == 0 || model.Map[x, y] == 1)
                            {
                                model.Map[x, y] = 3;
                                model.ThereIsEnd = true;
                            }
                            else if (model.Map[x, y] == 2)
                            {
                                model.Map[x, y] = 3;
                                model.ThereIsStart = false;
                                model.ThereIsEnd = true;
                            }
                            else if (model.Map[x, y] == 3)
                            {
                                model.Map[x, y] = 0;
                                model.ThereIsEnd = false;
                            }
                        }
                    }
                }
            }
        }

        public void StartGame()
        {
            if (model.ThereIsStart && model.ThereIsEnd)
            {
                model.StageOfGame = 1;
            }
            else
            {
                MessageBox.Show("Перед началом расположите начало и конец", "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void EndGame(int result)
        {
            model.StageOfGame = 0;
            for (int i = 0; i < model.Map.GetLength(0); i++)
                for (int j = 0; j < model.Map.GetLength(1); j++)
                    if (model.Map[i, j] == 2)
                    {
                        model.RobotPos = new Point(i, j);
                    }
            model.MapWay = new int[GameModel.SizeMap[0], GameModel.SizeMap[1]];
            MessageBox.Show(result == 1 ? "Робот дошёл до конца" : "Робот не смог найти путь", result == 1 ? "Победа" : "Поражение", MessageBoxButtons.OK);
        }

        private void AIRobot()
        {
            if (!model.PobotWasColusion)
            {
                if (model.RobotPos.X == GameModel.SizeMap[0] - 1 || model.RobotPos.X == 0) { model.PobotWasColusion = true; model.Direction = 2; }
                else if (model.Map[model.RobotPos.X - 1, model.RobotPos.Y] == 1 || model.Map[model.RobotPos.X + 1, model.RobotPos.Y] == 1)
                {
                    model.PobotWasColusion = true;
                    if (model.Map[model.RobotPos.X - 1, model.RobotPos.Y] == 1) model.Direction = 2;
                }
                if (model.RobotPos.Y == GameModel.SizeMap[1] - 1 || model.RobotPos.Y == 0) model.PobotWasColusion = true;
                else if (model.Map[model.RobotPos.X, model.RobotPos.Y - 1] == 1 || model.Map[model.RobotPos.X, model.RobotPos.Y + 1] == 1) model.PobotWasColusion = true;
                if (model.RobotPos.Y == GameModel.SizeMap[1] - 1) model.Direction = 1;
                if (!model.PobotWasColusion) model.RobotPos = new Point(model.RobotPos.X, model.RobotPos.Y - 1);
            }
            else
            {
                bool moveWas = false;
                while (moveWas == false)
                {
                    model.MapWay[model.RobotPos.X, model.RobotPos.Y]++;
                    if (model.MapWay[model.RobotPos.X, model.RobotPos.Y] > 40)
                    {
                        EndGame(0);
                        moveWas = true;
                    }
                    else
                    {
                        if (model.Direction == 0)
                        {
                            if (model.RobotPos.Y == 0) DownDirection();
                            else if (model.Map[model.RobotPos.X, model.RobotPos.Y - 1] == 1) DownDirection();
                            else
                            {
                                model.RobotPos = new Point(model.RobotPos.X, model.RobotPos.Y - 1);
                                if (model.RobotPos.X != GameModel.SizeMap[0] - 1 && model.Map[model.RobotPos.X + 1, model.RobotPos.Y] != 1) UpDirection();
                                moveWas = true;
                            }
                        }
                        else if (model.Direction == 1)
                        {
                            if (model.RobotPos.X == GameModel.SizeMap[0] - 1) DownDirection();
                            else if (model.Map[model.RobotPos.X + 1, model.RobotPos.Y] == 1) DownDirection();
                            else
                            {
                                model.RobotPos = new Point(model.RobotPos.X + 1, model.RobotPos.Y);
                                if (model.RobotPos.Y != GameModel.SizeMap[1] - 1 && model.Map[model.RobotPos.X, model.RobotPos.Y + 1] != 1) UpDirection();
                                moveWas = true;
                            }
                        }
                        else if (model.Direction == 2)
                        {
                            if (model.RobotPos.Y == GameModel.SizeMap[1] - 1) DownDirection();
                            else if (model.Map[model.RobotPos.X, model.RobotPos.Y + 1] == 1) DownDirection();
                            else
                            {
                                model.RobotPos = new Point(model.RobotPos.X, model.RobotPos.Y + 1);
                                if (model.RobotPos.X != 0 && model.Map[model.RobotPos.X - 1, model.RobotPos.Y] != 1) UpDirection();
                                moveWas = true;
                            }
                        }
                        else if (model.Direction == 3)
                        {
                            if (model.RobotPos.X == 0) DownDirection();
                            else if (model.Map[model.RobotPos.X - 1, model.RobotPos.Y] == 1) DownDirection();
                            else
                            {
                                model.RobotPos = new Point(model.RobotPos.X - 1, model.RobotPos.Y);
                                if (model.RobotPos.Y != 0 && model.Map[model.RobotPos.X, model.RobotPos.Y - 1] != 1) UpDirection();
                                moveWas = true;
                            }
                        }
                    }
                }
            }
        }

        private void UpDirection()
        {
            model.Direction--;
            if (model.Direction < 0) model.Direction = 3;
        }

        private void DownDirection()
        {
            model.Direction++;
            if (model.Direction > 3) model.Direction = 0;
        }
    }
}


using System;
using System.Drawing;
using System.Windows.Forms;
using RobotMVC.Controllers;
using RobotMVC.Models;

namespace RobotMVC
{
    public partial class GameView : Form
    {
        private GameModel model;
        private GameController controller;

        public GameView()
        {
            InitializeComponent();
        }

        public void SetModel(GameModel model)
        {
            this.model = model;
        }

        public void SetController(GameController controller)
        {
            this.controller = controller;
        }

        public void UpdateView()
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (model != null)
            {
                DrawObjects(e.Graphics);
                DrawMap(e.Graphics);
                e.Graphics.DrawImage(new Bitmap(System.IO.Directory.GetCurrentDirectory() + "\\robot.png"),
                    new Rectangle((int)this.Width / 2, 0, (int)this.Width / 2, this.Height - 40));
            }
        }

        private void DrawObjects(Graphics g)
        {
            for (int i = 0; i < model.Map.GetLength(0); i++)
                for (int j = 0; j < model.Map.GetLength(1); j++)
                {
                    Brush brush = Brushes.White;
                    if (model.Map[i, j] == 1)
                        brush = Brushes.Black;
                    else if (model.Map[i, j] == 2)
                        brush = Brushes.Green;
                    else if (model.Map[i, j] == 3)
                        brush = Brushes.Blue;

                    g.FillRectangle(brush, new Rectangle(GameModel.TopLeftPoint.X + i * GameModel.SizeCell,
                                                         GameModel.TopLeftPoint.Y + j * GameModel.SizeCell,
                                                         GameModel.SizeCell,
                                                         GameModel.SizeCell));
                }

            if (model.StageOfGame == 1)
            {
                g.FillRectangle(Brushes.Red, new Rectangle(GameModel.TopLeftPoint.X + model.RobotPos.X * GameModel.SizeCell,
                                                           GameModel.TopLeftPoint.Y + model.RobotPos.Y * GameModel.SizeCell,
                                                           GameModel.SizeCell,
                                                           GameModel.SizeCell));
            }
        }

        private void DrawMap(Graphics g)
        {
            for (int i = 0; i < model.Map.GetLength(1) + 1; i++)
            {
                g.DrawLine(Pens.Black,
                           new Point(GameModel.TopLeftPoint.X, GameModel.TopLeftPoint.Y + GameModel.SizeCell * i),
                           new Point(GameModel.TopLeftPoint.X + GameModel.SizeCell * model.Map.GetLength(0),
                                     GameModel.TopLeftPoint.Y + GameModel.SizeCell * i));
            }
            for (int i = 0; i < model.Map.GetLength(0) + 1; i++)
            {
                g.DrawLine(Pens.Black,
                           new Point(GameModel.TopLeftPoint.X + GameModel.SizeCell * i, GameModel.TopLeftPoint.Y),
                           new Point(GameModel.TopLeftPoint.X + GameModel.SizeCell * i,
                                     GameModel.TopLeftPoint.Y + GameModel.SizeCell * model.Map.GetLength(1)));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Wall_Click(object sender, EventArgs e)
        {
            model.DrawnElement = 1;
        }

        private void Start_Click(object sender, EventArgs e)
        {
            model.DrawnElement = 2;
        }

        private void End_Click(object sender, EventArgs e)
        {
            model.DrawnElement = 3;
        }

        private void Begin_Click(object sender, EventArgs e)
        {
            controller.StartGame(); // Вызываем StartGame() через контроллер
        }
    }
}

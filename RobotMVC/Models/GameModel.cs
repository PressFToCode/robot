using System.Drawing;

namespace RobotMVC.Models
{
    public class GameModel
    {
        public int[,] Map { get; set; }
        public Point RobotPos { get; set; }
        public int DrawnElement { get; set; }
        public bool ThereIsStart { get; set; }
        public bool ThereIsEnd { get; set; }
        public int StageOfGame { get; set; }
        public int Direction { get; set; }
        public bool PobotWasColusion { get; set; }
        public int FrameByFramePeriod { get; set; } = 25; // Период кадров
        public int[,] MapWay { get; set; }
        public static Point TopLeftPoint { get; } = new Point(10, 50);
        public static int SizeCell { get; } = 30;
        public static int[] SizeMap { get; } = { 9, 8 };

        public GameModel()
        {
            Map = new int[SizeMap[0], SizeMap[1]];
            MapWay = new int[SizeMap[0], SizeMap[1]];
            RobotPos = new Point(0, 0);
            DrawnElement = 1;
            ThereIsStart = false;
            ThereIsEnd = false;
            StageOfGame = 0;
            Direction = 0;
            PobotWasColusion = false;
        }
    }
}

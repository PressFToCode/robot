using RobotMVC.Controllers;
using RobotMVC.Models;
using RobotMVC;

namespace RobotMVC
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GameModel model = new GameModel();
            GameView view = new GameView();
            GameController controller = new GameController(model, view);

            view.SetController(controller); // Добавляем этот вызов

            Application.Run(view);
        }
    }
}
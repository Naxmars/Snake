using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Snake
{
    class Program
    {
        private const int WindowWidth = 25;
        private const int WindowHeight = 80;
        private const string WindowTitle = "Sharp Snake";

        private static Game game;

        static void Control()
        {
            while (game.IsRunning)
            {
                ConsoleKeyInfo a = Console.ReadKey(true);
                ConsoleKey key = a.Key;
                if (key == ConsoleKey.A) { if (game.Direction != 2) game.Direction = 1; }
                else if (key == ConsoleKey.D) { if (game.Direction != 1) game.Direction = 2; }
                else if (key == ConsoleKey.W) { if (game.Direction != 4) game.Direction = 3; }
                else if (key == ConsoleKey.S) { if (game.Direction != 3) game.Direction = 4; }
                else if (key == ConsoleKey.F1) { Console.BackgroundColor = ToggleColor(Console.BackgroundColor); }
                else if (key == ConsoleKey.F2) { Console.ForegroundColor = ToggleColor(Console.ForegroundColor); }
                Thread.Sleep(100);
            }
        }

        static ConsoleColor ToggleColor(ConsoleColor curCol)
        {
            if (curCol != (ConsoleColor)15)
                return (ConsoleColor)((int)(curCol) + 1);
            else
                return (ConsoleColor)0;
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(WindowHeight, WindowWidth);
            Console.SetBufferSize(WindowHeight, WindowWidth);
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.CursorVisible = false;
            game = new Game(Console.BufferHeight - 1, Console.BufferWidth);
            Thread inputThread = new Thread(new ThreadStart(Control));
            game.Start();
            inputThread.Start();

            while (game.IsRunning)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write(game.MapToString());
                game.UpdateSnake();
                Console.Title = WindowTitle + " :: " + "[Skóre: " + game.Score + "]";
                Thread.Sleep(100);
            }
        }
    }
}

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
        public static GameField hra;
        static void Control()
        {
            while (hra.isRunning)
            {
                ConsoleKeyInfo a = Console.ReadKey();
                ConsoleKey key = a.Key;
                if (key.ToString().Contains("A")) { if (hra.direction != 2) hra.direction = 1; }
                else if (key.ToString().Contains("D")) { if (hra.direction != 1) hra.direction = 2; }
                else if (key.ToString().Contains("W")) { if (hra.direction != 4) hra.direction = 3; }
                else if (key.ToString().Contains("S")) { if (hra.direction != 3) hra.direction = 4; }
                Thread.Sleep(100);
            }
        }

        static void Main(string[] args)
        {
            hra = new GameField(20, 20);
            Thread thread = new Thread(new ThreadStart(Control));
            hra.Start();
            thread.Start();

            while (hra.isRunning)
            {
                Console.SetCursorPosition(0,0);
                Console.Write(hra.mapToString());
                hra.updateSnake();
                Thread.Sleep(100);
            }
        }
    }
}

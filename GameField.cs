using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class GameField
    {
        private Random rnd;
        private Point foodPos;
        private bool running;
        private int snakedir;
        private List<Point> snake;
        private int[][] map;

        public GameField(int sizeX, int sizeY)
        {
            running = false;
            snakedir = 0;
            rnd = new Random();
            map = new int[sizeX][];
            for(int i = 0; i < sizeX; i++)
            {
                map[i] = new int[sizeY];
            }
            snake = new List<Point>();
            foodPos = new Point(rnd.Next(1, sizeX), rnd.Next(1, sizeY));
        }

        public bool isRunning
        {
            get { return this.running; }
        }

        public void generateMap()
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int y = 0; y < map[0].Length; y++)
                {
                    if (i == 0 | y == 0 | i == map.Length - 1 | y == map[0].Length-1) { map[i][y] = 1; }
                    else { map[i][y] = 0; }
                }
            }
        }

        public void Stop()
        {
            running = false;
            Console.SetCursorPosition(0,0);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Prohrál jsi!");
        }

        public void Start()
        {
            running = true;
            generateMap();
            snake.Add(new Point(map.Length / 2, map[0].Length / 2));
        }
        private void generateFood()
        {
            map[foodPos.X][foodPos.Y] = 0;
            foodPos.X = rnd.Next(2, map.Length - 1);
            foodPos.Y = rnd.Next(2, map[0].Length - 1);
            map[foodPos.X][foodPos.Y] = 3;
        }
        public void updateSnake()
        {
            if (foodPos.X == snake[0].X & foodPos.Y == snake[0].Y)
            {
                snake.Add(new Point(snake[snake.Count - 1].X, snake[snake.Count - 1].Y));
                generateFood();
            }

            map[foodPos.X][foodPos.Y] = 3;

            map[snake[snake.Count - 1].X][snake[snake.Count - 1].Y] = 0;
            Console.Write(" ");
            for (int i = snake.Count-2; i > -1 ; i--)
            {
                snake[i + 1].Set(snake[i]);
                
            }
            if (snakedir == 1) { snake[0].Y -= 1; }
            else if (snakedir == 2) { snake[0].Y += 1; }
            else if (snakedir == 3) { snake[0].X -= 1; }
            else if (snakedir == 4) { snake[0].X += 1; }


            //nastaví snejka na mapu (číslo 2 aby se na mapu vykreslil blok hada) Pokud by měla být přepsána zeď (1), naráží had do stěny a hráč prohrává.          
            for (int i = 0; i < snake.Count; i++)
            {
                if (map[snake[i].X][snake[i].Y] == 3 & i != 0) { this.generateFood(); }
                

                if (map[snake[i].X][snake[i].Y] != 1 & map[snake[i].X][snake[i].Y] != 2) { map[snake[i].X][snake[i].Y] = 2; }
                else { Stop(); }
            }
        }

        public int direction
        {
            get { return this.snakedir; }
            set { this.snakedir = value; }
        }

        public string mapToString()
        {
            string help = "";

            for (int i = 0; i < map.Length; i++)
            {
                for (int y = 0; y < map.Length; y++)
                {
                    if (map[i][y] == 0) { help += " "; }
                    else if (map[i][y] == 1) { help += "█"; }
                    else if (map[i][y] == 2) { help += "█"; map[i][y] = 0; }
                    else if (map[i][y] == 3) { help += "F"; }
                }
                help += "\r\n";
            }

            return help;
        }
    }
}

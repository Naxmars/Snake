using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Game
    {
        private Random rnd;
        private Point foodPos;
        private bool running;
        private int snakedir;
        private List<Point> snake;
        private MapBlock[][] map;
        private Sprite food;

        public bool IsRunning
        {
            get { return this.running; }
        }

        public int Direction
        {
            get { return this.snakedir; }
            set { this.snakedir = value; }
        }

        public int Score
        {
            get { return snake.Count - 1; }
        }

        public Game(int sizeX, int sizeY)
        {
            running = false;
            snakedir = 0;
            rnd = new Random();
            map = new MapBlock[sizeX][];
            food = new Sprite('░', '▒', '▓', '█');
            for(int i = 0; i < sizeX; i++)
            {
                map[i] = new MapBlock[sizeY];
            }
            snake = new List<Point>();
            GenerateFood();
        }

        public void GenerateMap()
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int y = 0; y < map[0].Length; y++)
                {
                    if (i == 0 | y == 0 | i == map.Length - 1 | y == map[0].Length - 1) { map[i][y] = MapBlock.Wall; }
                    else { map[i][y] = 0; }
                }
            }
        }

        public void Stop()
        {
            running = false;
            string msg = "Prohrál jsi! Skóre: " + Score;
            Console.SetCursorPosition((Console.BufferWidth / 2) - (msg.Length / 2), Console.BufferHeight / 2);
            Console.Write(msg);
        }

        public void Start()
        {
            running = true;
            GenerateMap();
            snake.Add(new Point(map.Length / 2, map[0].Length / 2));
        }

        private void GenerateFood()
        {
            if (foodPos == null)
                foodPos = new Point(0, 0);
            else
                map[foodPos.X][foodPos.Y] = MapBlock.Empty;
            foodPos.X = rnd.Next(2, map.Length - 2);
            foodPos.Y = rnd.Next(2, map[0].Length - 2);
            map[foodPos.X][foodPos.Y] = MapBlock.Food;
        }

        public void UpdateSnake()
        {
            if (foodPos.X == snake[0].X & foodPos.Y == snake[0].Y)
            {
                snake.Add(new Point(snake[snake.Count - 1].X, snake[snake.Count - 1].Y));
                GenerateFood();
            }

            map[foodPos.X][foodPos.Y] = MapBlock.Food;

            map[snake[snake.Count - 1].X][snake[snake.Count - 1].Y] = 0;
            for (int i = snake.Count-2; i > -1 ; i--)
            {
                snake[i + 1].Set(snake[i]);
                
            }
            if (snakedir == 1) { snake[0].Y -= 1; }
            else if (snakedir == 2) { snake[0].Y += 1; }
            else if (snakedir == 3) { snake[0].X -= 1; }
            else if (snakedir == 4) { snake[0].X += 1; }

            // Nastaví snejka na mapu. Pokud by měla být přepsána zeď, naráží had do stěny a hráč prohrává.          
            for (int i = 0; i < snake.Count; i++)
            {
                if (map[snake[i].X][snake[i].Y] == MapBlock.Food & i != 0) { this.GenerateFood(); }
                if (map[snake[i].X][snake[i].Y] != MapBlock.Wall & map[snake[i].X][snake[i].Y] != MapBlock.Snake) { map[snake[i].X][snake[i].Y] = MapBlock.Snake; }
                else { Stop(); }
            }
        }

        public string MapToString()
        {
            string help = "";

            for (int i = 0; i < map.Length; i++)
            {
                if (i > 0 && map[i].Length != Console.BufferWidth) { help += Environment.NewLine; }
                for (int y = 0; y < map[i].Length; y++)
                {
                    //if (i == map.Length-1 && y == map[i].Length-1) return help;
                    if (map[i][y] == MapBlock.Empty) { help += " "; }
                    else if (map[i][y] == MapBlock.Wall) { help += "█"; }
                    else if (map[i][y] == MapBlock.Snake) { help += "█"; map[i][y] = 0; }
                    else if (map[i][y] == MapBlock.Food) { help += food.NextSymbol; }
                }
            }

            return help;
        }
    }

    enum MapBlock
    {
        Empty = 0,
        Wall = 1,
        Snake = 2,
        Food = 3,
    }
}

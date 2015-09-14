using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Point
    {
        private int posX;
        private int posY;

        public Point(int X, int Y)
        {
            this.posX = X;
            this.posY = Y;
        }

        public void Set(Point input)
        {
            this.posX = input.X;
            this.posY = input.Y;
        }

        public int X
        {
            get { return this.posX; }
            set { this.posX = value; }
        }

        public int Y
        {
            get { return this.posY; }
            set { this.posY = value; }
        }

        public override string ToString()
        {
            return "[" + this.posX + ", " + this.posY + "]";
        }
    }
}

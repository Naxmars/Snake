using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Sprite
    {
        private int animIndex = 0;
        private bool countUp = true;
        private bool loop = true;
        private char[] anim;

        public char[] Symbols
        {
            get { return anim; }
            set { anim = value; }
        }

        public char NextSymbol
        {
            get
            {
                if (!loop && animIndex >= anim.Length) animIndex = 0;
                if (loop && countUp && animIndex >= anim.Length - 1) countUp = false;
                if (loop && !countUp && animIndex <= 0) countUp = true;
                return anim[(countUp ? animIndex++ : animIndex--)];
            }
        }

        public Sprite(bool loop, params char[] symbols)
        {
            this.anim = symbols;
        }

        public Sprite(params char[] symbols)
            : this(true, symbols) { }
    }
}

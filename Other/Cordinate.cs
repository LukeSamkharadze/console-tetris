using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTetris
{
    public struct Cordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Cordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    };
}

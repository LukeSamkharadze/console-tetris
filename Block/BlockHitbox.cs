using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTetris.Block
{
    public struct BlockHitbox
    {
        public bool[,] collision;
        public int lengthY;
        public int lengthX;

        public BlockHitbox(int lengthY, int lengthX)
        {
            this.lengthY = lengthY;
            this.lengthX = lengthX;

            collision = new bool[lengthY, lengthX];

            for (int y = 0; y < lengthY; y++)
            {
                for (int x = 0; x < lengthX; x++)
                {
                    collision[y, x] = true;
                }
            }
        }

        public BlockHitbox(int lengthY_, int lengthX_, bool[,] collision_)
        {
            lengthY = lengthY_;
            lengthX = lengthX_;

            collision = new bool[lengthY, lengthX];

            collision = collision_;
        }
    }

}

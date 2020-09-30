using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTetris.Block
{
    public struct BlockHitbox
    {
        public bool[,] hitbox;

        public int Height { get; }
        public int Width { get; }

        public BlockHitbox(int height, int width)
        {
            Height = height;
            Width = width;

            hitbox = new bool[height, width];

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    hitbox[y, x] = true;
        }

        public BlockHitbox(int height, int width, bool[,] collision_)
        {
            Height = height;
            Width = width;

            hitbox = new bool[Height, Width];

            hitbox = collision_;
        }
    }

}

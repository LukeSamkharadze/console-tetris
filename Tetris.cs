using ConsoleTetris.Block;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleTetris
{
    public class Tetris
    {
        public BlockHitbox[] collisions;

        public int height;
        public int width;

        public bool isGameOver;

        public Block.Block[] blocks = new Block.Block[0];

        public Tetris(int lengthY, int lengthX)
        {
            isGameOver = false;

            this.height = lengthY;
            this.width = lengthX;

            collisions = new BlockHitbox[]
            {
                new BlockHitbox(2,2),
                new BlockHitbox(1, 4),
                new BlockHitbox(2, 3, new bool[,]{{ true,false,false},{ true, true, true } }),
                new BlockHitbox(2, 3, new bool[,] { { false,false, true }, { true,true, true, } }),
                new BlockHitbox(2, 3, new bool[,]{{false,true,true},{ true,true,false }}),
                new BlockHitbox(2, 3, new bool[,]{{true,true,false},{ false,true,true }}),
                new BlockHitbox(2, 3, new bool[,]{{false,true,false},{ true,true,true }})
            };
        }

        public void PrintGame()
        {
            bool flag;

            for (int x = 0; x < width + 2; x++)
                System.Console.Write("■ ");

            System.Console.Write("\n");

            for (int y = 0; y < height; y++)
            {
                System.Console.Write("■ ");

                for (int x = 0; x < width; x++)
                {
                    flag = true;

                    for (int n = 0; n < blocks.Length; n++)
                        for (int collisionY = 0; blocks[n] != null && collisionY < blocks[n].hitbox.Height; collisionY++)
                            for (int collisionX = 0; collisionX < blocks[n].hitbox.Width; collisionX++)
                                if (blocks[n].hitbox.hitbox[collisionY, collisionX] == true)
                                    if (blocks[n].cordinate.Y + collisionY == y && blocks[n].cordinate.X + collisionX == x)
                                    {
                                        System.Console.Write("■ ");

                                        flag = false;
                                    }

                    if (flag)
                        System.Console.Write("  ");
                }

                System.Console.Write("■\n");
            }

            for (int x = 0; x < width + 2; x++)
                System.Console.Write("■ ");

            System.Console.Write("\nSCORE : {0}", blocks.Length);
        }

        public void CreateBlock()
        {
            CheckLines();

            System.Array.Resize<Block.Block>(ref blocks, blocks.Length + 1);

            blocks[blocks.Length - 1] = new Block.Block(this);

            CheckLose();
        }

        public void FallBlock()
        {
            blocks[blocks.Length - 1].Fall();
        }

        public void StartGettingInput()
        {
            var thread = new Thread(() =>
            {
                while (isGameOver == false)
                {
                    char input = System.Console.ReadKey(true).KeyChar;

                    if (input == 'a' || input == 'd')
                        blocks[blocks.Length - 1].Move(input);
                    else if (input == 's')
                        blocks[blocks.Length - 1].Fall();
                    else if (input == 'w')
                        blocks[blocks.Length - 1].Rotate();
                }
            });

            thread.IsBackground = true;
            thread.Start();
        }

        public void CheckLines()
        {
            for (int y = 0; y < height; y++)
            {
                bool[] blocksThatLostPiece = new bool[blocks.Length];

                for (int i = 0; i < blocks.Length; i++)
                    blocksThatLostPiece[i] = false;

                int counter = 0;

                for (int n = 0; n < blocks.Length; n++)
                    for (int collisionY = 0; collisionY < blocks[n].hitbox.Height; collisionY++)
                        if (blocks[n].cordinate.Y + collisionY == y)
                        {
                            for (int collisionX = 0; collisionX < blocks[n].hitbox.Width; collisionX++)
                                if (blocks[n].hitbox.hitbox[collisionY, collisionX] == true)
                                    counter++;

                            blocksThatLostPiece[n] = true;
                        }

                if (counter == width)
                    for (int n = 0; n < blocks.Length; n++)
                        if (blocksThatLostPiece[n] == true)
                        {
                            for (int collisionY = y - blocks[n].cordinate.Y - 1; collisionY >= 0; collisionY--)
                                for (int collisionX = 0; collisionX < blocks[n].hitbox.Width; collisionX++)
                                    blocks[n].hitbox.hitbox[collisionY + 1, collisionX] = blocks[n].hitbox.hitbox[collisionY, collisionX];

                            ChangeCollisionAndCordinate(n);
                        }
                        else if (blocks[n].cordinate.Y + blocks[n].hitbox.Height - 1 < y)
                            blocks[n].cordinate.Y++;
            }
        }

        public void CheckLose()
        {
            if (blocks.Length > 1)
                if (blocks[blocks.Length - 2].cordinate.Y < 0)
                    isGameOver = true;
        }

        public void ChangeCollisionAndCordinate(int n)
        {
            BlockHitbox NewCollision = new BlockHitbox(blocks[n].hitbox.Height - 1, blocks[n].hitbox.Width);

            for (int y = 0; y < blocks[n].hitbox.Height - 1; y++)
                for (int x = 0; x < blocks[n].hitbox.Width; x++)
                    NewCollision.hitbox[y, x] = blocks[n].hitbox.hitbox[y + 1, x];

            blocks[n].hitbox = NewCollision;

            blocks[n].cordinate.Y++;
        }
    };
}

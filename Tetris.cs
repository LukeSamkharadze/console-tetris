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

        public int lengthY;
        public int lengthX;

        public bool isGameOver;

        public Block.Block[] blocks = new Block.Block[0];

        public Tetris(int lengthY, int lengthX)
        {
            isGameOver = false;

            this.lengthY = lengthY;
            this.lengthX = lengthX;

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
            return;
        } // DONE

        public void PrintGame()
        {
            bool flag;

            for (int x = 0; x < lengthX + 2; x++)
                System.Console.Write("■ ");

            System.Console.Write("\n");

            for (int y = 0; y < lengthY; y++)
            {
                System.Console.Write("■ ");

                for (int x = 0; x < lengthX; x++)
                {
                    flag = true;

                    for (int n = 0; n < blocks.Length; n++)
                    {
                        for (int collisionY = 0; blocks[n] != null && collisionY < blocks[n].collision.lengthY; collisionY++)
                        {
                            for (int collisionX = 0; collisionX < blocks[n].collision.lengthX; collisionX++)
                            {
                                if (blocks[n].collision.collision[collisionY, collisionX] == true)
                                {
                                    if (blocks[n].cordinate.y + collisionY == y && blocks[n].cordinate.x + collisionX == x)
                                    {
                                        System.Console.Write("■ ");

                                        flag = false;
                                    }
                                }
                            }
                        }
                    }

                    if (flag)
                        System.Console.Write("  ");
                }
                System.Console.Write("■\n");
            }

            for (int x = 0; x < lengthX + 2; x++)
                System.Console.Write("■ ");

            System.Console.Write("\nSCORE : {0}", blocks.Length);

            return;
        } // DONE

        public void CreateBlock()
        {
            CheckLines();

            System.Array.Resize<Block.Block>(ref blocks, blocks.Length + 1);

            blocks[blocks.Length - 1] = new Block.Block(this);

            CheckLose();

            return;
        } // DONE

        public void FallBlock()
        {
            blocks[blocks.Length - 1].Fall();

            return;
        } // DONE

        public void StartGettingInput()
        {
            var thread = new Thread(() => {
                while (isGameOver == false)
                {
                    char input = System.Console.ReadKey(true).KeyChar;

                    if (input == 'a' || input == 'd')
                    {
                        blocks[blocks.Length - 1].Move(input);
                    }
                    else if (input == 's')
                    {
                        blocks[blocks.Length - 1].Fall();
                    }
                    else if (input == 'w')
                    {
                        blocks[blocks.Length - 1].Rotate();
                    }
                }
            });

            thread.IsBackground = true;
            thread.Start();
        } // DONE

        public void CheckLines()
        {
            for (int y = 0; y < lengthY; y++)
            {
                bool[] blocksThatLostPiece = new bool[blocks.Length];

                for (int i = 0; i < blocks.Length; i++)
                {
                    blocksThatLostPiece[i] = false;
                }

                int counter = 0;

                for (int n = 0; n < blocks.Length; n++)
                {
                    for (int collisionY = 0; collisionY < blocks[n].collision.lengthY; collisionY++)
                    {
                        if (blocks[n].cordinate.y + collisionY == y)
                        {
                            for (int collisionX = 0; collisionX < blocks[n].collision.lengthX; collisionX++)
                            {
                                if (blocks[n].collision.collision[collisionY, collisionX] == true)
                                {
                                    counter++;
                                }
                            }
                            blocksThatLostPiece[n] = true;
                        }
                    }
                }

                if (counter == lengthX)
                {
                    for (int n = 0; n < blocks.Length; n++)
                    {
                        if (blocksThatLostPiece[n] == true)
                        {
                            for (int collisionY = y - blocks[n].cordinate.y - 1; collisionY >= 0; collisionY--)
                            {
                                for (int collisionX = 0; collisionX < blocks[n].collision.lengthX; collisionX++)
                                {
                                    blocks[n].collision.collision[collisionY + 1, collisionX] = blocks[n].collision.collision[collisionY, collisionX];
                                }
                            }

                            ChangeCollisionAndCordinate(n);
                        }
                        else if (blocks[n].cordinate.y + blocks[n].collision.lengthY - 1 < y)
                        {
                            blocks[n].cordinate.y++;
                        }
                    }
                }
            }
        } // DONE

        public void CheckLose()
        {
            if (blocks.Length > 1)
            {
                if (blocks[blocks.Length - 2].cordinate.y < 0)
                {
                    isGameOver = true;
                }
            }
        } // DONE

        public void ChangeCollisionAndCordinate(int n)
        {
            BlockHitbox NewCollision = new BlockHitbox(blocks[n].collision.lengthY - 1, blocks[n].collision.lengthX);

            for (int y = 0; y < blocks[n].collision.lengthY - 1; y++)
            {
                for (int x = 0; x < blocks[n].collision.lengthX; x++)
                {
                    NewCollision.collision[y, x] = blocks[n].collision.collision[y + 1, x];
                }
            }

            blocks[n].collision = NewCollision;

            blocks[n].cordinate.y++;
        } // DONE
    };
}

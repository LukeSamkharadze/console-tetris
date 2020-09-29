using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTetris.Block
{
    public class Block
    {
        public Tetris tetris;
        public BlockHitbox collision;
        public Cordinate cordinate;

        public Block(Tetris tetris)
        {
            this.tetris = tetris;

            collision = tetris.collisions[new System.Random().Next(0, tetris.collisions.Length)];

            cordinate = new Cordinate(new System.Random().Next(0, tetris.lengthX - collision.lengthX + 1), -collision.lengthY + 1);

            return;
        } // DONE

        public void Fall()
        {
            for (int n = 0; n < tetris.blocks.Length - 1; n++)
            {
                if (cordinate.x <= tetris.blocks[n].cordinate.x + tetris.blocks[n].collision.lengthX - 1 &&
                    cordinate.x + collision.lengthX - 1 >= tetris.blocks[n].cordinate.x &&
                    cordinate.y <= tetris.blocks[n].cordinate.y + tetris.blocks[n].collision.lengthY - 1 &&
                    cordinate.y + collision.lengthY >= tetris.blocks[n].cordinate.y)
                {
                    for (int collisionY = 0; collisionY < collision.lengthY; collisionY++)
                    {
                        for (int collisionX = 0; collisionX < collision.lengthX; collisionX++)
                        {
                            if (collision.collision[collisionY, collisionX] == true)
                            {
                                for (int collisionYY = 0; collisionYY < tetris.blocks[n].collision.lengthY; collisionYY++)
                                {
                                    for (int collisionXX = 0; collisionXX < tetris.blocks[n].collision.lengthX; collisionXX++)
                                    {
                                        if (tetris.blocks[n].collision.collision[collisionYY, collisionXX] == true)
                                        {
                                            if (cordinate.x + collisionX == tetris.blocks[n].cordinate.x + collisionXX &&
                                                cordinate.y + collisionY + 1 == tetris.blocks[n].cordinate.y + collisionYY)
                                            {
                                                tetris.CreateBlock();

                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (cordinate.y + collision.lengthY - 1 == tetris.lengthY - 1)
            {
                tetris.CreateBlock();

                return;
            }

            cordinate.y++;

            return;
        } // DONE

        public void Move(char input)
        {
            if (input == 'a')
            {
                for (int n = 0; n < tetris.blocks.Length - 1; n++)
                {
                    if (cordinate.x - 1 <= tetris.blocks[n].cordinate.x + tetris.blocks[n].collision.lengthX - 1 &&
                        cordinate.x + collision.lengthX - 1 >= tetris.blocks[n].cordinate.x &&
                        cordinate.y <= tetris.blocks[n].cordinate.y + tetris.blocks[n].collision.lengthY - 1 &&
                        cordinate.y + collision.lengthY - 1 >= tetris.blocks[n].cordinate.y)
                    {
                        for (int collisionY = 0; collisionY < collision.lengthY; collisionY++)
                        {
                            for (int collisionX = 0; collisionX < collision.lengthX; collisionX++)
                            {
                                if (collision.collision[collisionY, collisionX] == true)
                                {
                                    for (int collisionYY = 0; collisionYY < tetris.blocks[n].collision.lengthY; collisionYY++)
                                    {
                                        for (int collisionXX = 0; collisionXX < tetris.blocks[n].collision.lengthX; collisionXX++)
                                        {
                                            if (tetris.blocks[n].collision.collision[collisionYY, collisionXX] == true)
                                            {
                                                if (cordinate.x + collisionX - 1 == tetris.blocks[n].cordinate.x + collisionXX &&
                                                    cordinate.y + collisionY == tetris.blocks[n].cordinate.y + collisionYY)
                                                {
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (cordinate.x != 0)
                {
                    cordinate.x--;
                }

            }
            else if (input == 'd')
            {
                for (int n = 0; n < tetris.blocks.Length - 1; n++)
                {
                    if (cordinate.x <= tetris.blocks[n].cordinate.x + tetris.blocks[n].collision.lengthX - 1 &&
                        cordinate.x + collision.lengthX >= tetris.blocks[n].cordinate.x &&
                        cordinate.y <= tetris.blocks[n].cordinate.y + tetris.blocks[n].collision.lengthY - 1 &&
                        cordinate.y + collision.lengthY - 1 >= tetris.blocks[n].cordinate.y)
                    {
                        for (int collisionY = 0; collisionY < collision.lengthY; collisionY++)
                        {
                            for (int collisionX = 0; collisionX < collision.lengthX; collisionX++)
                            {
                                if (collision.collision[collisionY, collisionX] == true)
                                {
                                    for (int collisionYY = 0; collisionYY < tetris.blocks[n].collision.lengthY; collisionYY++)
                                    {
                                        for (int collisionXX = 0; collisionXX < tetris.blocks[n].collision.lengthX; collisionXX++)
                                        {
                                            if (tetris.blocks[n].collision.collision[collisionYY, collisionXX] == true)
                                            {
                                                if (cordinate.x + collisionX + 1 == tetris.blocks[n].cordinate.x + collisionXX &&
                                                    cordinate.y + collisionY == tetris.blocks[n].cordinate.y + collisionYY)
                                                {
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (cordinate.x + collision.lengthX != tetris.lengthX)
                {
                    cordinate.x++;
                }

            }

        } // DONE

        public void Rotate()
        {
            bool flag = true;
            BlockHitbox newCollision = new BlockHitbox(collision.lengthX, collision.lengthY);

            for (int x = 0; x < collision.lengthX; x++)
            {
                for (int y = 0; y < collision.lengthY; y++)
                {
                    newCollision.collision[x, y] = collision.collision[collision.lengthY - 1 - y, x];
                }
            }

            for (int newCollisionY = 0; newCollisionY < newCollision.lengthY; newCollisionY++)
            {
                for (int newCollisionX = 0; newCollisionX < newCollision.lengthX; newCollisionX++)
                {
                    for (int n = 0; n < tetris.blocks.Length; n++)
                    {
                        if (this != tetris.blocks[n])
                        {
                            for (int collisionY = 0; collisionY < tetris.blocks[n].collision.lengthY; collisionY++)
                            {
                                for (int collisionX = 0; collisionX < tetris.blocks[n].collision.lengthX; collisionX++)
                                {
                                    if (tetris.blocks[n].cordinate.y + collisionY == cordinate.y + newCollisionY &&
                                        tetris.blocks[n].cordinate.x + collisionX == cordinate.x + newCollisionX)
                                    {

                                        flag = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }


            if (cordinate.x + newCollision.lengthX > tetris.lengthX)
            {
                flag = false;
            }

            if (cordinate.y + newCollision.lengthY > tetris.lengthY)
            {
                flag = false;
            }

            if (flag)
            {
                collision = newCollision;
            }
        } // DONE
    };

}

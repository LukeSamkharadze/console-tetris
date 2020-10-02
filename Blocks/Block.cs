using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTetris.Blocks
{
    public class Block
    {
        public Tetris tetris;
        public BlockHitbox hitbox;
        public Cordinate cordinate;

        public Block(Tetris tetris)
        {
            this.tetris = tetris;

            hitbox = tetris.collisions[new System.Random().Next(0, tetris.collisions.Length)];

            cordinate = new Cordinate(new System.Random().Next(0, tetris.Width - hitbox.Width + 1), -hitbox.Height + 1);
        }

        public void Fall()
        {
            for (int n = 0; n < tetris.blocks.Length - 1; n++)
                if (cordinate.X <= tetris.blocks[n].cordinate.X + tetris.blocks[n].hitbox.Width - 1 &&
                    cordinate.X + hitbox.Width - 1 >= tetris.blocks[n].cordinate.X &&
                    cordinate.Y <= tetris.blocks[n].cordinate.Y + tetris.blocks[n].hitbox.Height - 1 &&
                    cordinate.Y + hitbox.Height >= tetris.blocks[n].cordinate.Y)
                    for (int hitboxY = 0; hitboxY < hitbox.Height; hitboxY++)
                        for (int hitboxX = 0; hitboxX < hitbox.Width; hitboxX++)
                            if (hitbox.hitbox[hitboxY, hitboxX] == true)
                                for (int hitboxYY = 0; hitboxYY < tetris.blocks[n].hitbox.Height; hitboxYY++)
                                    for (int hitboxXX = 0; hitboxXX < tetris.blocks[n].hitbox.Width; hitboxXX++)
                                        if (tetris.blocks[n].hitbox.hitbox[hitboxYY, hitboxXX] == true)
                                            if (cordinate.X + hitboxX == tetris.blocks[n].cordinate.X + hitboxXX &&
                                                cordinate.Y + hitboxY + 1 == tetris.blocks[n].cordinate.Y + hitboxYY)
                                            {
                                                tetris.CreateBlock();
                                                return;
                                            }

            if (cordinate.Y + hitbox.Height - 1 == tetris.Height - 1)
            {
                tetris.CreateBlock();
                return;
            }

            cordinate.Y++;
        }

        public void Move(char input)
        {
            Dictionary<char, int> dX = new Dictionary<char, int>() { { 'a' , -1 }, { 'd' , 1 } };

            for (int n = 0; n < tetris.blocks.Length - 1; n++)
                if (cordinate.X + dX[input] <= tetris.blocks[n].cordinate.X + tetris.blocks[n].hitbox.Width - 1 &&
                    cordinate.X + hitbox.Width + dX[input] >= tetris.blocks[n].cordinate.X &&
                    cordinate.Y <= tetris.blocks[n].cordinate.Y + tetris.blocks[n].hitbox.Height - 1 &&
                    cordinate.Y + hitbox.Height - 1 >= tetris.blocks[n].cordinate.Y)
                    for (int hitboxY = 0; hitboxY < hitbox.Height; hitboxY++)
                        for (int hitboxX = 0; hitboxX < hitbox.Width; hitboxX++)
                            if (hitbox.hitbox[hitboxY, hitboxX] == true)
                                for (int hitboxYY = 0; hitboxYY < tetris.blocks[n].hitbox.Height; hitboxYY++)
                                    for (int hitboxXX = 0; hitboxXX < tetris.blocks[n].hitbox.Width; hitboxXX++)
                                        if (tetris.blocks[n].hitbox.hitbox[hitboxYY, hitboxXX] == true)
                                            if (cordinate.X + hitboxX + dX[input] == tetris.blocks[n].cordinate.X + hitboxXX &&
                                                cordinate.Y + hitboxY == tetris.blocks[n].cordinate.Y + hitboxYY)
                                                return;

            if (cordinate.X + hitbox.Width * (dX[input] + 1) / 2 != tetris.Width * (dX[input]+1)/2)
                cordinate.X += dX[input];
        }

        public void Rotate()
        {
            bool flag = true;

            BlockHitbox newHitbox = new BlockHitbox(hitbox.Width, hitbox.Height);

            for (int x = 0; x < hitbox.Width; x++)
                for (int y = 0; y < hitbox.Height; y++)
                    newHitbox.hitbox[x, y] = hitbox.hitbox[hitbox.Height - 1 - y, x];

            for (int newHitboxY = 0; newHitboxY < newHitbox.Height; newHitboxY++)
                for (int newHitboxX = 0; newHitboxX < newHitbox.Width; newHitboxX++)
                    for (int n = 0; n < tetris.blocks.Length; n++)
                        if (this != tetris.blocks[n])
                            for (int hitboxY = 0; hitboxY < tetris.blocks[n].hitbox.Height; hitboxY++)
                                for (int hitboxX = 0; hitboxX < tetris.blocks[n].hitbox.Width; hitboxX++)
                                    if (tetris.blocks[n].cordinate.Y + hitboxY == cordinate.Y + newHitboxY &&
                                        tetris.blocks[n].cordinate.X + hitboxX == cordinate.X + newHitboxX)
                                        flag = false;

            if (cordinate.X + newHitbox.Width > tetris.Width)
                flag = false;

            if (cordinate.Y + newHitbox.Height > tetris.Height)
                flag = false;

            if (flag)
                hitbox = newHitbox;
        }
    };

}

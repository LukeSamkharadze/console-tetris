using ConsoleTetris;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Tetris tetris = new Tetris(25, 15);

            tetris.CreateBlock();
            tetris.StartGettingInput();

            while (tetris.isGameOver == false)
            {
                tetris.PrintGame();
                tetris.FallBlock();

                System.Threading.Thread.Sleep(75);
                System.Console.Clear();
            }

            System.Console.Write("Game Over");
            System.Threading.Thread.Sleep(1750);
        }
    };
}
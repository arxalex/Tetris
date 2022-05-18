using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Tetris
{
    public class Program
    {
        public static void Main()
        {
            ConsoleKeyInfo exit;
            do
            {
                PlayTetris.Play();
                Console.WriteLine("Press Enter to restart");
                exit = Console.ReadKey();
            }
            while (exit.Key == ConsoleKey.Enter);
        }
    }
}
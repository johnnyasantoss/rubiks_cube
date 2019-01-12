﻿using System;
using RubiksCube.Core.Cube;
using RubiksCube.Core.Formatting;

namespace RubiksCube.Core
{
    static class Program
    {
        private static void Main(string[] args)
        {
            var r = new Cube.RubiksCube(3);

            ConsoleKey key;
            var consoleFormatter = new ConsoleFormatter(r);

            do
            {
                Console.Clear();
                Console.WriteLine("Press Q to exit.");
                Console.WriteLine("Any other key will only refresh the screen");
                Console.WriteLine();

                consoleFormatter.Render();

                key = Console.ReadKey(true)
                    .Key;

            } while (key != ConsoleKey.Q);
        }
    }
}

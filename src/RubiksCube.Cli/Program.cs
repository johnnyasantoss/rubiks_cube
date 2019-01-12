﻿using System;
using RubiksCube.Core;
using RubiksCube.Formatting.Console;

namespace RubiksCube.Cli
{
    static class Program
    {
        private static void Main(string[] args)
        {
            var size = (ushort)3;

            if (args.Length > 0)
            {
                ushort.TryParse(args[0], out size);
            }

            var r = new Core.RubiksCube(size);

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

//                var input = ReadLine.Read("(move)> ");

                r.Turn(SimpleDirectionTurn.Top);
            } while (key != ConsoleKey.Q);
        }
    }
}
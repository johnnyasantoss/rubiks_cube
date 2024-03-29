﻿using System;
using RubiksCube.Expressions.Parsers;
using RubiksCube.Formatting.Console;

namespace RubiksCube.Cli
{
    static class Program
    {
        private static void Main(string[] args)
        {
            var size = ReadArgs(args);

            var cube = new Core.RubiksCube(size);

            //TODO: Make the formatter something configurable
            var consoleFormatter = new ConsoleFormatter(cube);
            RubiksCubeExpressionParser parser = new RubiksCubeSimpleExpressionParser(cube);

            SetupReadline();

            Console.Clear();

            do
            {
                Console.WriteLine("Use the command \"help\".");
                Console.WriteLine();

                consoleFormatter.Render();

                var input = ReadLine.Read("(move)> ")
                    .Replace("\0", string.Empty);

                //TODO: Implement help command

                if (CheckExitExpression(input))
                    break;

                var result = parser.Parse(input);

                if (!result.Success)
                {
                    Console.Clear();
                    Console.Error.WriteLine(result.ErrorMessage);
                    continue;
                }

                foreach (var movement in result.Movements)
                {
                    cube.Turn(movement);
                }

                Console.Clear();
            } while (true);
        }

        private static ushort ReadArgs(string[] args)
        {
            var size = (ushort)3;

            if (args.Length > 0)
            {
                ushort.TryParse(args[0], out size);
            }

            return size;
        }

        private static void SetupReadline()
        {
            ReadLine.AutoCompletionHandler = new Test();
            ReadLine.HistoryEnabled = true;
        }

        private static bool CheckExitExpression(string input)
        {
            input = input.ToUpperInvariant();

            return input == "QUIT"
                || input == "Q"
                || input == "EXIT";
        }
    }

    internal class Test : IAutoCompleteHandler
    {
        public string[] GetSuggestions(string text, int index)
        {
            return new[]
            {
                "TOP", "LEFT"
            };
        }

        public char[] Separators { get; set; } =
        {
            ' ', '\''
        };
    }
}

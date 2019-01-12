using System;
using RubiksCube.Expressions;
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

            var cube = new Core.RubiksCube(size);

            //TODO: Make the formatter something configurable
            var consoleFormatter = new ConsoleFormatter(cube);
            RubiksCubeExpressionParser parser = new RubiksCubeSimpleExpressionParser();

            Console.Clear();

            do
            {
                Console.WriteLine("Use the command \"help\".");
                Console.WriteLine();

                consoleFormatter.Render();

                var input = ReadLine.Read("(move)> ");

                //TODO: Check help command

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

        private static bool CheckExitExpression(string input)
        {
            input = input.ToUpperInvariant();

            return input == "QUIT"
                || input == "Q"
                || input == "EXIT";
        }
    }
}

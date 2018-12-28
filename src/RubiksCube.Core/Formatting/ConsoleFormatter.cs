using System;
using RubiksCube.Core.Cube;

namespace RubiksCube.Core.Formatting
{
    public class ConsoleFormatter : ICubeFormatter
    {
        private readonly Cube.RubiksCube _cube;

        private const string Block = "█";

        public ConsoleFormatter(Cube.RubiksCube cube)
        {
            _cube = cube;
        }

        public void Render()
        {
            Console.ResetColor();

            foreach (var cubeFace in _cube.Faces)
            {
                for (var x = 0; x < cubeFace.Size; x++)
                {
                    for (var y = 0; y < cubeFace.Size; y++)
                    {
                        var currentSlot = cubeFace.Slots[x, y];

                        Console.ForegroundColor =
                            Console.BackgroundColor =
                                ToConsoleColor(currentSlot.Color);

                        Console.Write(Block);
                        Console.Write(Block);
                    }

                    Console.WriteLine();
                }
            }

            Console.ResetColor();
        }

        private ConsoleColor ToConsoleColor(CubeColor color)
        {
            if (color == CubeColor.Red)
                return ConsoleColor.Red;

            if (color == CubeColor.Blue)
                return ConsoleColor.Blue;

            if (color == CubeColor.Green)
                return ConsoleColor.Green;

            if (color == CubeColor.White)
                return ConsoleColor.White;

            if (color == CubeColor.Orange)
                return ConsoleColor.DarkYellow;

            if (color == CubeColor.Yellow)
                return ConsoleColor.Yellow;

            throw new Exception("Color not expected");
        }
    }
}

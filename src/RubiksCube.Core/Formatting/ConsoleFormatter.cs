using System;
using RubiksCube.Core.Cube;
using Alba.CsConsoleFormat;

namespace RubiksCube.Core.Formatting
{
    public class ConsoleFormatter : ICubeFormatter
    {
        private readonly Cube.RubiksCube _cube;

        private const string Block = "██";

        public ConsoleFormatter(Cube.RubiksCube cube)
        {
            _cube = cube;
        }

        public void Render()
        {
            Console.ResetColor();

            var size = _cube.FrontFace.Size * Block.Length;

            var grid = new Grid
            {
                Stroke = LineThickness.None
                , Columns =
                {
                    GridLength.Char(size)
                    , GridLength.Char(size)
                    , GridLength.Char(size)
                }
                , Align = Align.Center
            };

            var document = new Document(grid);

            var paddingPlaceholders = new Cell[3];

            foreach (var cubeFace in _cube.Faces)
            {
                var padding = GetPaddingCrossStyleByCubeFace(cubeFace);

                if (ShouldBreakCrossStyle(cubeFace))
                    paddingPlaceholders = new[]
                    {
                        new Cell(), new Cell(), new Cell()
                    };

                for (var x = 0; x < cubeFace.Size; x++)
                {
                    for (var y = 0; y < cubeFace.Size; y++)
                    {
                        var currentSlot = cubeFace.Slots[x, y];

                        paddingPlaceholders[padding]
                            .Children.Add(
                                new Span(Block)
                                {
                                    Color = ToConsoleColor(currentSlot.Color)
                                }
                            );
                    }
                }

                if (!ShouldBreakCrossStyle(cubeFace))
                    continue;

                foreach (var placeholder in paddingPlaceholders)
                    grid.Children.Add(placeholder);
            }

            ConsoleRenderer.RenderDocument(document);

            Console.ResetColor();
        }

        private int GetPaddingCrossStyleByCubeFace(CubeFace cubeFace)
        {
            if (cubeFace == _cube.LeftFace)
                return 0;

            if (cubeFace == _cube.RightFace)
                return 2;

            return 1;
        }

        private bool ShouldBreakCrossStyle(CubeFace cubeFace)
            => cubeFace != _cube.FrontFace && cubeFace != _cube.RightFace;

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

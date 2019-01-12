using System;
using Xunit;

namespace RubiksCube.Core.UnitTests.Cube
{
    public class RubiksCubeTests
    {
        [Theory]
        [InlineData(SimpleDirectionTurn.Top, 0, false)]
        [InlineData(SimpleDirectionTurn.TopReverse, 0, true)]
        public void CanTurnRow(SimpleDirectionTurn directionTurn, int rowIndex, bool isReverse)
        {
            var cube = new RubiksCube(3);

            cube.Turn(directionTurn);

            AssertRowTurn(cube, rowIndex, isReverse);
        }

        private static void AssertRowTurn(RubiksCube cube, int rowIndex, bool isReverse)
        {
            if (rowIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(rowIndex));

            var cubeFrontFace = cube.FrontFace;

            var nextFace = isReverse
                ? cube.LeftFace
                : cube.RightFace;

            for (var i = 0; i < cubeFrontFace.Size; i++)
            {
                Assert.Equal(
                    cubeFrontFace.Slots[rowIndex, i]
                        .Color
                    , nextFace.CenterColor
                );
            }
        }
    }
}

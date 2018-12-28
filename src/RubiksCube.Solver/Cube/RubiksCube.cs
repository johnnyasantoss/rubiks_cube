using System;

namespace RubiksCube.Solver.Cube
{
    public class RubiksCube
    {
        public const ushort Size = 6;
        private readonly CubeFace[] _faces;

        public RubiksCube(ushort size)
        {
            _faces = new CubeFace[Size]
            {
                new CubeFace(CubeColor.White, size)
                , new CubeFace(CubeColor.Orange, size)
                , new CubeFace(CubeColor.Green, size)
                , new CubeFace(CubeColor.Yellow, size)
                , new CubeFace(CubeColor.Red, size)
                , new CubeFace(CubeColor.Blue, size)
            };

            SanityCheck();
        }

        private void SanityCheck()
        {
            var faceSize = -1;

            for (var i = 0; i < Size; i++)
            {
                var currentFace = _faces[i];

                if (faceSize == -1)
                    faceSize = currentFace.Size;

                if (faceSize != currentFace.Size)
                    throw new Exception("One face doesn't have the same size as the others");

                for (var j = 0; j < currentFace.Size; j++)
                for (var k = 0; k < currentFace.Size; k++)
                {
                    var currentSlot = currentFace.Slots[j, k];

                    if (currentSlot == null)
                        throw new Exception($"Invalid slot @ {currentFace.CenterColor}: {j}, {k}");
                }
            }
        }
    }
}

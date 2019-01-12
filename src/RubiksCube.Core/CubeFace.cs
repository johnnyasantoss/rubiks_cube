using System;

namespace RubiksCube.Core.Cube
{
    public class CubeFace
    {
        public CubeFace(CubeColor centerColor, ushort size)
        {
            if (size < 2)
                throw new Exception("Invalid cube face size");

            CenterColor = centerColor;
            Size = size;
            Slots = new CubeSlot[size, size];

            Initialize();
        }

        public CubeSlot[,] Slots { get; }
        public CubeColor CenterColor { get; }
        public ushort Size { get; }

        private void Initialize()
        {
            InitializeSlots();
        }

        private void InitializeSlots()
        {
            for (var i = 0; i < Size; i++)
            for (var j = 0; j < Size; j++)
            {
                var slotType = GetSlotType(i, j);
                Slots[i, j] = new CubeSlot(CenterColor, slotType);
            }
        }

        private CubeSlotType GetSlotType(int x, int y)
        {
            var biggest = Size - 1;

            if ((x == 0 || x == biggest) && (y == 0 || y == biggest))
                return CubeSlotType.Edge;

            var biggestWithoutEdge = biggest - 1;
            var lowestWithoutEdge = 0 + 1;

            if ((x <= biggestWithoutEdge && x >= lowestWithoutEdge)
                && (y <= biggestWithoutEdge && y >= lowestWithoutEdge))
                return CubeSlotType.Center;

            return CubeSlotType.Side;
        }
    }
}

using RubiksCube.Core.Enums;

namespace RubiksCube.Core
{
    public class CubeSlot
    {
        public CubeSlot(CubeColor color, CubeSlotType type)
        {
            Color = color;
            Type = type;
        }

        public CubeColor Color { get; }

        public CubeSlotType Type { get; }
    }
}

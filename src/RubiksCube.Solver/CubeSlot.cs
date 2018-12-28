namespace RubiksCube.Solver
{
    class CubeSlot
    {
        public CubeSlot(CubeColor color, SlotType type)
        {
            Color = color;
            Type = type;
        }

        public CubeColor Color { get; }
        public SlotType Type { get; }
    }
}

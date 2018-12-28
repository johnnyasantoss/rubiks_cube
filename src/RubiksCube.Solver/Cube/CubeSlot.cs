namespace RubiksCube.Solver.Cube
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

namespace RubiksCube.Core
{
    public struct RubiksCubeMovement
    {
        public TurnType TurnType { get; set; }

        public ushort Indexer { get; set; }

        public TurnDirection Direction { get; set; }
    }
}

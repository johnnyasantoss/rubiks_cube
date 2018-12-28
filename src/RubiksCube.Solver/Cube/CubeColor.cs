namespace RubiksCube.Solver.Cube
{
    public class CubeColor
    {
        public static CubeColor Red = new CubeColor("Red");

        public static CubeColor Green = new CubeColor("Green");

        public static CubeColor Yellow = new CubeColor("Yellow");

        public static CubeColor White = new CubeColor("White");

        public static CubeColor Blue = new CubeColor("Blue");

        public static CubeColor Orange = new CubeColor("Orange");

        private CubeColor(string description)
        {
            Description = description;
        }

        public string Description { get; }

        public override string ToString()
          => Description;
    }
}

namespace RubiksCube.Core.Cube
{
    public class CubeColor
    {
        public static readonly CubeColor Red = new CubeColor("Red");

        public static readonly CubeColor Green = new CubeColor("Green");

        public static readonly CubeColor Yellow = new CubeColor("Yellow");

        public static readonly CubeColor White = new CubeColor("White");

        public static readonly CubeColor Blue = new CubeColor("Blue");

        public static readonly CubeColor Orange = new CubeColor("Orange");

        private CubeColor(string description)
        {
            Description = description;
        }

        public string Description { get; }

        public override string ToString()
            => Description;
    }
}

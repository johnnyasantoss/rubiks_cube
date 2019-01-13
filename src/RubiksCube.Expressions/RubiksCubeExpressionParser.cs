namespace RubiksCube.Expressions
{
    public abstract class RubiksCubeExpressionParser
    {
        protected Core.RubiksCube Cube { get; }

        public RubiksCubeExpressionParser(Core.RubiksCube cube)
        {
            Cube = cube;
        }

        public abstract ParseResult Parse(string expression);
    }
}

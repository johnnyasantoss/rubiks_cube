namespace RubiksCube.Expressions.Parsers
{
    public abstract class RubiksCubeExpressionParser
    {
        public RubiksCubeExpressionParser(Core.RubiksCube cube)
        {
            Cube = cube;
        }

        protected Core.RubiksCube Cube { get; }

        public abstract ParseResult Parse(string expression);
    }
}

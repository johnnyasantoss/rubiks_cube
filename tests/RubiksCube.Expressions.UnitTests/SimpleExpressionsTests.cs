using System.Linq;
using RubiksCube.Expressions.Parsers;
using Xunit;

namespace RubiksCube.Expressions.UnitTests
{
    public class SimpleExpressionsTests
    {
        private readonly RubiksCubeSimpleExpressionParser _parser;

        public SimpleExpressionsTests()
        {
            var cube = new Core.RubiksCube(3);
            _parser = new RubiksCubeSimpleExpressionParser(cube);
        }

        [Theory]
        [InlineData("top")]
        [InlineData("TOP")]
        [InlineData("T")]
        [InlineData("L")]
        [InlineData("B")]
        public void ShouldBeAbleParseSimpleExpressions(string expression)
        {
            var result = _parser.Parse(expression);

            Assert.True(result.Success);
            Assert.Single(result.Movements);
        }

        [Theory]
        [InlineData("tttt")]
        [InlineData("rltb")]
        [InlineData("utrlbblrt")]
        public void ShouldBeAbleParseMultipleExpressions(string expression)
        {
            var result = _parser.Parse(expression);

            Assert.True(result.Success);
            Assert.True(result.Movements.Count() > 1);
        }

        [Theory]
        [InlineData("RL'TB", "rltb")]
        public void ShouldBeAbleParseMultipleExpressionsInACorrectOrder(string expected, string expression)
        {
            var result = _parser.Parse(expression);

            //TODO: assertion
        }
    }
}

using System.Linq;
using Xunit;

namespace RubiksCube.Expressions.UnitTests
{
    public class SimpleExpressionsTests
    {
        [Theory]
        [InlineData("top")]
        [InlineData("TOP")]
        [InlineData("T")]
        [InlineData("L")]
        [InlineData("B")]
        public void ShouldBeAbleParseSimpleExpressions(string expression)
        {
            var parser = new RubiksCubeSimpleExpressionParser();

            var result = parser.Parse(expression);

            Assert.True(result.Success);
            Assert.Single(result.Movements);
        }

        [Theory]
        [InlineData("tttt")]
        [InlineData("rltb")]
        [InlineData("utrlbblrt")]
        public void ShouldBeAbleParseMultipleExpressions(string expression)
        {
            var parser = new RubiksCubeSimpleExpressionParser();

            var result = parser.Parse(expression);

            Assert.True(result.Success);
            Assert.True(result.Movements.Count() > 1);
        }

        [Theory]
        [InlineData("RL'TB", "rltb")]
        public void ShouldBeAbleParseMultipleExpressionsInACorrectOrder(string expected, string expression)
        {
            var parser = new RubiksCubeSimpleExpressionParser();

            var result = parser.Parse(expression);

            //TODO: assertion
        }
    }
}

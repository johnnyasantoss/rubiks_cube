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
    }
}

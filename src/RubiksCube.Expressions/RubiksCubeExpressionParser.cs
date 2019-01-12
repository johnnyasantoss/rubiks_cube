using System.Collections.Generic;

namespace RubiksCube.Expressions
{
    public abstract class RubiksCubeExpressionParser
    {
        public abstract IEnumerable<RubiksCubeExpression> Parse();
    }
}

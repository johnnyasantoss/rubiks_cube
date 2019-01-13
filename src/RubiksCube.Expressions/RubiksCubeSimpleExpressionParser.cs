using System;
using System.Collections.Generic;
using System.Linq;
using RubiksCube.Core;

namespace RubiksCube.Expressions
{
    public class RubiksCubeSimpleExpressionParser : RubiksCubeExpressionParser
    {
        /// <summary>
        /// All simple expressions (in upper case invariant)
        /// </summary>
        private readonly KnowExpression[] _knownExpressions;

        private static readonly KnowExpressionBuilder[] KnowExpressionBuilders =
        {
            new KnowSimpleExpressionBuilder("TOP", "T", TurnType.Line, true)
            , new KnowSimpleExpressionBuilder("BOTTOM", "B", TurnType.Line, false)
            , new KnowSimpleExpressionBuilder("LEFT", "L", TurnType.Column, true)
            , new KnowSimpleExpressionBuilder("FRONT", "F", TurnType.Column, false)
            , new KnowSimpleExpressionBuilder("RIGHT", "R", TurnType.Column, false)
            , new KnowSimpleExpressionBuilder("BACK", "U", TurnType.Column, true)
        };

        public RubiksCubeSimpleExpressionParser(Core.RubiksCube cube)
            : base(cube)
        {
            _knownExpressions = BuildAndSortByBiggerExpression(
                cube
                , KnowExpressionBuilders
            );
        }

        private static KnowExpression[] BuildAndSortByBiggerExpression(
            Core.RubiksCube cube
            , KnowExpressionBuilder[] values
        )
        {
            return values
                .SelectMany(m => m.BuildAllVariants(cube.Size))
                .OrderByDescending(m => m.Expression.Length)
                .ToArray();
        }

        public override ParseResult Parse(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                return new ParseResult("Expression can't be null.");

            var expressionRef = new Ref<string>(ref expression);

            var movements = GetMatchExpressions(expressionRef)
                .OrderBy(m => m.index)
                .Select(m => m.movement)
                .ToArray();

            expression = expressionRef.Value;

            return string.IsNullOrWhiteSpace(expression)
                ? new ParseResult(movements)
                : new ParseResult("Couldn't parse the whole expression");
        }

        private IEnumerable<(int index, RubiksCubeMovement movement)> GetMatchExpressions(Ref<string> expressionRef)
        {
            var expression = expressionRef.Value.ToUpperInvariant();

            foreach (var knownExpression in _knownExpressions)
            {
                if (string.IsNullOrWhiteSpace(expression))
                    break;

                int index;

                do
                {
                    index = expression.IndexOf(knownExpression.Expression, StringComparison.Ordinal);

                    if (index < 0)
                        break; // breaks do-while

                    expression = ReplaceWithWhiteSpace(expression, index, knownExpression.Expression.Length);

                    yield return (index, new RubiksCubeMovement
                    {
                        Indexer = knownExpression.Indexer
                        , Direction = knownExpression.IsReverse
                            ? TurnDirection.Reverse
                            : TurnDirection.Normal
                        , TurnType = knownExpression.TurnType
                    });
                } while (index >= 0);
            }

            expressionRef.Value = expression;
        }

//TODO: Extract this to a extension method
        private static string ReplaceWithWhiteSpace(string expression, int from, int len)
        {
            var whitespace = new string(' ', len);
            var before = expression.Substring(0, from);
            var rest = expression.Substring(from + len);
            expression = before + whitespace + rest;
            return expression;
        }
    }

//TODO: Move this to a utility lib
    public class Ref<T>
    {
        public Ref(ref T value)
        {
            Value = value;
        }

        public T Value { get; set; }
    }
}

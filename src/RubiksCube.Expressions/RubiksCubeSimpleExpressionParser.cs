using System;
using System.Collections.Generic;
using System.Linq;
using RubiksCube.Core;

namespace RubiksCube.Expressions
{
    public class RubiksCubeSimpleExpressionParser : RubiksCubeExpressionParser
    {
        private const int SimpleIndexer = 0;

        /// <summary>
        /// All simple expressions (in upper case invariant)
        /// </summary>
        private static readonly Dictionary<TurnType, string[]> KnownExpressions = new Dictionary<TurnType, string[]>
        {
            {
                TurnType.Line, new[]
                {
                    // TOP
                    "TOP'"
                    , "T'"
                    , "TOP"
                    , "T"

                    // BOTTOM
                    , "BOTTOM'"
                    , "B'"
                    , "BOTTOM"
                    , "B"
                }
            }
            ,
            {
                TurnType.Column, new[]
                {
                    // LEFT
                    "LEFT'"
                    , "L'"
                    , "LEFT"
                    , "L"

                    // FRONT
                    , "FRONT'"
                    , "F'"
                    , "FRONT"
                    , "F"

                    // RIGHT
                    , "RIGHT'"
                    , "R'"
                    , "RIGHT"
                    , "R"

                    // BACK
                    , "BACK'"
                    , "U'"
                    , "BACK"
                    , "U"
                }
            }
        };

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

            foreach (var key in KnownExpressions.Keys)
            foreach (var knownExpression in KnownExpressions[key])
            {
                if (string.IsNullOrWhiteSpace(expression))
                    break;

                int index;

                do
                {
                    index = expression.IndexOf(knownExpression, StringComparison.Ordinal);

                    if (index < 0)
                        break; // breaks do-while

                    expression = ReplaceWithWhiteSpace(expression, index, knownExpression.Length);

                    yield return (index, new RubiksCubeMovement
                    {
                        Indexer = SimpleIndexer
                        , Direction = knownExpression.Contains('\'')
                            ? TurnDirection.Reverse
                            : TurnDirection.Normal
                        , TurnType = key
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

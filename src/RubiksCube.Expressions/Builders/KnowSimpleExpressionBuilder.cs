using System;
using RubiksCube.Core.Enums;

namespace RubiksCube.Expressions.Builders
{
    internal class KnowSimpleExpressionBuilder : KnowExpressionBuilder
    {
        public KnowSimpleExpressionBuilder(string longVersion, string shortVersion, TurnType turnType)
            : base(longVersion, shortVersion, turnType)
        {
        }

        public bool IsZeroIndex { get; set; }

        public bool IsMiddle { get; set; }

        protected override ushort GetIndexer(ushort cubeSize)
        {
            if (IsZeroIndex)
                return 0;

            if (IsMiddle)
                return (ushort)(Math.Round(cubeSize / 2D) - 1D);

            return (ushort)(cubeSize - 1);
        }
    }
}

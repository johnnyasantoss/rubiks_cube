using RubiksCube.Core.Enums;

namespace RubiksCube.Expressions.Builders
{
    internal class KnowSimpleExpressionBuilder : KnowExpressionBuilder
    {
        public KnowSimpleExpressionBuilder(string longVersion, string shortVersion, TurnType turnType, bool isZeroIndex)
            : base(longVersion, shortVersion, turnType)
        {
            IsZeroIndex = isZeroIndex;
        }

        public bool IsZeroIndex { get; }

        protected override ushort GetIndexer(ushort cubeSize)
            => (ushort)(IsZeroIndex
                ? 0
                : cubeSize - 1);
    }
}

using RubiksCube.Core;

namespace RubiksCube.Expressions
{
    internal class KnowSimpleExpressionBuilder : KnowExpressionBuilder
    {
        public bool IsZeroIndex { get; }

        public KnowSimpleExpressionBuilder(string longVersion, string shortVersion, TurnType turnType, bool isZeroIndex)
            : base(longVersion, shortVersion, turnType)
        {
            IsZeroIndex = isZeroIndex;
        }

        protected override ushort GetIndexer(ushort cubeSize)
            => (ushort)(IsZeroIndex
                ? 0
                : cubeSize - 1);
    }
}

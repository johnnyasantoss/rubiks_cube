using RubiksCube.Core.Enums;

namespace RubiksCube.Expressions.Builders
{
    internal abstract class KnowExpressionBuilder
    {
        public const string ReverseChar = "'";

        private readonly string _longVersion;
        private readonly string _shortVersion;
        private readonly TurnType _turnType;

        public KnowExpressionBuilder(string longVersion, string shortVersion, TurnType turnType)
        {
            _turnType = turnType;
            _longVersion = longVersion.ToUpperInvariant();
            _shortVersion = shortVersion.ToUpperInvariant();
        }

        public KnowExpression[] BuildAllVariants(ushort cubeSize)
        {
            var indexer = GetIndexer(cubeSize);

            return new[]
            {
                new KnowExpression(_longVersion + ReverseChar, indexer, true, _turnType)
                , new KnowExpression(_longVersion, indexer, false, _turnType)
                , new KnowExpression(_shortVersion + ReverseChar, indexer, true, _turnType)
                , new KnowExpression(_shortVersion, indexer, false, _turnType)
            };
        }

        protected abstract ushort GetIndexer(ushort cubeSize);
    }
}

using RubiksCube.Core.Enums;

namespace RubiksCube.Expressions
{
    internal class KnowExpression
    {
        public KnowExpression(string expression, ushort indexer, bool isReverse, TurnType turnType)
        {
            Expression = expression;
            Indexer = indexer;
            IsReverse = isReverse;
            TurnType = turnType;
        }

        public string Expression { get; }
        public bool IsReverse { get; }
        public TurnType TurnType { get; }
        public ushort Indexer { get; }
    }
}

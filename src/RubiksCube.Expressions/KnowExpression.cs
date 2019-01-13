using RubiksCube.Core;

namespace RubiksCube.Expressions
{
    internal class KnowExpression
    {
        public string Expression { get; }
        public bool IsReverse { get; }
        public TurnType TurnType { get; }
        public ushort Indexer { get; }

        public KnowExpression(string expression, ushort indexer, bool isReverse, TurnType turnType)
        {
            Expression = expression;
            Indexer = indexer;
            IsReverse = isReverse;
            TurnType = turnType;
        }
    }
}

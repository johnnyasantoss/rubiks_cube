using System.Collections.Generic;
using RubiksCube.Core;

namespace RubiksCube.Expressions
{
    public class ParseResult
    {
        public ParseResult(string errorMessage)
        {
            Success = false;
            ErrorMessage = errorMessage;
        }

        public ParseResult(IEnumerable<RubiksCubeMovement> movements)
        {
            Success = true;
            Movements = movements;
        }

        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public IEnumerable<RubiksCubeMovement> Movements { get; set; }
    }
}

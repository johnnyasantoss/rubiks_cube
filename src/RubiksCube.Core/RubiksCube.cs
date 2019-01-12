using System;

namespace RubiksCube.Core
{
    public class RubiksCube
    {
        public const ushort FacesSize = 6;

        public const int BackFaceIndex = 5;
        public const int BottomFaceIndex = 4;
        public const int FrontFaceIndex = 2;
        public const int LeftFaceIndex = 1;
        public const int TopFaceIndex = 0;
        public const int RightFaceIndex = 3;

        private readonly CubeFace[] _faces;

        public RubiksCube(CubeFace[] faces)
        {
            if (faces == null)
                throw new ArgumentNullException(nameof(faces));

            if (faces.Length != FacesSize)
                throw new InvalidOperationException("Invalid amount of faces for a cube.");

            _faces = faces;
            SanityCheck();
        }

        public RubiksCube(ushort size)
            : this(
                new[]
                {
                    new CubeFace(CubeColor.Orange, size)
                    , new CubeFace(CubeColor.Green, size)
                    , new CubeFace(CubeColor.White, size)
                    , new CubeFace(CubeColor.Blue, size)
                    , new CubeFace(CubeColor.Red, size)
                    , new CubeFace(CubeColor.Yellow, size)
                }
            )
        {
        }

        public CubeFace[] Faces => _faces;

        public CubeFace BackFace => _faces[BackFaceIndex];

        public CubeFace BottomFace => _faces[BottomFaceIndex];

        public CubeFace FrontFace => _faces[FrontFaceIndex];

        public CubeFace LeftFace => _faces[LeftFaceIndex];

        public CubeFace TopFace => _faces[TopFaceIndex];

        public CubeFace RightFace => _faces[RightFaceIndex];

        private void SanityCheck()
        {
            var faceSize = -1;

            for (var i = 0; i < FacesSize; i++)
            {
                var currentFace = _faces[i];

                if (faceSize == -1)
                    faceSize = currentFace.Size;

                if (faceSize != currentFace.Size)
                    throw new Exception("One face doesn't have the same size as the others");

                for (var j = 0; j < currentFace.Size; j++)
                for (var k = 0; k < currentFace.Size; k++)
                {
                    var currentSlot = currentFace.Slots[j, k];

                    if (currentSlot == null)
                        throw new Exception($"Invalid slot @ {currentFace.CenterColor}: {j}, {k}");
                }
            }
        }

        public void Turn(SimpleDirectionTurn turn)
        {
            switch (turn)
            {
                case SimpleDirectionTurn.Front:
                    throw new NotImplementedException();
                case SimpleDirectionTurn.FrontReverse:
                    throw new NotImplementedException();
                case SimpleDirectionTurn.Left:
                    throw new NotImplementedException();
                case SimpleDirectionTurn.LeftReverse:
                    throw new NotImplementedException();
                case SimpleDirectionTurn.Right:
                    throw new NotImplementedException();
                case SimpleDirectionTurn.RightReverse:
                    throw new NotImplementedException();
                case SimpleDirectionTurn.Top:
                    Turn(TurnType.Line, 0, TurnDirection.Normal);
                    break;
                case SimpleDirectionTurn.TopReverse:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException(nameof(turn), turn, null);
            }
        }

        public void Turn(RubiksCubeMovement movement)
        {
            Turn(movement.TurnType, movement.Indexer, movement.Direction);
        }

        public void Turn(TurnType type, ushort indexer, TurnDirection direction)
        {
            switch (type)
            {
                case TurnType.Line:
                    TurnLine(ref indexer, ref direction);
                    break;
                case TurnType.Column:
                    throw new NotImplementedException();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void TurnLine(
            ref ushort indexer
            , ref TurnDirection direction
        )
        {
            switch (direction)
            {
                case TurnDirection.Normal:
                    TurnLineNormal(ref indexer);
                    break;
                case TurnDirection.Reverse:
                    throw new NotImplementedException();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        private void TurnLineNormal(ref ushort indexer)
        {
            var frontFace = FrontFace;

            var faces = new[]
            {
                frontFace, LeftFace, BackFace, RightFace, frontFace
            };

            var temp = new CubeSlot[frontFace.Size];

            foreach (var currentFace in faces)
            {
                for (var i = 0; i < currentFace.Size; i++)
                {
                    //swap
                    var currentTemp = currentFace.Slots[indexer, i];
                    currentFace.Slots[indexer, i] = temp[i];
                    temp[i] = currentTemp;
                }
            }
        }
    }
}

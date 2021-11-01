using System;
using RubiksCube.Core.Enums;

namespace RubiksCube.Core
{
    public class RubiksCube
    {
        public const ushort FacesAmount = 6;

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

            if (faces.Length != FacesAmount)
                throw new InvalidOperationException("Invalid amount of faces for a cube.");

            _faces = faces;

            Size = faces[0]
                .Size;

            SanityCheck();
        }

        public RubiksCube(ushort size)
            : this(
                new[]
                {
                    new CubeFace(CubeColor.Orange, size), new CubeFace(CubeColor.Green, size),
                    new CubeFace(CubeColor.White, size), new CubeFace(CubeColor.Blue, size),
                    new CubeFace(CubeColor.Red, size), new CubeFace(CubeColor.Yellow, size)
                }
            )
        {
            Size = size;
        }

        public ushort Size { get; }

        public ushort Middle => (ushort) (Math.Round(Size / 2D) - 1D);

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

            for (var i = 0; i < FacesAmount; i++)
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
            var maxIndex = (ushort) (Size - 1);

            switch (turn)
            {
                case SimpleDirectionTurn.Front:
                    Turn(TurnType.Line, maxIndex, TurnDirection.Normal);
                    break;
                case SimpleDirectionTurn.FrontReverse:
                    Turn(TurnType.Line, maxIndex, TurnDirection.Reverse);
                    break;
                case SimpleDirectionTurn.Left:
                    Turn(TurnType.Column, 0, TurnDirection.Normal);
                    break;
                case SimpleDirectionTurn.LeftReverse:
                    Turn(TurnType.Column, 0, TurnDirection.Reverse);
                    break;
                case SimpleDirectionTurn.Right:
                    Turn(TurnType.Column, maxIndex, TurnDirection.Normal);
                    break;
                case SimpleDirectionTurn.RightReverse:
                    Turn(TurnType.Column, maxIndex, TurnDirection.Reverse);
                    break;
                case SimpleDirectionTurn.Top:
                    Turn(TurnType.Line, 0, TurnDirection.Normal);
                    break;
                case SimpleDirectionTurn.TopReverse:
                    Turn(TurnType.Line, 0, TurnDirection.Reverse);
                    break;
                case SimpleDirectionTurn.Bottom:
                    Turn(TurnType.Line, maxIndex, TurnDirection.Normal);
                    break;
                case SimpleDirectionTurn.BottomReverse:
                    Turn(TurnType.Line, maxIndex, TurnDirection.Reverse);
                    break;
                case SimpleDirectionTurn.Back:
                    Turn(TurnType.Line, 0, TurnDirection.Normal);
                    break;
                case SimpleDirectionTurn.BackReverse:
                    Turn(TurnType.Line, 0, TurnDirection.Reverse);
                    break;
                case SimpleDirectionTurn.Center:
                    Turn(TurnType.Line, Middle, TurnDirection.Normal);
                    break;
                case SimpleDirectionTurn.CenterReverse:
                    Turn(TurnType.Line, Middle, TurnDirection.Reverse);
                    break;
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
                    TurnColumn(ref indexer, ref direction);
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
                    TurnLineReversed(ref indexer);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        private void TurnColumn(
            ref ushort indexer
            , ref TurnDirection direction
        )
        {
            switch (direction)
            {
                case TurnDirection.Normal:
                    TurnColumnNormal(ref indexer);
                    break;
                case TurnDirection.Reverse:
                    TurnColumnReversed(ref indexer);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        private void TurnColumnNormal(ref ushort indexer)
        {
            var faces = new[]
            {
                TopFace, FrontFace, BottomFace, BackFace, TopFace
            };

            var temp = new CubeSlot[Size];

            foreach (var currentFace in faces)
            {
                for (var i = 0; i < currentFace.Size; i++)
                {
                    //swap
                    var currentTemp = currentFace.Slots[i, indexer];
                    currentFace.Slots[i, indexer] = temp[i];
                    temp[i] = currentTemp;
                }
            }
        }

        private void TurnColumnReversed(ref ushort indexer)
        {
            var faces = new[]
            {
                TopFace, BackFace, BottomFace, FrontFace, TopFace
            };

            var temp = new CubeSlot[Size];

            foreach (var currentFace in faces)
            {
                for (var i = 0; i < currentFace.Size; i++)
                {
                    //swap
                    var currentTemp = currentFace.Slots[i, indexer];
                    currentFace.Slots[i, indexer] = temp[i];
                    temp[i] = currentTemp;
                }
            }
        }

        private void TurnLineNormal(ref ushort indexer)
        {
            var faces = new[]
            {
                FrontFace, LeftFace, BackFace, RightFace, FrontFace
            };

            var temp = new CubeSlot[Size];

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

        private void TurnLineReversed(ref ushort indexer)
        {
            var faces = new[]
            {
                FrontFace, RightFace, BackFace, LeftFace, FrontFace
            };

            var temp = new CubeSlot[Size];

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

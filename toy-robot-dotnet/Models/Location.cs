using System;

namespace toy_robot_dotnet.Models
{
    public class Location
    {
        public Location(int x, int y)
        {
            X = x >= 0 ? x : 0;
            Y = y >= 0 ? y : 0;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public bool ValidatePosition()
        {
            return X >= 0 && Y >= 0;
        }

        public void Move(FaceEnum face, int step = 1)
        {
            switch (face)
            {
                case FaceEnum.EAST:
                    X += step;
                    break;
                case FaceEnum.WEST:
                    X -= step;
                    break;
                case FaceEnum.NORTH:
                    Y += step;
                    break;
                case FaceEnum.SOUTH:
                    Y -= step;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
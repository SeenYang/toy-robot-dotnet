using System;

namespace toy_robot_dotnet.Models
{
    public class Robot
    {
        public Robot(Location location = null, FaceEnum face = FaceEnum.NORTH)
        {
            Location = location ?? new Location(0, 0);
            Face = face;
        }

        public Location Location { get; set; }

        public FaceEnum Face { get; set; }

        public string Report()
        {
            return $"{Location.X}, {Location.Y}, {Enum.GetName(typeof(FaceEnum), Face)?.ToUpper()}";
        }

        public void Move(Location location)
        {
            if (location.ValidatePosition()) Location = location;
        }

        public void TurnLeft()
        {
            Face = Face switch
            {
                FaceEnum.EAST => FaceEnum.NORTH,
                FaceEnum.WEST => FaceEnum.SOUTH,
                FaceEnum.NORTH => FaceEnum.WEST,
                FaceEnum.SOUTH => FaceEnum.EAST,
                _ => throw new ArgumentOutOfRangeException()
            };
        }


        public void TurnRight()
        {
            Face = Face switch
            {
                FaceEnum.EAST => FaceEnum.SOUTH,
                FaceEnum.WEST => FaceEnum.NORTH,
                FaceEnum.NORTH => FaceEnum.EAST,
                FaceEnum.SOUTH => FaceEnum.WEST,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
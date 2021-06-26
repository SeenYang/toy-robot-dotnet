using System.Collections.Generic;

namespace toy_robot_dotnet.Models
{
    public class Board
    {
        public Board(int width = 5, int height = 5)
        {
            Width = width <= 0 ? 5 : width;
            Height = height <= 0 ? 5 : height;
        }

        public int Width { get; set; }
        public int Height { get; set; }

        public bool IsValidLocation(Location location)
        {
            return location.ValidatePosition() && location.X < Width - 1 && location.Y < Height - 1;
        }

        public IEnumerable<string> Map(Robot robot)
        {
            return new List<string>();
        }
    }
}
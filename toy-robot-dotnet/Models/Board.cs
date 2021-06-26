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
            return location.ValidatePosition() && location.X < Width && location.Y < Height;
        }

        public string[,] Map(Robot robot)
        {
            var x = robot.Location.X;
            var y = robot.Location.Y;
            
            var result = new string[Height, Width];
            // string[row, column]
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    if (j == x && i == y)
                    {
                        result[i, j] = "R";
                    }
                    else
                    {
                        result[i, j] = "_";
                    }
                    
                }
            }
            
            
            return result;
        }
    }
}
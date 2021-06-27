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
            return location.ValidatePosition() && 
                   location.X < Width && 
                   location.Y < Height;
        }

        /// <summary>
        /// This is the method to print the board and indicate where the robot is.
        ///
        /// It's returning 2-D string array, string[Height, Width]
        /// 
        /// Ideally, double iteration should be replaced by more fancy way.
        /// However, for multidimensional array in C#, no matter what approach,
        /// double iterator is playing the role behind it.
        ///
        /// Potential solution could be used similar to hashmap, or other to speed up
        /// search or sorting. 
        /// </summary>
        /// <param name="robot">the `robot` with location specified.</param>
        /// <returns>
        /// Return will be an 2-d array of string, string[height, width].
        /// Printing out the map by console should reverse the order of height, which is the index[0]
        /// </returns>
        public string[,] PrintMap(Robot robot)
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
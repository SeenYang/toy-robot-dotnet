using toy_robot_dotnet.Models;
using Xunit;

namespace toy_robot_dotnet.tests
{
    public class BoardTests
    {
        [Fact(DisplayName = "Valid input, should initiate board properly")]
        public void Initiate_Test1()
        {
            // Arrange
            const int height = 6;
            const int width = 6;

            // Action
            var board = new Board(width, height);

            // Assert
            Assert.Equal(6, board.Width);
            Assert.Equal(6, board.Height);
        }

        [Fact(DisplayName = "Invalid input, should initiate board with default value.")]
        public void Initiate_Test2()
        {
            // Arrange
            const int height = -1;
            const int width = -1;

            // Action
            var board = new Board(width, height);

            // Assert
            Assert.Equal(5, board.Width);
            Assert.Equal(5, board.Height);
        }

        [Fact(DisplayName = "Valid input, should return true.")]
        public void Validation_Test1()
        {
            // Arrange
            const int height = 5;
            const int width = 5;
            var location = new Location(3, 4);

            // Action
            var board = new Board();
            var result = board.IsValidLocation(location);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Valid input, should return false.")]
        public void Validation_Test2()
        {
            // Arrange
            const int height = 5;
            const int width = 5;
            var location = new Location(0, 0);

            // Action
            var board = new Board();
            var result = board.IsValidLocation(location);

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Invalid input, should return false.")]
        public void Validation_Test3()
        {
            // Arrange
            const int height = 5;
            const int width = 5;
            // for width 5, max index should be 4. same as height.
            var location = new Location(5, 5);
            var board = new Board();

            // Action
            var result = board.IsValidLocation(location);

            // Assert
            Assert.False(result);
        }


        [Fact(DisplayName = "Map(), valid input, should return map.")]
        public void Map_Test1()
        {
            // Arrange
            var board = new Board();
            var robot = new Robot(new Location(2, 2), FaceEnum.WEST);

            // Action
            var result = board.Map(robot);

            // Assert
            Assert.NotEmpty(result);
        }
    }
}
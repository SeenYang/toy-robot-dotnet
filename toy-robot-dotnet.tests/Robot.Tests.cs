using toy_robot_dotnet.Models;
using Xunit;

namespace toy_robot_dotnet.tests
{
    public class RobotTests
    {
        [Fact(DisplayName = "Valid input, should initiate robot position properly")]
        public void Initiate_Test1()
        {
            // Arrange
            // Assert the initiate position is valid within the board.
            var location = new Location(3, 2);
            const FaceEnum face = FaceEnum.SOUTH;

            // Action
            var robot = new Robot(location, face);

            // Assert
            Assert.Equal(location.X, robot.Location.X);
            Assert.Equal(location.Y, robot.Location.Y);
            Assert.Equal(face, robot.Face);
        }

        [Fact(DisplayName = "Empty input, should initiate robot position with default value.")]
        public void Initiate_Test2()
        {
            // Arrange

            // Action
            var robot = new Robot();

            // Assert
            Assert.Equal(0, robot.Location.X);
            Assert.Equal(0, robot.Location.Y);
            Assert.Equal(FaceEnum.NORTH, robot.Face);
        }

        [Fact(DisplayName = "Invalid input, should initiate robot position with default value.")]
        public void Initiate_Test3()
        {
            // Arrange
            var invalidLocation = new Location(-1, -2);
            const FaceEnum face = FaceEnum.SOUTH;

            // Action
            var robot = new Robot(invalidLocation, face);

            // Assert
            Assert.Equal(0, robot.Location.X);
            Assert.Equal(0, robot.Location.Y);
            Assert.Equal(FaceEnum.SOUTH, robot.Face);
        }

        [Fact(DisplayName = "Move to location, should be at new positions.")]
        public void Move_Test1()
        {
            // Arrange
            var robot = new Robot(new Location(3, 2), FaceEnum.EAST);
            var destination = new Location(4, 2);

            // Action
            robot.Move(destination);

            // Assert
            Assert.Equal(destination.X, robot.Location.X);
            Assert.Equal(destination.Y, robot.Location.Y);
        }

        [Fact(DisplayName = "Valid Robot initiate, should report proper location")]
        public void Report_Test1()
        {
            // Arrange
            // Only test about 0,0 due to board edge test should be handled by board.
            var southFacingRobot = new Robot(new Location(3, 2), FaceEnum.SOUTH);

            // Action
            var report = southFacingRobot.Report();

            // Assert
            Assert.Equal("3, 2, SOUTH", report);
        }
    }
}
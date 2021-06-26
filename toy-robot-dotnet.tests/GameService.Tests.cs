using toy_robot_dotnet.Models;
using toy_robot_dotnet.Services;
using Xunit;

namespace toy_robot_dotnet.tests
{
    public class GameServiceTests
    {
        [Fact(DisplayName = "Initiate(), Valid input, initiate board and robot.")]
        public void Initiate_Test1()
        {
            const string command = "PLACE 3,2,SOUTH";
            var service = new GameService();

            service.Initiate(command);

            Assert.NotNull(service.Board);
            Assert.NotNull(service.Robot);
            Assert.Equal(3, service.Robot.Location.X);
            Assert.Equal(2, service.Robot.Location.Y);
            Assert.Equal(FaceEnum.SOUTH, service.Robot.Face);
        }

        [Fact(DisplayName = "Initiate(), Invalid input, not initiate board and robot.")]
        public void Initiate_Test2()
        {
            const string command = "PLACE 6,3,SOUTH";
            var service = new GameService();

            service.Initiate(command);

            Assert.Null(service.Board);
            Assert.Null(service.Robot);
        }

        [Fact(DisplayName = "PerformAction(), Move robot, valid input, robot should be in new position.")]
        public void PerformAction_Test1()
        {
            // Arrange 
            var service = new GameService
            {
                Robot = new Robot(new Location(3, 2), FaceEnum.WEST),
                Board = new Board() // 5 x 5 board.
            };
            var command = "MOVE";

            // Action
            service.PerformAction(command);

            // Action
            Assert.Equal(2, service.Robot.Location.X);
            Assert.Equal(2, service.Robot.Location.Y);
        }

        [Fact(DisplayName = "PerformAction(), Move robot, invalid input, robot should be in old position.")]
        public void PerformAction_Test2()
        {
            // Arrange 
            var service = new GameService
            {
                Robot = new Robot(new Location(4, 4), FaceEnum.NORTH), // robot stand at the edge.
                Board = new Board() // 5 x 5 board.
            };
            var command = "MOVE";

            // Action
            service.PerformAction(command);

            // Action
            Assert.Equal(new Location(4, 4), service.Robot.Location);
        }
    }
}
using toy_robot_dotnet.Services;

namespace toy_robot_dotnet
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var game = new GameService();
            game.Start();
        }
    }
}
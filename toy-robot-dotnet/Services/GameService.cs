using System;
using System.Text.RegularExpressions;
using toy_robot_dotnet.Models;

namespace toy_robot_dotnet.Services
{
    public class GameService
    {
        private const string PlaceRegex = "(PLACE) ([0-9]+),([0-9]+),(NORTH|EAST|SOUTH|WEST)";
        private const string ActionRegex = "(MOVE|REPORT|MAP)";
        private const string RotateRegex = "(LEFT|RIGHT)";

        public Board Board;
        public Robot Robot;

        public void Start()
        {
            Console.WriteLine("Welcome to Robot game.");
            Console.WriteLine("Please enter one of the following commands to start the game:");
            Console.WriteLine("To `Place robot`, please enter: PLACE X,Y,FACE");
            Console.WriteLine("To `move robot`, please enter: MOVE");
            Console.WriteLine("To `turn robot around`, please enter: LEFT or RIGHT");
            Console.WriteLine("To `report robot location`, please enter: REPORT");
            Console.WriteLine("To `show robot on map`, please enter: MAP");
            Console.WriteLine("To `exit the game`, please enter: EXIT");
            while (true) // Loop indefinitely
            {
                var line = Console.ReadLine()?.ToUpper(); // Get string from user
                if (line == "EXIT") // Check string
                    break;

                try
                {
                    CommandHandler(line);
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter valid command and try again.");
                }
            }
        }

        private void CommandHandler(string command)
        {
            switch (true)
            {
                // Only initiate game once
                case { } when Regex.IsMatch(command, PlaceRegex) && Robot == null && Board == null:
                    Initiate(command);
                    break;
                case { } when Regex.IsMatch(command, ActionRegex) && Robot != null && Board != null:
                    PerformAction(command);
                    break;
                case { } when Regex.IsMatch(command, RotateRegex) && Robot != null && Board != null:
                    PerformRotate(command);
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        ///     Method for initiate the game.
        ///     Only will be trigger while `Board` and `Robot` are not initiated and command `Place` is receive with valid
        ///     position.
        /// </summary>
        /// <param name="command"></param>
        public void Initiate(string command)
        {
            var regex = new Regex(PlaceRegex);
            var m = regex.Match(command);
            var x = int.Parse(m.Groups[2].ToString());
            var y = int.Parse(m.Groups[3].ToString());
            Enum.TryParse(m.Groups[4].ToString(), out FaceEnum face);

            var board = new Board();
            var initiateLocation = new Location(x, y);
            if (!board.IsValidLocation(initiateLocation))
                return;

            // Initiate the robot and board. game start.
            Board = board;
            Robot = new Robot(new Location(x, y), face);
            // Print initiate map.
            PerformAction("MAP");
        }

        /// <summary>
        ///     MOVE REPORT MAP
        /// </summary>
        /// <param name="command"></param>
        public void PerformAction(string command)
        {
            var regex = new Regex(ActionRegex);
            var m = regex.Match(command);
            Enum.TryParse(m.Groups[1].ToString(), out GameAction actionEnum);

            switch (actionEnum)
            {
                case GameAction.MOVE:
                    var location = new Location(Robot.Location.X, Robot.Location.Y);
                    location.Move(Robot.Face);
                    if (Board.IsValidLocation(location))
                        Robot.Move(location);
                    Console.WriteLine($"Robot current at {Robot.Report()}");
                    break;
                case GameAction.REPORT:
                    Console.WriteLine(Robot.Report());
                    break;
                case GameAction.MAP:
                    var map = Board.PrintMap(Robot);
                    for (var i = map.GetLength(0) - 1; i >= 0; i--)
                    {
                        for (var k = 0; k < map.GetLength(1); k++)
                        {
                            Console.Write(map[i, k] + "\t");
                        }

                        Console.WriteLine();
                    }

                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        ///     LEFT or RIGHT
        /// </summary>
        /// <param name="command"></param>
        public void PerformRotate(string command)
        {
            var regex = new Regex(RotateRegex);
            var m = regex.Match(command);
            Enum.TryParse(m.Groups[1].ToString(), out GameAction actionEnum);

            switch (actionEnum)
            {
                case GameAction.LEFT:
                    Robot.TurnLeft();
                    Console.WriteLine($"Robot current at {Robot.Report()}");
                    break;
                case GameAction.RIGHT:
                    Robot.TurnRight();
                    Console.WriteLine($"Robot current at {Robot.Report()}");
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
        }
    }
}

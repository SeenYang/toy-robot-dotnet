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
        }

        public void CommandHandler(string command)
        {
            switch (true)
            {
                // Only initiate game once
                case { } when Regex.IsMatch(command, PlaceRegex) && Robot == null && Board == null:
                    Initiate(command);
                    break;
                case { } when Regex.IsMatch(command, ActionRegex):
                    PerformAction(command);
                    break;
                case { } when Regex.IsMatch(command, RotateRegex):
                    PerformRotate(command);
                    break;
                default:
                    Console.WriteLine("Invalid command.");
                    break;
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
                case GameAction.Move:
                    var location = Robot.Location;
                    location.Move(Robot.Face);
                    if (Board.IsValidLocation(location))
                        Robot.Move(location);
                    break;
                case GameAction.Report:
                    Console.WriteLine(Robot.Report());
                    break;
                case GameAction.Map:
                    var map = Board.Map(Robot);
                    foreach (var line in map) Console.WriteLine(line);

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
            var regex = new Regex(ActionRegex);
            var m = regex.Match(command);
            Enum.TryParse(m.Groups[1].ToString(), out GameAction actionEnum);

            switch (actionEnum)
            {
                case GameAction.TurnLeft:
                    Robot.TurnLeft();
                    break;
                case GameAction.TurnRight:
                    Robot.TurnRight();
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
        }
    }
}
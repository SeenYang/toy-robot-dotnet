Toy Robot Simulator
========================

Description
-----------
> Detail constrains please refer to the [PROBLEM.md](./PROBLEM.md)
- The application is a simulation of a toy robot moving on a square tabletop,
  of dimensions 5 units x 5 units.
- There are no other obstructions on the table surface.
- The robot is free to roam around the surface of the table, but must be
  prevented from falling to destruction. Any movement that would result in the
  robot falling from the table must be prevented, however further valid
  movement commands must still be allowed.

Create an application that can read in commands of the following (textual) form:

    PLACE X,Y,F
    MOVE
    LEFT
    RIGHT
    REPORT

- PLACE will put the toy robot on the table in position X,Y and facing NORTH,
  SOUTH, EAST or WEST.
- The origin (0,0) can be considered to be the SOUTH WEST most corner. 
  > If robot locates at (1,2) in a 4x4 board.
  > 
  > | 3 | _ | _ | _ | _ |
  > |---|---|---|---|---|
  > | 2 | _ | R | _ | _ |
  > | 1 | _ | _ | _ | _ |
  > | 0 | _ | _ | _ | _ |
  > | * | 0 | 1 | 2 | 3 |

- The first valid command to the robot is a PLACE command, after that, any
  sequence of commands may be issued, in any order, including another PLACE
  command. The application should discard all commands in the sequence until
  a valid PLACE command has been executed.
- MOVE will move the toy robot one unit forward in the direction it is
  currently facing.
  > robot at (1,2) North. MOVE, will be (1,3) North
- LEFT and RIGHT will rotate the robot 90 degrees in the specified direction
  without changing the position of the robot.
- REPORT will announce the X,Y and F of the robot. This can be in any form,
  but standard output is sufficient.
  
Edge Scenarios
-------------------
1. A robot that is not on the table can choose the ignore the MOVE, LEFT, RIGHT and REPORT commands.
    > Game will be initiated only after valid `PLACE X,Y,F` command.
2. Input can be from a file, or from standard input, as the developer chooses.
   > For this console app, console `std input` is the way to take user's input.
3. Provide test data to exercise the application.
   > Please refer to attachment at the end ot this file.

Solution Introduction
---------------------
This is a console app written by .net core 3.1.
.net core is a framework that allows developers to use C# to develop cross-platform application 
and easily deploy to all main stream OS systems, including Linux, Windows, MacOS. In the meantime,
.net core application can be containerised to perform seamless deployment including K8S, Docker container or ECS.

## application design
This is a game contain two parts in design: the Game, and the contents.
### Contents
The game contains two contents, the `board` and the `robot`.
* `robot` come with it's functionalities, including `move`, `turn`, `report`, etc.
* `board` come with it's functionalities, like `validate` and `drawing the board`.

### Game
Game itself should only perform I/O, like `taking input from user` and `display the game`.

## Solution breakdown
1. `Program.cs` is the entrypoint of the console app.
2. `Models/` contains `Board.cs` and `Robot.cs`
3. `Services/` contains `GameService.cs` with all the game logic.

## Build and Run
1. install .net core 3.1 sdk on your machine. 
2. run
    1. run via `vs code/rider/visual studio` debug/run mode.
    2. run compiled file.
    3. run on `bash/terminal`.
    4. run via `docker` (docker file attached).

Publish commands:
```bash
// Here is showing steps on linux/MacOS.
dotnet restore
dotnet build
dotnet publish -c Release  
dotnet ./toy-robot-dotnet/bin/Release/netcoreapp3.1/publish/toy-robot-dotnet.dll
```

Bash commands:
```bash
  dotnet restore
  dotnet build
  dotnet run --project=toy-robot-dotnet
```

Docker commands:
```bash
docker build -t counter-image -f Dockerfile .
docker create --name core-counter counter-image

## Start
docker start core-counter

## Stop
docker stop core-counter
```

Sample Input
------------

```bash
## Valid case
PLACE 2,3 SOUTH
MOVE
MOVE
LEFT
MOVE
MAP

EXIT
```

```bash
MOVE
LEFT

## Only the first valid `PLACE` will initiate the game.
PLACE 2,3 SOUTH
MOVE
MOVE
MAP

EXIT
```
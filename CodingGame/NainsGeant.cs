using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;


class Bender
{

  enum Direction
  {
    SOUTH = 0,
    EAST,
    NORTH,
    WEST,
  }

  readonly List<Direction> _moveSequence = new List<Direction> { Direction.SOUTH, Direction.EAST, Direction.NORTH, Direction.WEST };
  readonly List<Direction> _moveSequenceInverted = new List<Direction> { Direction.WEST, Direction.NORTH, Direction.EAST, Direction.SOUTH };

  Dictionary<Direction, Func<Tuple<int, int>, Tuple<int, int>>> _moves = new Dictionary<Direction, Func<Tuple<int, int>, Tuple<int, int>>>
    {
      {Direction.SOUTH, point => Tuple.Create(point.Item1, point.Item2 + 1)},
      {Direction.NORTH, point => Tuple.Create(point.Item1, point.Item2 - 1)},
      {Direction.EAST, point => Tuple.Create(point.Item1 + 1, point.Item2)},
      {Direction.WEST, point => Tuple.Create(point.Item1 - 1, point.Item2)}
    };

  private Direction _direction;
  private Tuple<int, int> _position;

  private bool _isInverted;
  private bool _beerMode;

  private readonly char[][] _map;
  private readonly List<Tuple<int, int>> _gates;

  private readonly Action<string> _writeLine = s => Console.WriteLine(s);

  private void WriteLine(string s)
  {
    _writeLine(s);
  }

  public Bender(Tuple<int, int> position, char[][] map, List<Tuple<int, int>> gates, Action<string> writeLine)
    : this(map, position, gates)
  {
    _writeLine = writeLine;
  }

  public Bender(char[][] map, Tuple<int, int> position, List<Tuple<int, int>> gates)
  {
    _map = map;
    _position = position;
    _gates = gates;
    _direction = Direction.SOUTH;
    _isInverted = false;
    _beerMode = false;
  }

  public void Run()
  {
    while (true)
    {
      var currentPosition = _map[_position.Item1][_position.Item2];

      if (currentPosition == '$') { break; }
      if (currentPosition == 'B') { _beerMode = !_beerMode; }
      if (currentPosition == 'I') { _isInverted = !_isInverted; }
      if (currentPosition == 'T') { _position = _gates.First(x => x.Item1 != _position.Item1 && x.Item2 != _position.Item2); }
      if (currentPosition == 'S') { _direction = Direction.SOUTH; }
      if (currentPosition == 'E') { _direction = Direction.EAST; }
      if (currentPosition == 'N') { _direction = Direction.NORTH; }
      if (currentPosition == 'W') { _direction = Direction.WEST; }

      if (currentPosition == 'X' && _beerMode) { _map[_position.Item1][_position.Item2] = ' '; }

      var nextPotentialMove = _moves[_direction](_position);
      var nextPotentialPosition = _map[nextPotentialMove.Item1][nextPotentialMove.Item2];

      // MOVE
      if (nextPotentialPosition == 'X' || nextPotentialPosition == '#')
      {
        var sequence = !_isInverted ? _moveSequence : _moveSequenceInverted;
        var nextMoveFound = false;
        foreach (var direction in sequence)
        {
          var tryNextMove = _moves[direction](_position);
          var tryNextPosition = _map[tryNextMove.Item2][tryNextMove.Item1];
          if (tryNextPosition == '#' || (tryNextPosition == 'X' && !_beerMode))
          {
            continue;
          }

          _direction = direction;
          _position = tryNextMove;
          nextMoveFound = true;

          break;
        }

        WriteLine(nextMoveFound ? _direction.ToString() : "LOOP");
      }
      else
      {
        _position = nextPotentialMove;
        WriteLine(_direction.ToString());
      }
    }

  }


}

class BenderSolution
{
  static void Main(string[] args)
  {
    string[] inputs = ReadLine().Split(' ');
    int L = int.Parse(inputs[0]);
    int C = int.Parse(inputs[1]);

    var map = new char[L][];
    var gates = new List<Tuple<int, int>>();
    Tuple<int, int> initialPosition = null;

    for (int i = 0; i < L; i++)
    {
      var lineMap = ReadLine().ToCharArray();

      map[i] = lineMap;
      for (int j = 0; j < C; j++)
      {
        var c = lineMap[j];
        if (c.Equals('T'))
        {
          gates.Add(Tuple.Create(j, i));
        }
        if (c.Equals('@'))
        {
          initialPosition = Tuple.Create(j, i);
        }
      }
    }

    var bender = new Bender(map, initialPosition, gates);
    bender.Run();
  }

  private static string ReadLine()
  {
    return Console.ReadLine();
  }
}
/*
8 8
########
#     $#
#      #
#      #
#  @   #
#      #
#      #
########

  */

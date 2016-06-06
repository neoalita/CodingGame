using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;


class BenderSolution
{

  class Bender
  {
    private readonly int _l;
    private readonly int _c;

    public enum Direction
    {
      SOUTH = 0,
      EAST,
      NORTH,
      WEST,
    }

    List<Direction> _moveSequence = new List<Direction> { Direction.SOUTH, Direction.EAST, Direction.NORTH, Direction.WEST };
    List<Direction> _moveSequenceInverted = new List<Direction> { Direction.WEST, Direction.NORTH, Direction.EAST, Direction.SOUTH };

    Dictionary<Direction, Func<Tuple<int, int>, Tuple<int, int>>> _moves = new Dictionary<Direction, Func<Tuple<int, int>, Tuple<int, int>>>
    {
      {Direction.SOUTH, point => Tuple.Create(point.Item1, point.Item2 + 1)},
      {Direction.NORTH, point => Tuple.Create(point.Item1, point.Item2 - 1)},
      {Direction.EAST, point => Tuple.Create(point.Item1 - 1, point.Item2)},
      {Direction.WEST, point => Tuple.Create(point.Item1 + 1, point.Item2)}
    };

    private Direction _direction;
    private Tuple<int, int> _position;

    private bool _isInverted;
    private bool _beerMode;

    private readonly char[][] _map;
    private readonly List<Tuple<int, int>> _gates;


    public Bender(char[][] map, Tuple<int, int> position, List<Tuple<int, int>> gates, int L, int C)
    {
      _l = L;
      _c = C;
      _map = map;
      _position = position;
      _gates = gates;
      _direction = Direction.SOUTH;
      _isInverted = false;
      _beerMode = false;
    }

    public void Run()
    {
      var moveList = new List<string>();
      while (true)
      {
        var currentPosition = _map[_position.Item1][_position.Item2];
        Console.Error.WriteLine("[MOVE] From : " + _position.Item1 + " " + _position.Item2 + " " + currentPosition + " " + _direction);

        if (currentPosition == '$')
        {
          Console.Error.WriteLine("[END :)]");
          return;
        }

        if (currentPosition == 'B')
        {
          Console.Error.WriteLine("[BEER MODE]"); _beerMode = !_beerMode;
        }

        if (currentPosition == 'I')
        {
          Console.Error.WriteLine("[INVERT]"); _isInverted = !_isInverted;
        }

        if (currentPosition == 'T')
        {
          Console.Error.WriteLine("[TELEPORT] From " + _position.Item1 + " " + _position.Item2);
          _position = _gates.Single(x => x.Item1 != _position.Item1 && x.Item2 != _position.Item2);
          Console.Error.WriteLine("[TELEPORT] To " + _position.Item1 + " " + _position.Item2);
          continue;
        }

        if (currentPosition == 'S')
        {
          Console.Error.WriteLine("[CHANGEDIR] From " + _direction);
          _direction = Direction.SOUTH;
          Console.Error.WriteLine("[CHANGEDIR] To " + _direction);
        }
        if (currentPosition == 'E')
        {
          Console.Error.WriteLine("[CHANGEDIR] From " + _direction);
          _direction = Direction.EAST; Console.Error.WriteLine("[CHANGEDIR] To " + _direction);
        }
        if (currentPosition == 'N')
        {
          Console.Error.WriteLine("[CHANGEDIR] From " + _direction);
          _direction = Direction.NORTH; Console.Error.WriteLine("[CHANGEDIR] To " + _direction);
        }
        if (currentPosition == 'W')
        {
          Console.Error.WriteLine("[CHANGEDIR] From " + _direction);
          _direction = Direction.WEST; Console.Error.WriteLine("[CHANGEDIR] To " + _direction);
        }


        if (currentPosition == 'X' && _beerMode)
        {
          Console.Error.WriteLine("[DESTROY] " + _position.Item1 + " " + _position.Item2);
          _map[_position.Item1][_position.Item2] = ' ';
          currentPosition = ' ';

        }

        // MOVE
        if (currentPosition == 'X' || currentPosition == '#')
        {
          Console.Error.WriteLine("[WALL FOUND] " + _position.Item1 + " " + _position.Item2 + " " + currentPosition);
          var sequence = !_isInverted ? _moveSequence : _moveSequenceInverted;
          var nextMoveFound = false;
          foreach (var direction in sequence)
          {
            var nextMove = _moves[_direction](_position);
            if (nextMove.Item1 >= _l || nextMove.Item2 >= _l)
            { continue; }

            var tryNextPosition = _map[nextMove.Item1][nextMove.Item2];
            Console.Error.WriteLine("[TRY] " + nextMove.Item1 + " " + nextMove.Item2 + " " + tryNextPosition + " " + direction);

            if (tryNextPosition == '#' || (tryNextPosition == 'X' && !_beerMode)) { continue; }

            Console.Error.WriteLine("[TRY SUCCESS] " + nextMove.Item1 + " " + nextMove.Item2 + " " + tryNextPosition + " " + direction);
            _direction = direction;
            _position = nextMove;
            nextMoveFound = true;

            break;
          }

          if (nextMoveFound)
          {
            moveList.Add(_direction.ToString());
            continue;
          }

          moveList.Add("LOOP");
          break;
        }

        _position = _moves[_direction](_position);
        Console.Error.WriteLine("[MOVE] To : " + _position.Item1 + " " + _position.Item2 + " " + _direction);
        moveList.Add(_direction.ToString());

      }

      foreach (var move in moveList)
      {
        Console.WriteLine(move);
      }
    }
  }



  static void Main(string[] args)
  {
    string[] inputs = Console.ReadLine().Split(' ');
    int L = int.Parse(inputs[0]);
    int C = int.Parse(inputs[1]);

    var map = new char[L][];
    var gates = new List<Tuple<int, int>>();
    Tuple<int, int> initialPosition = null;

    for (int i = 0; i < L; i++)
    {
      var lineMap = Console.ReadLine().ToCharArray();
      map[i] = lineMap;
      for (int j = 0; j < lineMap.Length; j++)
      {
        var c = lineMap[j];
        if (c.Equals('T'))
        {
          gates.Add(Tuple.Create(i, j));
        }
        if (c.Equals('@'))
        {
          initialPosition = Tuple.Create(i, j);
        }
      }
    }

    var bender = new Bender(map, initialPosition, gates, L, C);
    bender.Run();
  }


}
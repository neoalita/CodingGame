using System;
using System.Linq;

class Solution
{
  static void Main(string[] args)
  {
    var inputs = Console.ReadLine().Split(' ').Skip(1).Select(int.Parse);

    var max = 0;
    var loss = 0;
    foreach (var input in inputs)
    {
      if (input > max) { max = input; }
      if (input - max < loss) { loss = input - max; }
    }

    Console.WriteLine(loss);

  }
}
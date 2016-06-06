using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class MathSolution
{
  static void Main(string[] args)
  {
    int N = int.Parse(Console.ReadLine());
    int C = int.Parse(Console.ReadLine());
    var budgets = new SortedSet<int>();
    for (int i = 0; i < N; i++)
    {
      int B = int.Parse(Console.ReadLine());
      Console.Error.WriteLine("Add B : " + B);
      budgets.Add(B);
    }

    // Write an action using Console.WriteLine()
    // To debug: Console.Error.WriteLine("Debug messages...");
    if (budgets.Sum() < N)
    { Console.WriteLine("IMPOSSIBLE"); }

    foreach (var budget in budgets)
    {
      var currentBudget = Math.Min(budget, (int)Math.Floor((decimal)(C /N)));
      Console.WriteLine(currentBudget);
      C -= currentBudget;
      N--;
    }
    
  }
}
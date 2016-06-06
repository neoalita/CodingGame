using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace CodingGame.HARD.Complexity
{
  using System;

  /**
   * Auto-generated code below aims at helping you parse
   * the standard input according to the problem statement.
   **/
  class Solution
  {
    static void Main(string[] args)
    {
      var epsilonRandom = new Random();
      var complexities = new Dictionary<string, Func<int, double>>
      {
         {"O(1)", n => 10000+epsilonRandom.NextDouble()},
         {"O(log n)", n => Math.Log(n)},
         {"O(n)", n => n},
         {"O(n log n)", n => n*Math.Log(n)},
         {"O(n^2)", n => Math.Pow(n, 2)},
         {"O(n^2 log n)", n => Math.Pow(n, 2)*Math.Log(n)},
         {"O(n^3)", n => Math.Pow(n, 3)},
         {"O(2^n)", n => Math.Pow(2, n)},
      };

      var simulations = new Dictionary<string, List<double>>();
      var correlations = new Dictionary<string, double>();

      var graph = new List<double>();

      int N = int.Parse(Console.ReadLine());
      for (int i = 0; i < N; i++)
      {
        string[] inputs = Console.ReadLine().Split(' ');
        int x = int.Parse(inputs[0]);
        int y = int.Parse(inputs[1]);

        graph.Add(y);

        foreach (var complexity in complexities)
        {
          if (!simulations.ContainsKey(complexity.Key)) { simulations[complexity.Key] = new List<double>(); }
          simulations[complexity.Key].Add(complexity.Value(x));
        }
      }

      foreach (var complexity in complexities)
      {
        var correl = CalculateCorrelation(graph, simulations[complexity.Key]);
        Console.Error.WriteLine($"Corel ({complexity.Key}): {correl}");
        correlations[complexity.Key] = correl;
      }

      var value = correlations.First(x => x.Value == correlations.Where(y => !double.IsNaN(y.Value)).Max(y => y.Value)).Key;
      Console.WriteLine(value);
    }

    public static double CalculateCorrelation(List<double> source, List<double> simulation)
    {
      var xbar = source.Sum() / source.Count;
      var ybar = simulation.Sum() / source.Count;

      var cov = source.Select((t, i) => (t - xbar) * (simulation[i] - ybar)).Sum() / source.Count;

      var roX = Math.Sqrt(source.Average(x => Math.Pow(x - xbar, 2)));
      var roY = Math.Sqrt(simulation.Average(x => Math.Pow(x - ybar, 2)));

      Console.Error.WriteLine($"RoX : {roX}");
      Console.Error.WriteLine($"RoY : {roY}");

      return cov / (roX * roY);
    }

  }
}
using System;

namespace CodingGame.VERYHARD
{
  using System.Collections.Generic;
  using System.Linq;

  namespace CodingGame.VeryHard
  {

    public class ResistanceSolution
    {


      static void Main(string[] args)
      {

        IDictionary<string, int> dico = new Dictionary<string, int>();

        string L = Console.ReadLine();
        int N = int.Parse(Console.ReadLine());

        int maxSize = 0;

        for (int i = 0; i < N; i++)
        {
          var morse = ResistanceSolver.ConvertToMorse(Console.ReadLine());
          maxSize = Math.Max(maxSize, morse.Length);

          if (dico.ContainsKey(morse))
          {
            dico[morse]++;
          }
          else
          {
            dico[morse] = 1;
          }
        }

        var solver = new ResistanceSolver(dico, maxSize);
        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(solver.Compute(L));
      }

    }

    public class ResistanceSolver
    {
      public IDictionary<string, int> Dico { get; set; }
      public int MaxSize { get; set; }
      public IDictionary<string, int> cache = new Dictionary<string, int>();

      public long callCompute = 0;
      public long callCache = 0;
      public long callStartWith = 0;
      public long callRecursive = 0;

      private static readonly IDictionary<char, string> _morse = new Dictionary<char, string>()
          {
              {'A',".-"}, {'B',"-..."}, {'C',"-.-."}, {'D',"-.."}, {'E',"."}, {'F',"..-."},
              { 'G',"--."}, {'H',"...."}, {'I',".."}, {'J',".---"}, {'K',"-.-"}, {'L',".-.."},
              { 'M',"--"}, {'N',"-."}, {'O',"---"}, {'P',".--."}, {'Q',"--.-"}, {'R',".-."},
              { 'S',"..."}, {'T',"-"}, {'U',"..-"}, {'V',"...-"}, {'W',".--"}, {'X',"-..-"},
              { 'Y',"-.--"}, {'Z',"--.."},
          };

      public static string ConvertToMorse(string source)
      {
        return source.Aggregate(string.Empty, (current, c) => current + _morse[char.ToUpper(c)]);
      }

      public ResistanceSolver(IDictionary<string, int> dico, int maxSize)
      {
        Dico = dico;
        MaxSize = maxSize;
      }

      public long Compute(string sequence)
      {
        // OLD SOLUTION
        //long total = 0;
        //callCompute++;
        //if (sequence.Length == 0)
        //{
        //  return 1;
        //}
        //if (cache.ContainsKey(sequence))
        //{
        //  callCache++;
        //  return cache[sequence];
        //}

        //foreach (var word in Dico)
        //{
        //  callStartWith++;
        //  if (sequence.StartsWith(word))
        //  {
        //    checked
        //    {
        //      callRecursive++;
        //      total += Compute(sequence.Substring(word.Length));
        //    }
        //  }
        //}

        var sequenceLength = sequence.Length;
        var totaux = new long[sequenceLength + 1];
        totaux[0] = 1;

        for (var i = 1; i < sequenceLength + 1; i++)
        {
          for (var j = Math.Max(0, i - MaxSize); j < i; j++)
          {
            var subSequence = sequence.Substring(j, i-j);
            if (Dico.ContainsKey(subSequence))
            {
              totaux[i] += totaux[j] * Dico[subSequence];
            }
          }
        }
        return totaux[sequenceLength];
      }
    }
  }
}
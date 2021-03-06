using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodingGame.VERYHARD.CodingGame.VeryHard;
using NUnit.Framework;

namespace CodingGame.VERYHARD
{
  public class ResistanceTest
  {
    [TestCase("TOTO", "--------")]
    [TestCase("PAPA", ".--..-.--..-")]
    public void Shoud_convert_to_morse(string input, string result)
    {
      Assert.AreEqual(result, ResistanceSolver.ConvertToMorse(input));
    }

    [Test]
    public void Should_success_test_03()
    {
      var values = new List<string>()
      {
        ResistanceSolver.ConvertToMorse("HELL"),
        ResistanceSolver.ConvertToMorse("HELLO"),
        ResistanceSolver.ConvertToMorse("OWORLD"),
        ResistanceSolver.ConvertToMorse("WORLD"),
        ResistanceSolver.ConvertToMorse("TEST")
      };

      IDictionary<string, int> dico = new Dictionary<string, int>();
      
      int maxSize = 0;

      foreach (var value in values)
      {
        var morse =value;
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


      ResistanceSolver solver = new ResistanceSolver(dico, maxSize);

      Assert.AreEqual(2, solver.Compute("......-...-..---.-----.-..-..-.."));
    }

    [Test]
    public void Should_success_test_02()
    {
      var values = new List<string>()
      {
ResistanceSolver.ConvertToMorse("GOD"),
ResistanceSolver.ConvertToMorse("GOOD"),
ResistanceSolver.ConvertToMorse("MORNING"),
ResistanceSolver.ConvertToMorse("G"),
ResistanceSolver.ConvertToMorse("HELLO"),
      };

      IDictionary<string, int> dico = new Dictionary<string, int>();

      int maxSize = 0;

      foreach (var value in values)
      {
        var morse = value;
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


      ResistanceSolver solver = new ResistanceSolver(dico, maxSize);

      Assert.AreEqual(1, solver.Compute("--.-------.."));
    }

    [Test]
    public void Should_success_test_04()
    {
      var values = new List<string>(File.ReadAllLines(@"C:\Users\Sebastien\Documents\visual studio 2015\Projects\CodingGame\CodingGame\VERYHARD\input4.txt").Select(ResistanceSolver.ConvertToMorse));

      IDictionary<string, int> dico = new Dictionary<string, int>();

      int maxSize = 0;

      foreach (var value in values)
      {
        var morse = value;
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

      var compute = solver.Compute(".-...-.-...--......--....------.-.....---..--.-.----...-----.-.........-..--.-.-.----....--...-.--.--.--.--...-.-..--....--.--.-....--.-.-..--..-...-..-...-.......-.......-....--.--...-.-.......----....-.-.---..-.-.-.....-.-..----.--...-....--.-..-..-...-.........---...-.-.--..-.----....-..-.....--..-.-.-...........--.-.......--...-....---...-..---..-..--.--.....-.-.-..-.-.----.......-..-..-.----.--.-.-.---.-...-.....-....---..-..-..-----.-......-.-..---.-....--.-..-..--..-....-........-.-.....--...-....----.-......-...---.-...-.....-.-.-...----..-...........-.--.-.-.-....-..-..-.....-.-...-....-....-.-.-.-..--.---..-.-..-...--.-..--..-..-.--........-.-..-.-.----...---..-...-.-..-.-..--.-.-.-......-........--.-..-.-......---.-..-..-...--.-..-.-.........-...-..-...-.....--.-..-....--.----.-.-.-..-.--..----...--..-.-.------.....-..-.....----...--...--.-..-.-........--.-..-..--..-..-.-..-.--..--.-.-....-...-.-.----...-----.-.........--.-.----......-...-.-.-.-------..-....--..-.-.-...-.-.......-.-.-......-.-.-------..--...-.-..--..----....-...---.-...--.-..-.....-..-.------..-.-.-........-...-...-----.-.........-.--..-...-.-.-....--..-.-...--.-.-..--...-........--..-.-..-......--...-....-.-...-.....-.-.-------.------.--.-.-...-.-..--....--..........----....---..-...-.-.--.-.........-...-..-...-..-......--.-..-.-...--.....-...-.-......-...-...--.---..-.-.-.-...-.-.-..-.....--..........----....-.-..-.-..--.-.-.-...-..-------.--..--.-..-..-..--....----....-.-............-..-...-...--..----...-.-....-.-..--....-.-...-.--..--...-..-.........-.--..----....--.-.-...-..--....-......--.--...-.-......-.-..-.-..--..----..-..--..-....---.-..-..-----......-------..--.-..-....---..--......-..-.-.-.---.-...--.---..-.-..-...-.....--.-..-.-........--..----.--.-.-...-..--..--..-.-.-.-...-.-.-..-....-.-..--.....--.----.-.....-.-.....-..--...-.-..-..-....--..--....-....-...--.-..-----.-......--..-.-..-..-.--.--..-....-...-.-...--.-...--.-..--....--.......--..-.-...--.-..-....-....-...---..-....-...-..-.---.-.--......--.-.....-......-..--..-....-..-......-...-----.-.........---..-.-...--.-.-.--..-..--..--.-..-.--..-.-.-.......---.--...-.-....-.-.-------.-...-.------....-....--..-.-..--....---.---.--...-.-.......-.-.---..--...-----.-........----.---.-...-.-....---..-....--..-.--..-..----.--....-.-....---..-.-.-...--...-......-....-----.-......-....---..-....--..-.-...-.-.----....--....---.-..-.-..--.....--.---.-.-...-...-..--......--.-..-.-...-..--....---.--...-.--.--..-.---.-..-..-..----..-.-...-....---..--...-.-----.-...-.-.----.-..-...-.---..--.-.----.-..-...----..-....--.-...-..-..--.--..-...-.....--.-..-..-.-...--.-.-.--....-.--..-.-.-..--..--....--.--...-.-.......----..-...-.-.-.-...--.-..-.-.-..-...-..-......--.-.-.-....-...-.-..--..-..-.--.......--.-..-.-.-.....-..-.-..-...-.---....------..-.-......--......--.-..-.-..---.-..--...--.-..-.---..-...-.......-..-....-..-...-........-....---..-....--..-.-....-...--.-..-...--..-......-....-...-...--...---.-..--..----.-....--.-.----...-----.-.........--...-.-.----....-..-......-.-..-.-..--.-.-.-.......-..-.--.-..-.-.-....-...-.-.-..--....--........--.-..-..--..-.--...-.-.----....-..-..-.....-.--.-.-...........-.-.-.........--.-..-.-.-.-......--..-....-....-..----.--...-.....-......-.-..-.-..--.-.-.-.....--.-..-...-.....-...-.-..-.-.--..-....-....-..-.-..--.-.-.--..-.-..-..-.--.--..-....-.....-...--.-..-.-......-..-.--.-..-.-.-.....--.-.----...--..-.-...--......--.--..-....--.---..-.-.-....--..-....-...-....-....-.-...----..-....-...........--.-......-.......-..--.-......-...--.-........----.-----..--....-.-.----...........-.-.--.-..-.--.-.......--.-..-..--..-.-..---.--..-..--..---.-......-.-.........-...-..-..........-.-.-.-...-.--.-.....--.-..-..--..-.-.........--.-..-.-.-.-......--..-....-....-..----.--...-....-.-.---.-..-......--.----.-...-.--.........-.-----...---.-.......-.......--..-....-....-..-.-..........--.-.....-....--.----.-.-.-..-.--..----....-.-----...---.-.......--.-..-.-..--.-.-.-.........--.-.-....-..--..-.--..-.-..-.-...-...-...--.-.-.-..--.-.----...-----.-.....-.......--....-....-...--..----....-.-.-------..-.-.-....-.-.......--..-....-....-...--...---.-.....----.---.-..-.....-.-.----...--.--.......--......-..-.......-.-.-----.--..-..-.....-...--.......-.-..--------..-.---..-.--..-....-...--.-......-......---.--..-..--...-.-....-...-.-..--..-..-.--........-.-.----....-..-.....-...-.-.-.-..---.-.....-...--...-........--.--......------...-..-.....--.-..-.-...-.------..----.-.-..-.-----..--.-.-.--..-.--.-----.-..-..-.--.-..---..-----..-...-.-....---..--.--.--....-.---..----.-----..--.----.--.....--..-.---.----.-..-....-..----..-.--.---.--.-....--.-----.-..-....-.-..-..-.---.-.----...-...-...--...-------.-..-.-..-....-.-..-..-.--.-....-.-.-.-..----.-...-.-.....-.-.----......-.-..--..---.--.-..--.......-..-...-.-.-----......-.-..--....--..........----.........-.-..-......-.-.-....-.......-.......-..--....---.---.--...-.-......--...-........-.-----..-.------..-.-....-.-...-.-.-..-....-.-.----.......-.--..--...-.....-.-.----...--.-.--.-.----...--.....-........----.-..--...-...........--..-.-..-......---.--..-..--...-.-.....-..-...-.-......-.-......--.--...-.-........-...-.---.-.--.--..----.....-.-.-...-.-..-..-.-.....--.---.-.-.--.-.--..-.-..-..-.--.--..-........-.-..-...---.-..-.-.-....--....-...-.-.....-.....----..-......-.-......--.--..--.-.-...-.....-..-.--.-..-.-.-.-......-.-......-.-..--..-..-----.....-.......--..-..--...-......-.....-.....----.....-..--....----.-..--..-..-.--.......--.-..-........--......-....-....--...-........--.-.-...-.-.----...-----.-.....-...--.-.--....--..-.....---....--..--..-......-.-..-....-..-...--.-.----...-..-..-.-.-..-...-.....-..-....-..---.-...-.....-..-...-.-..----.--...-...-.-.-..----.-...-...---.-.....-.-....--...-..--.-..-..-...-.-..-..-..--....----.-..--..-..-.--........-----.-......-.--..-..----..-...-...-.-....-.-.........-........--.-..-.-.....---...-.-.-------..-.--..--...-..-...-..-.-..........---..-...--.-..-.-..--..-....-........-.-....-.-.----...-..-..-.-.-.....------.-.....-.-......-.......-..--....----.-.-------..--...-.-..--..----....-....-...-..........-.-.-......-....--.-........--...-.........-..--.---..-.-..-......-.-.-------..--...-.-..--..----.....-.-.-..--..-..-.--........-----.-.....-....--...-......-.....-...-....-.-.....-.-...--.....-...-.-..--...-..-....-...---.....--.-...........-....-..-.-.....--.-.-...-..--....-.-.-....-.....-------......-......-.....-..-..--.-.-...--...-.------....-....--..-.-..--.-......--..--.--...----.-.--....--.-..----.-..-...-...-....-...-....-..--....----.-..--..-..-.--.......--.-..-.----....-....--..-----...-...--.-.........-......--..........----.....-.-----.-.....-.----.-----.-.-.....-.-..-...-..-..-.--.-..-.-.-.-..-...-.-....--..--.-.-...-..--..-.-.-.----..-..--..--.-..-..-.....-.-..-...-..-..-.--.-..-.-.-.-..-...-.-....--...-..-...-..-.-.-.....-..--.-..-..-...-.-.-...-..-.-.........---..--...-.--..--...-...-..-..--....----.-..--..-..-.--........-....--...-......-.......--.-.......--....-.--...---.-.......--..-...-....-....-...-.-..-.-.-------..--...-.-..--..----..--.--.--.-.--..-.......-....-...-...-......--..-.-.-...-.-...-.-..-------.-.-.-...-.-..-..-...-.-.--.-.-...-..--..-..--....----.-..--..-..-.--.......--.-..-..-.-..--....----.-...--.....----...--.-..-...--...-........--.-.-......----.-............-.---.....-.-.....-.-.-------.-.-.-..-..-.-..-....-...-..-...-.-.--.-.--.-...--..-.-...--.-...-...-..-.....-..-.-.-.---.-..-.-...------..-.-.-........-..-.--.-..-.-.-.-.......-.-..-.-..--..----..-.-.......-..-...-..-.-.-.....--..-.-..-....-..-.-...--......-...-...-.....----.-....-.........-.-..-....-...---------.-.-...-..---..-.-..--..--.-.-------..--...-.-..--..----....--.-.-...-..--.....-..-..--....----.-..--..-..-.--.........--..-.-.--..-...-..--.-.....-.-....--..-.-....--..---..--.-....--..-.-....--...--.---..-...-.--.-.-..-........-.-....--...-........--..-.-..-...-...-...--..----.-.-......-...-....-...--....--.-...-......---.-.....-.----.-----.-.-.-.....-...--...-.........--..-.-...--.....-..-.--.-.-..-..-....--..--.--...-...--.....-...------.-....--..-.-.--...-......--.-..-..-......-...-.-..--..........----..--....-..-...-.-.--..-.-..-...-...---....-.-....--...---.--..-...-.-..---..-.---..--..-...--.-.......-...-...-.-..--...--....-....--.-..-.-....--.-.----.--.----...---..-...---..--...-...-.-.---.....-...-......-.--.....--.......-....-.-..----........-....-.-.-......-.....-..-...--.-...-----.--..-...-.-.---..---.--.....-...-..-.-...-..--..----.-....-...-..........-..........------.-.---.-...-.-..-..-......-.-..........-.---.-.......-...-......--.-..-..-..--.-..-.....-..--.-...--.-..----.....--..-.....-...-.--..-....-.--..-.-.--.........-.--..----....--.-.-...-..--.....-.-.-.---.-...-..--.-.-.....-..--.--.--.-.-.--..-....-....-.-----.-.....--.-..-..-.....-.-...-.-....-.-..--....-.-...-.--..--...-...-...-.-.-..-..-...-...--.-.-.-.....-.-...-..-.--.-..-.-.-.-..-..--..........----..-..-.------..-.-.-........-..-.-...-.-.-..-.......-..-.--.-..-.-.-.-.....-......-..-......-..--.-..-..-...-.-..-.-.-..---....-...---.-.-...-..--.-.-.-.-......--.-...----.-.-.-.-.---.-....--...-..-...-..-.-.-.--..----.----........-...-......-.....----.-.--..-....-.....--...-...........--.-..-..--..-.-.--.-..-..-..--.-..-.....--..--.--....--.-.....-..-.-..--.-.--....--...-......-...-..-.....---....--..--..-......-.-..-....-.-....-....-.-.-..----.-....-...-.-...---.--...-.-..-........-......-....--..-..--..-.--....-...-.-.---..-.-...--.-.--.-.-------..--...--.-..-..-..-.-----.-......-....---..-....--..-.-.-..---.-....--.-..-..-...--.-..-.--..--.-...---.--.-..-...-..-..........--.-..-..--..-.-.....-..-..-..--...--.---..-.-.-......-.-....-.---..-...-...-.-.....-.-....-...---.-.-...-..--.-.-.-.-......--.-...-...--....-...-.------....-....--..-.--..--.--...-..----.-.-...-..-..-..--.-.-..");

      Console.WriteLine("callCompute: {0}", solver.callCompute);
      Console.WriteLine("callCache: {0}", solver.callCache);
      Console.WriteLine("callStartWith: {0}", solver.callStartWith);
      Console.WriteLine("callRecursive: {0}", solver.callRecursive);

      Assert.AreEqual(57330892800, compute);

    }
  }
}
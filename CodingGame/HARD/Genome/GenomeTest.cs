//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using NUnit.Framework;

//namespace CodingGame.HARD.Genome
//{
//  public class GenomeTest
//  {
//    [Test]
//    public void Test_02()
//    {
//      var queue = new Queue<string>();
//      queue.Enqueue("AGATTA");
//      queue.Enqueue("GATTACA");
//      queue.Enqueue("TACAGA");
//      var result = Compute(queue);
//      Console.WriteLine(result);

//      Assert.AreEqual(10, result);
//    }

//    [Test]
//    public void Test_02_r()
//    {
//      var queue = new Queue<string>();
//      queue.Enqueue("GATTACA");
//      queue.Enqueue("AGATTA");
//      queue.Enqueue("TACAGA");
//      var result = Compute(queue);
//      Console.WriteLine(result);

//      Assert.AreEqual(10, result);
//    }

//    [Test]
//    public void Test_01()
//    {
//      var queue = new Queue<string>();
//      queue.Enqueue("AAC");
//      queue.Enqueue("CCTT");
//      var result = Compute(queue);
//      Console.WriteLine(result);

//      Assert.AreEqual(6, result);

//    }

//    [Test]
//    public void Test_03()
//    {
//      var queue = new Queue<string>();

//      queue.Enqueue("AA");
//      queue.Enqueue("ACT"); queue.Enqueue("TT");
//      var result = Compute(queue);
//      Console.WriteLine(result);

//      Assert.AreEqual(5, result);

//    }

//    [Test]
//    public void Test_04()
//    {
//      var values = new List<string>() { "TACAGA", "AGATTA", "GATTACA" };
//      foreach (var perm in GetPermutations(values, values.Count))
//      {
//        Console.Write("[");
//        perm.ToList().ForEach(x=>Console.Write(x + ","));
//        Console.WriteLine("]");

//      }
//    }

//    //private static int Compute(Queue<string> queue)
//    //{

//    //  var permutation = GetPermutations(queue, queue.Count).Select(x =>
//    //  {

//    //    return null;
//    //  }
//    //  );


//    //}

//    public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
//    {
//      if (length == 1) { return list.Select(t => new[] { t }); }

//      return GetPermutations(list, length - 1)
//              .SelectMany(t => list.Where(e => !t.Contains(e)), (t1, t2) => t1.Concat(new[] { t2 }));
//    }

//    public static string Nest(Queue<string> queue)
//    {
//      /*
//      def nest(strings):
//        ''' tries to nest strings, in order, and return the result'''
//        res = strings.pop()
//        while len(strings) > 0:
//            new = strings.pop()
//            for i in range(len(new) + 1):
//                subLast = min(i + len(res), len(new))
//                if (new[i:subLast] == res[:subLast - i]):
//                    res = new[:subLast] + res[subLast-i:] + new[i+len(res):]
//                    break
//        return res
//      */

//      //var res = queue.Dequeue();
//      //while (queue.Count > 0)
//      //{
//      //  var nest = queue.Dequeue();
//      //  for (int i = 0; i < nest.Length +1; i++)
//      //  {
//      //    var subLast = Math.Min(i + res.Length, nest.Length);
//      //    if (nest.Substring(i, subLast) == res.Substring(0, res.Length - subLast - i))
//      //    {
//      //      res = nest.Substring(0, nest.Length - subLast) + 
//      //    }
//      //  }
//      //}

//      //return res;
//    }
  
//  }

 
//}

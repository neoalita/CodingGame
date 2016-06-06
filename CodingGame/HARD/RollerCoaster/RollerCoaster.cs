namespace CodingGame.HARD.RollerCoaster
{
  using System;
  using System.Linq;
  using System.Collections.Generic;

  class Solution
  {
    static void Main(string[] args)
    {
      string[] inputs = Console.ReadLine().Split(' ');
      int C = int.Parse(inputs[1]);
      int N = int.Parse(inputs[2]);

      var groups = new Groups(C);
      for (int i = 0; i < N; i++)
      {
        groups.AddGroup(int.Parse(Console.ReadLine()));
      }

      var game = new RollerCoaster(int.Parse(inputs[0]), C, groups);

      game.Run();

      Console.WriteLine(game.Result);
    }
  }

  class RollerCoaster
  {
    private int Slots { get; set; }
    private int RemainingRuns { get; set; }
    private Groups Groups { get; set; }

    public int Result { get; set; } = 0;

    public RollerCoaster(int slots, int remainingRuns, Groups groups)
    {
      Slots = slots;
      RemainingRuns = remainingRuns;
      Groups = groups;
    }

    public void Run()
    {
      while (RemainingRuns > 0)
      {
        Result += Groups.GetNextSlot();
        RemainingRuns--;
      }
    }
  }

  class Groups
  {
    private Queue<int> Queue { get; set; } = new Queue<int>();

    private int ExpectedGroupSize { get; set; }

    public Groups(int expectedGroupSize)
    {
      ExpectedGroupSize = expectedGroupSize;
    }

    public void AddGroup(int group)
    {
      Queue.Enqueue(group);
    }

    public int GetNextSlot()
    {
      var nextGroup = 0;
      while (nextGroup < ExpectedGroupSize)
      {
        var last = Queue.First();
        if (nextGroup + last > ExpectedGroupSize)
        {
          return nextGroup;
        }

        nextGroup += last;
        Queue.Dequeue();
        Queue.Enqueue(last);
      }
      return nextGroup;
    }
  }
}
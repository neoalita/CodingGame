using NUnit.Framework;

namespace CodingGame.HARD.RollerCoaster
{
  class RollerCoasterTest
  {
    [Test]
    public void Should_success_test_01()
    {
      var groups = new Groups(3);
      groups.AddGroup(3);
      groups.AddGroup(1);
      groups.AddGroup(1);
      groups.AddGroup(2);

      var game = new RollerCoaster(3, 3, groups);
      game.Run();

      Assert.AreEqual(7, game.Result);
    }
  }
}
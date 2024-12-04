using KR2;

namespace KR2.Tests;

[TestFixture]
[TestOf(typeof(PriorityTurn<int>))]
public class PriorityTurnTest
{
    [Test]
    public void METHOD()
    {
        var turn = new PriorityTurn<int>();
        var expectedResult = new List<int>() { 0, 7, 6, 5, 10 };

        turn.Enqueue(0, 3);
        turn.Enqueue(5, 1);
        turn.Enqueue(10, 1);
        turn.Enqueue(6, 2);
        turn.Enqueue(7, 3);

        var actualResult = new List<int>();
        while (!turn.Empty())
        {
            actualResult.Add(turn.Dequeue());
        }

        Assert.That(expectedResult, Is.EqualTo(actualResult));
    }
}
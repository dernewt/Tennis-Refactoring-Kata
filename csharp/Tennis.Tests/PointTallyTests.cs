using Xunit;
using FluentAssertions;
using Tennis.Tennis8;

namespace Tennis.Tests;

public class PointTallyTests
{
    [Theory]
    [InlineData(0, 0, true)]
    [InlineData(3, 3, true)]
    [InlineData(5, 5, true)]
    [InlineData(0, 1, false)]
    [InlineData(1, 0, false)]
    [InlineData(3, 4, false)]
    public void TiedGameTest(int score1, int score2, bool expected)
    {
        var game = new PointTally("a", "b", score1, score2);
        game.IsTied().Should().Be(expected);
    }

    [Theory]
    [InlineData(5, 5, false)]
    [InlineData(0, 3, false)]
    [InlineData(2, 3, false)]
    [InlineData(4, 5, true)]
    [InlineData(0, 4, true)]
    public void EndGameTest(int score1, int score2, bool expected)
    {
        var game = new PointTally("a", "b", score1, score2);
        game.IsEndGame().Should().Be(expected);
    }

    [Fact]
    public void AwardMissingPlayerFails()
    {
        var noBob = new PointTally("mike", "joe");

        noBob.Invoking(p => p.Award("bob"))
            .Should().Throw<ArgumentException>();
    }

    [Fact]
    public void DuplicatePlayerFails()
    {
        var dupePlayer = () => new PointTally("", "");
        dupePlayer.Should().Throw<ArgumentException>();
    }

    [Theory]
    [ClassData(typeof(TestDataGenerator))]
    public void AcceptanceTest(int points1, int points2, string expected)
    {
        var game = new PointTally(
            TestDataGenerator.Player1Name,
            TestDataGenerator.Player2Name,
            points1, points2);

        game.ToString(PointTally.GameFormat).Should().Be(expected);
    }
}

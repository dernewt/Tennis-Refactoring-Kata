using System.Collections;
using Xunit;

namespace Tennis.Tests;

public class TestDataGenerator : IEnumerable<object[]>
{
    public const string Player1Name = "player1";
    public const string Player2Name = "player2";
    private readonly List<object[]> _data =
    [
        [0, 0, "Love-All"],
        [1, 1, "Fifteen-All"],
        [2, 2, "Thirty-All"],
        [3, 3, "Deuce"],
        [4, 4, "Deuce"],
        [1, 0, "Fifteen-Love"],
        [0, 1, "Love-Fifteen"],
        [2, 0, "Thirty-Love"],
        [0, 2, "Love-Thirty"],
        [3, 0, "Forty-Love"],
        [0, 3, "Love-Forty"],
        [4, 0, "Win for player1"],
        [0, 4, "Win for player2"],
        [2, 1, "Thirty-Fifteen"],
        [1, 2, "Fifteen-Thirty"],
        [3, 1, "Forty-Fifteen"],
        [1, 3, "Fifteen-Forty"],
        [4, 1, "Win for player1"],
        [1, 4, "Win for player2"],
        [3, 2, "Forty-Thirty"],
        [2, 3, "Thirty-Forty"],
        [4, 2, "Win for player1"],
        [2, 4, "Win for player2"],
        [4, 3, "Advantage player1"],
        [3, 4, "Advantage player2"],
        [5, 4, "Advantage player1"],
        [4, 5, "Advantage player2"],
        [15, 14, "Advantage player1"],
        [14, 15, "Advantage player2"],
        [6, 4, "Win for player1"],
        [4, 6, "Win for player2"],
        [16, 14, "Win for player1"],
        [14, 16, "Win for player2"],
    ];

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class TennisTests
{
    [Theory]
    [ClassData(typeof(TestDataGenerator))]
    public void Tennis1Test(int p1, int p2, string expected)
    {
        var game = new TennisGame1("player1", "player2");
        CheckAllScores(game, p1, p2, expected);
    }

    [Theory]
    [ClassData(typeof(TestDataGenerator))]
    public void Tennis2Test(int p1, int p2, string expected)
    {
        var game = new TennisGame2("player1", "player2");
        CheckAllScores(game, p1, p2, expected);
    }

    [Theory]
    [ClassData(typeof(TestDataGenerator))]
    public void Tennis3Test(int p1, int p2, string expected)
    {
        var game = new TennisGame3("player1", "player2");
        CheckAllScores(game, p1, p2, expected);
    }
    [Theory]
    [ClassData(typeof(TestDataGenerator))]
    public void Tennis4Test(int p1, int p2, string expected)
    {
        var game = new TennisGame4("player1", "player2");
        CheckAllScores(game, p1, p2, expected);
    }
    [Theory]
    [ClassData(typeof(TestDataGenerator))]
    public void Tennis5Test(int p1, int p2, string expected)
    {
        var game = new TennisGame5("player1", "player2");
        CheckAllScores(game, p1, p2, expected);
    }
    [Theory]
    [ClassData(typeof(TestDataGenerator))]
    public void Tennis6Test(int p1, int p2, string expected)
    {
        var game = new TennisGame6("player1", "player2");
        CheckAllScores(game, p1, p2, expected);
    }
    [Theory]
    [ClassData(typeof(TestDataGenerator))]
    public void Tennis7Test(int p1, int p2, string expected)
    {
        var game = new TennisGame7("player1", "player2");
        CheckAllScores(game, p1, p2, "Current score: " + expected + ", enjoy your game!");
    }

    private static void CheckAllScores(ITennisGame game, int player1Score, int player2Score, string expectedScore)
    {
        var highestScore = Math.Max(player1Score, player2Score);
        for (var i = 0; i < highestScore; i++)
        {
            if (i < player1Score)
                game.WonPoint("player1");
            if (i < player2Score)
                game.WonPoint("player2");
        }

        Assert.Equal(expectedScore, game.GetScore());
    }
}
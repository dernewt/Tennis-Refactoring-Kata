namespace Tennis.Tennis8;

public class PointTally : IFormattable
{
    private readonly Dictionary<string, int> PointsPerPlayer;

    public PointTally(string player1Name, string player2Name)
        : this(player1Name, player2Name, Love, Love) { }

    public PointTally(
    string player1Name,
    string player2Name,
    int player1StartingPoints,
    int player2StartingPoints)
    {
        if (player1Name == player2Name)
            throw new ArgumentException("Players not unique");

        PointsPerPlayer = new()
    {
        { player1Name, player1StartingPoints},
        { player2Name, player2StartingPoints}
    };
    }

    public bool IsTied() => PointsPerPlayer.Values.Distinct().Count() == 1;

    public bool IsEndGame() => PointsPerPlayer.Values.Max() > Forty && !IsTied();

    public void Award(string playerName)
    {
        try
        {
            PointsPerPlayer[playerName]++;
        }
        catch (KeyNotFoundException)
        {
            throw new ArgumentException("Invalid player");
        }
    }

    public override string ToString() => ToString(DetermineFormat());

    public const string EndGameFormat = "E";
    public const string TiedFormat = "T";
    public const string GameFormat = "A";
    public const string DashedFormat = "D";
    public string ToString(string? format, IFormatProvider? formatProvider = null)
        => format switch
        {
            _ when formatProvider is not null => throw new NotImplementedException(),
            DashedFormat or null or "" => ToStringWhenDashed(),
            GameFormat => ToString(DetermineFormat()),
            TiedFormat => ToStringWhenTied(),
            EndGameFormat => ToStringWhenEndGame(),
            _ => throw new FormatException()
        };

    protected const int NumberToWinBy = 2;

    protected const int Love = 0;
    protected const int Fifteen = 1;
    protected const int Thirty = 2;
    protected const int Forty = 3;

    protected string DetermineFormat() => this switch
    {
        _ when IsTied() => TiedFormat,
        _ when IsEndGame() => EndGameFormat,
        _ => string.Empty,
    };

    public string ToStringWhenDashed() => string.Join("-",
            PointsPerPlayer.Values.Select(ToStringSingular));

    protected string ToStringWhenTied() => PointsPerPlayer.Values.First() switch
    {
        >= Forty => "Deuce",
        var s => $"{ToStringSingular(s)}-All",
    };

    protected string ToStringWhenEndGame()
    {
        var orderedScores = PointsPerPlayer.OrderByDescending(s => s.Value);

        if (orderedScores.First().Value - orderedScores.Last().Value >= NumberToWinBy)
            return $"Win for {orderedScores.First().Key}";

        return $"Advantage {orderedScores.First().Key}";
    }

    protected static string ToStringSingular(int score)
    {
        return score switch
        {
            Love => nameof(Love),
            Fifteen => nameof(Fifteen),
            Thirty => nameof(Thirty),
            _ => nameof(Forty),
        };
    }
}

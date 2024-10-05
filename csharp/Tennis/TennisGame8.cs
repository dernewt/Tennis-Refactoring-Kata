using Tennis.Tennis8;

namespace Tennis;
public class TennisGame8(string Player1Name, string Player2Name) : ITennisGame
{
    private readonly PointTally Points = new(Player1Name, Player2Name);
    
    public string GetScore() => Points.ToString(PointTally.GameFormat);
    
    public void WonPoint(string playerName) => Points.Award(playerName);

}

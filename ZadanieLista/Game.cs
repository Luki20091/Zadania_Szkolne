public enum GameType
{
    RPG,
    Action,
    Adventure,
    Strategy,
    Simulation,
    Puzzle,
    Sports,
    Other
}

public class Game
{
    public string Title { get; set; }
    public int Year { get; set; }
    public GameType Type { get; set; }
}

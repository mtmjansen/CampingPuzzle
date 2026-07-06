namespace CampingPuzzle.Server.Classes;

public class Tile
{
    public int Row { get; set; }
    public int Column { get; set; }
    public TileType Type { get; set; }

    public Tile(int row, int column, TileType type)
    {
        Row = row;
        Column = column;
        Type = type;
    }
}

namespace CampingPuzzle.Server.Classes;

public class Camping
{
    public int Rows { get; set; }
    public int Columns { get; set; }
    public Tile[] Tiles { get; init; }

    private IEnumerable<Tile> Init()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                yield return new Tile(row, column, TileType.Empty);
            }
        }
    }

    public Camping(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        Tiles = [.. Init()];
    }
}

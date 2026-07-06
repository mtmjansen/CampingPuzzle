using CampingPuzzle.Server.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace CampingPuzzle.Server.Classes;

public class Tile
{
    public int Row { get; set; }
    public int Column { get; set; }

    private TileType _type;
    public static IHubContext<CampingHub>? HubContext { get; set; }

    public TileType Type
    {
        get
        {
            NotifyTypeRead();
            return _type;
        }
        set => _type = value;
    }

    public Tile(int row, int column, TileType type)
    {
        Row = row;
        Column = column;
        _type = type;
    }

    private void NotifyTypeRead()
    {
        var tile = new
        {
            Row,
            Column,
            Type = _type.ToString(),
        };
        _ = HubContext?.Clients.All.SendAsync("TileTypeRead", tile);
    }
}

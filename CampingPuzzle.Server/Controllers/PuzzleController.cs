using CampingPuzzle.Server.Classes;
using Microsoft.AspNetCore.Mvc;

namespace CampingPuzzle.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PuzzleController : ControllerBase
    {
        [HttpGet(Name = "GetTileType")]
        public IActionResult GetTileType([FromQuery] int row, [FromQuery] int column)
        {
            var tile = new Tile(row, column, TileType.Grass); // Replace with actual logic to get the tile type based on row and column

            return Ok(tile.Type.ToString()); // trigger the NotifyTypeRead method in the Tile class
        }
    }
}

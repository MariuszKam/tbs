using System;
using Godot;
using TBS.Domain.Hex;
using TBS.Infrastructure.Logging;

namespace TBS.Presentation.Scripts;

public partial class HexGridController : Node
{
    private HexGrid _grid;
    private Random _random = new Random();

    [Export] private TileMapLayer _tileMap;

    public override void _Ready()
    {
        _tileMap = GetNode<TileMapLayer>("HexTileMap");
        _grid = new HexGrid(120, 120);
        RenderAllTiles();
    }

    private void RenderAllTiles()
    {
        foreach (var tile in _grid.GetAllTiles())
        {
            RenderTile(tile);
        }

        GameLogger.Info($"All tiles: {_grid.GetAllTiles()}");
    }

    private void RenderTile(HexTile tile)
    {
        Vector2I coords = new(tile.Position.Q, tile.Position.R);

        _tileMap.SetCell(
            coords: coords,
            sourceId: 1,
            atlasCoords: new Vector2I(_random.Next(3), _random.Next(2))
        );
    }


    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButton)
        {
            if (mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Left)
            {
                Vector2 world = _tileMap.GetGlobalMousePosition();
                Vector2I coords = _tileMap.LocalToMap(_tileMap.ToLocal(world));

                GameLogger.Info($"Cords of tile clicked: {coords.X},{coords.Y}");
            }
        }
    }
}
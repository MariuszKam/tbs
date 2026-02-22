using System;
using Godot;
using TBS.Domain.Hex;
using TBS.Infrastructure.Logging;

namespace TBS.Presentation.Scripts;

public partial class HexGridController : Node2D
{
	private HexGrid _grid;
	
	private Random _random = new Random();
	

	[Export] private TileMapLayer _tileMap;

	public override void _Ready()
	{
		_tileMap = GetNode<TileMapLayer>("../TileMapLayer");
		_grid = new HexGrid(10, 20);
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
		GameLogger.Info($"Passed tile: {tile}");
		Vector2I coords = new(tile.Position.Q, tile.Position.R);
		GameLogger.Info($"Coords: {coords}");

		_tileMap.SetCell(
			coords: coords,
			sourceId: 1,
			atlasCoords: new Vector2I(_random.Next(3), _random.Next(2))
		);
		
		GameLogger.Info($"Cell with cords: {tile.Position.Q},{tile.Position.R} created");
	}

	

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseButton 
		    && mouseButton.Pressed 
		    && mouseButton.ButtonIndex == MouseButton.Left)
		{
			Vector2 global = GetGlobalMousePosition();
			Vector2 local = _tileMap.ToLocal(global);
			Vector2I coords = _tileMap.LocalToMap(local);

			GameLogger.Info($"Cords of tile clicked: {coords.X},{coords.Y}");
		}
	}
}

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
		_grid = new HexGrid(10, 20);
		GenerateMap();
	}

	private void GenerateMap()
	{
		for (int q = 0; q < _grid.Width; q++)
		{
			for (int r = 0; r < _grid.Height; r++)
			{
				_tileMap.SetCell(
					coords: new Vector2I(q, r),
					sourceId: 1,
					atlasCoords: new Vector2I(_random.Next(4), _random.Next(2))
				);
				GameLogger.Info($"Cell with cords: {q},{r} created");
			}
		}
		GameLogger.Info($"Map generated with {_grid.Width}x{_grid.Height}");
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseButton && mouseButton.Pressed)
		{
			Vector2 globalMousePosition = GetGlobalMousePosition();
			Vector2 localPosition = _tileMap.ToLocal(globalMousePosition);
			Vector2I coords = _tileMap.LocalToMap(localPosition);
			
			Hex hex = new Hex(coords.X, coords.Y);
			GameLogger.Info($"Clicked on {hex.Q}, {hex.R}");
			
		}
	}
}

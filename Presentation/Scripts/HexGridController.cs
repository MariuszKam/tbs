using System;
using Godot;
using TBS.Domain.Hex;
using TBS.Infrastructure.Logging;

namespace TBS.Presentation.Scripts;

public partial class HexGridController : Node2D
{
	private HexGrid _grid;
	
	private Random _random = new Random();
	
	private Camera2D _camera;

	private bool _isDragging = false;
	private Vector2 _lastMousePosition;
	

	[Export] private TileMapLayer _tileMap;

	public override void _Ready()
	{
		_tileMap = GetNode<TileMapLayer>("../TileMapLayer");
		_camera = GetNode<Camera2D>("../Camera2D");
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
		if (@event is InputEventMouseButton mouseButton)
		{
			if (mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Left)
			{
				Vector2 global = GetGlobalMousePosition();
				Vector2 local = _tileMap.ToLocal(global);
				Vector2I coords = _tileMap.LocalToMap(local);

				GameLogger.Info($"Cords of tile clicked: {coords.X},{coords.Y}");
			}
			
			_isDragging = mouseButton.Pressed && mouseButton.ButtonIndex == MouseButton.Left;
			_lastMousePosition = mouseButton.Position;
			
		}
		
		if (@event is InputEventMouseMotion motion && _isDragging)
		{
			Vector2 delta = motion.Position - _lastMousePosition;

			_camera.Position -= delta;

			_lastMousePosition = motion.Position;
		}
		
		
	}
	
	
}

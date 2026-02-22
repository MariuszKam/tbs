using System.Collections.Generic;

namespace TBS.Domain.Hex;

public sealed class HexGrid
{
    public int Width { get; }
    public int Height { get; }
    private readonly Dictionary<Hex, HexTile> _tiles = new();

    public HexGrid(int width, int height)
    {
        Width = width;
        Height = height;

        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for (int q = 0; q < Width; q++)
        {
            for (int r = 0; r < Height; r++)
            {
                var hex = new Hex(q, r);
                _tiles[hex] = new HexTile(hex, true);
            }
        }
    }

    public HexTile? GetTile(Hex hex)
    {
        _tiles.TryGetValue(hex, out var tile);
        return tile;
    }
    
    public IEnumerable<HexTile> GetAllTiles()
    {
        return _tiles.Values;
    }

    public bool ToggleWalkable(Hex hex)
    {
        return _tiles.TryGetValue(hex, out var tile) && tile.ToggleWalkable();
    }

    public IEnumerable<HexTile> GetNeighbours(Hex hex)
    {
        foreach (var neighbour in hex.Neighbours())
        {
            if (_tiles.TryGetValue(neighbour, out var tile))
                yield return tile;
        }
    }
}
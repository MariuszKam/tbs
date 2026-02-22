namespace TBS.Domain.Hex;

public class HexTile
{
    public Hex Position { get; }
    public bool IsWalkable { get; private set; }

    public HexTile(Hex position, bool isWalkable)
    {
        Position = position;
        IsWalkable = isWalkable;
    }

    public bool ToggleWalkable()
    {
        IsWalkable = !IsWalkable;
        return IsWalkable;
    }
}
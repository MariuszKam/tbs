using System.Collections.Generic;

namespace TBS.Hex;

public sealed class HexGrid(int width, int height)
{
    
    //Constant just for test and beginning
    public int Width { get; } = width;
    public int Height { get; }= height;
    //Six directions
    private static readonly (int dq, int dr)[] Directions =
    [
        ( 1,  0),
        (-1,  0),
        ( 0,  1),
        ( 0, -1),
        ( 1, -1),
        (-1,  1)
    ];

    private bool IsInside(Hex hex)
    {
        return hex.Q >= 0 &&
               hex.R >= 0 &&
               hex.Q < Width &&
               hex.R < Height;
    }

    public IEnumerable<Hex> GetNeighbours(Hex hex)
    {
        foreach (var (dq, dr) in Directions)
        {
            var candidate = new Hex(hex.Q + dq, hex.R + dr);

            if (IsInside(candidate))
                yield return candidate;
        }
    }
    
}
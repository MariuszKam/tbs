using System;
using System.Collections.Generic;

namespace TBS.Domain.Hex;

public readonly record struct Hex(int Q, int R)
{
    private static readonly (int dq, int dr)[] Directions =
    [
        ( 1,  0),
        (-1,  0),
        ( 0,  1),
        ( 0, -1),
        ( 1, -1),
        (-1,  1)
    ];

    public IEnumerable<Hex> Neighbours()
    {
        foreach (var (dq, dr) in Directions)
            yield return new Hex(Q + dq, R + dr);
    }
    
    public int DistanceTo(Hex other)
    {
        int dq = Q - other.Q;
        int dr = R - other.R;
        int ds = (-Q - R) - (-other.Q - other.R);

        return (Math.Abs(dq) + Math.Abs(dr) + Math.Abs(ds)) / 2;
    }
}
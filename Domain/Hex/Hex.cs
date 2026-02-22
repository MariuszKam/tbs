using System;

namespace TBS.Domain.Hex;

public readonly record struct Hex(int Q, int R)
{
    public int DistanceTo(Hex other)
    {
        int dq = Q - other.Q;
        int dr = R - other.R;
        int ds = (-Q - R) - (-other.Q - other.R);

        return (Math.Abs(dq) + Math.Abs(dr) + Math.Abs(ds)) / 2;
    }
}
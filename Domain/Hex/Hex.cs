using System;

namespace TBS.Hex;

public sealed class Hex(int q, int r) : IEquatable<Hex>
{
    public int Q { get; } = q;
    public int R { get; } = r;

    
    public int DistanceTo(Hex other)
    {
        int dq = Q - other.Q;
        int dr = R - other.R;
        int ds = (-Q - R) - (-other.Q - other.R);

        return (Math.Abs(dq) + Math.Abs(dr) + Math.Abs(ds)) / 2;
    }
    
    public bool Equals(Hex other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Q == other.Q && R == other.R;
    }

    public override bool Equals(object obj)
    {
        return ReferenceEquals(this, obj) || obj is Hex other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Q, R);
    }
}
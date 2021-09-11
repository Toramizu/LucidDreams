using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Coordinate
{
    const int BIG_INT = 1000;

    [SerializeField] int x;
    public int X { get { return x; } }
    [SerializeField] int y;
    public int Y { get { return y; } }

    public Coordinate() { }
    public Coordinate(int x, int y) { this.x = x; this.y = y; }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != typeof(Coordinate)) return false;
        return Equals((Coordinate)obj);
    }

    public bool Equals(Coordinate coo)
    {
        if (ReferenceEquals(null, coo)) return false;
        if (ReferenceEquals(this, coo)) return true;
        return x == coo.x && y == coo.y;
    }

    public override int GetHashCode()
    {
        return BIG_INT * x + y;
    }

    public override string ToString()
    {
        return x.ToString() + ';' + y;
    }
}

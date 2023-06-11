using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex_Hex
{
    private Coord coord;
    private readonly int cellSize = 1;
    public Vector3 centerPos = Vector3.zero;

    public Vertex_Hex(Coord coord, int cellSize = 1)
    {
        this.coord = coord;
        this.cellSize = cellSize;
    }

    public Vector2 GetWorldPos()
    {
        
        return new Vector2(Mathf.Sqrt(3) * ( coord.q + coord.r/2.0f)*cellSize , 1.5f * coord.r*cellSize);
    }

    public static List<Vertex_Hex> Hex(int radius,int cellSize = 1)
    {
        List<Vertex_Hex> result = new List<Vertex_Hex>();
        foreach (var coord in Coord.CoordHex( new Coord(0,0,0),radius))
        {
            result.Add(new Vertex_Hex(coord,cellSize));
        }

        return result;
    }

    public static List<Vertex_Hex> GetRingVertices(List<Vertex_Hex> vertices,int radius)
    {
        if (radius == 0) return vertices.GetRange(0, 1);
        // 前面总数：(a1 + an)/2 * n 
        return vertices.GetRange((3+3*(radius-1))*(radius-1)+1, 6 * radius);
    }

    //将值转变为num 为hash做准备 
    public int ConvertToNum()
    {
        return coord.q * 100 + coord.r;
    }
}

public class Coord
{
    public readonly int q;
    public readonly int r;
    public readonly int s;

    public Coord(int q, int r, int s)
    {
        this.q = q;
        this.r = r;
        this.s = s;
    }

     static readonly Coord[] Direction =
    {
        new Coord(0, 1, -1),
        new Coord(-1, 1, 0),
        new Coord(-1, 0, 1),
        new Coord(0, -1, 1),
        new Coord(1, -1, 0),
        new Coord(1, 0, -1),
    };
     
     static Coord CoordScale(Coord coord, int scale)
    {
        return new Coord(coord.q * scale, coord.r * scale, coord.s * scale);
    }

     private static List<Coord> CoordRing(Coord center, int radius)
    {
        List<Coord> result = new List<Coord>();
        if (radius == 0)
        {
            result.Add(center);
            return result;
        }

        Coord temp = CoordScale(Direction[4], radius) + center;
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < radius; j++)
            {
                result.Add(temp);
                temp = temp + Direction[i];
            }
        }
        return result;
    }

    public static List<Coord> CoordHex(Coord center, int radius)
    {
        List<Coord> result = new List<Coord>();
        for (int i = 0; i <= radius; i++)
        {
            result.AddRange( CoordRing(center, i));
        }

        return result;
    }

    public static Coord operator +(Coord a, Coord b)
    {
        return new Coord(a.q+b.q,a.r+b.r,a.s+b.s);
    }

}
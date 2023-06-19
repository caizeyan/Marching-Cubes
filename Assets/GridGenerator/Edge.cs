using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//边
public class Edge
{
    public VertexHex vertexHexA;
    public VertexHex vertexHexB;

    public VertexMid mid;
    //hash唯一id
    public int UID = 0;

    //a : r q s 小
    public Edge(VertexHex hexA, VertexHex hexB)
    {
        vertexHexA = hexA;
        vertexHexB = hexB;
        UID = GetEdgeUID(hexA, hexB);
        mid = new VertexMid(this);
    }


    public static int GetEdgeUID(VertexHex a, VertexHex b)
    {
        int numA = Mathf.Min(a.ConvertToNum(),b.ConvertToNum());
        int numB = Mathf.Max(a.ConvertToNum(), b.ConvertToNum());
        return numA * 10000 + numB;
    }
    
}

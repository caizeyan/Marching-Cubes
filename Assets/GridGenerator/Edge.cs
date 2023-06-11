using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//边
public class Edge
{
    public Vertex_Hex vertexHexA;
    public Vertex_Hex vertexHexB;

    public Vertex_Mid mid;
    //hash唯一id
    public int UID = 0;

    //a : r q s 小
    public Edge(Vertex_Hex hexA, Vertex_Hex hexB)
    {
        vertexHexA = hexA;
        vertexHexB = hexB;
        UID = GetEdgeUID(hexA, hexB);
        mid = new Vertex_Mid(this);
    }


    public static int GetEdgeUID(Vertex_Hex a, Vertex_Hex b)
    {
        int numA = Mathf.Min(a.ConvertToNum(),b.ConvertToNum());
        int numB = Mathf.Max(a.ConvertToNum(), b.ConvertToNum());
        return numA * 10000 + numB;
    }
    
}

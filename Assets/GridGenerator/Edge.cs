using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge
{
    public Vertex_Hex vertexHexA;
    public Vertex_Hex vertexHexB;

    public Vertex_Mid mid;
    //hash唯一id
    private int UID = 0;

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
    
    
    public static Dictionary<int, Edge> edges = new Dictionary<int, Edge>();
    public static Edge GetOrCreateEdge(Vertex_Hex a, Vertex_Hex b)
    {
        int uid = GetEdgeUID(a, b);
        if (!edges.ContainsKey(uid))
        {
            edges[uid] = new Edge(a, b);
        }
        return edges[uid];
    }
    
    public static Edge GetEdge(Vertex_Hex a, Vertex_Hex b)
    {
        int uid = GetEdgeUID(a, b);
        return edges[uid];
    }

    public static void RemoveEdge(Vertex_Hex a, Vertex_Hex b)
    {
        int uid = GetEdgeUID(a, b);
        if (edges.ContainsKey(uid))
        {
            edges[uid] = null;
        }
    }
    
    public static void RemoveEdge(Edge edge)
    {
     
        if (edges.ContainsKey(edge.UID))
        {
            edges[edge.UID] = null;
        }
    }
}

//三角形

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Triangle
{
    public Vertex_Hex vertexHexA;
    public Vertex_Hex vertexHexB;
    public Vertex_Hex vertexHexC;

    public Edge edgeA;
    public Edge edgeB;
    public Edge edgeC;
    public Vertex_TriangleCenter center;
    public HashSet<Edge> edges = new HashSet<Edge>();
    
    public Triangle(Vertex_Hex vertexHexA, Vertex_Hex vertexHexB, Vertex_Hex vertexHexC)
    {
        this.vertexHexA = vertexHexA;
        this.vertexHexB = vertexHexB;
        this.vertexHexC = vertexHexC;
        //加边
        edgeA = Grid.Instant.GetOrCreateEdge(vertexHexA,vertexHexB);
        edgeB = Grid.Instant.GetOrCreateEdge(vertexHexB, vertexHexC);
        edgeC = Grid.Instant.GetOrCreateEdge(vertexHexC, vertexHexA);
        edges.Add(edgeA);
        edges.Add(edgeB);
        edges.Add(edgeC);

        center = new Vertex_TriangleCenter(this);
    }

    //判断是否有共用的边
    public bool IsNeighbor(Triangle target)
    {
        HashSet<Edge> temp = new HashSet<Edge>(edges);
        temp.IntersectWith(target.edges);
        return temp.Count == 1;
    }
    
    //判断是否有共用的边
    public Edge GetNeighborEdge(Triangle target)
    {
        HashSet<Edge> temp = new HashSet<Edge>(edges);
        temp.IntersectWith(target.edges);
        return temp.Single();
    }

    
    //找到所有相邻的三角形
    public List<Triangle> FindAllNeighbors(List<Triangle> triangles)
    {
        List<Triangle> result = new List<Triangle>();
        foreach (var triangle in triangles)
        {
            if (IsNeighbor(triangle))
            {
                result.Add(triangle);
            }
        }
        return result;
    }

    //找到独立的点
    public Vertex_Hex FindIsolateVertex(Edge edge)
    {
        int numA = edge.vertexHexA.ConvertToNum();
        int numB = edge.vertexHexB.ConvertToNum();
        if (vertexHexA.ConvertToNum() != numA && vertexHexA.ConvertToNum() != numB)
        {
            return vertexHexA;
        }
        if (vertexHexB.ConvertToNum() != numA && vertexHexB.ConvertToNum() != numB)
        {
            return vertexHexB;
        }

        return vertexHexC;
    }

    public Quad MergeNeighborTriangle(Triangle neighbor)
    {
        var edge = GetNeighborEdge(neighbor);
        var a = edge.vertexHexA;
        var c = edge.vertexHexB;
        var b = FindIsolateVertex(edge);
        var d = neighbor.FindIsolateVertex(edge);
        return new Quad(a, b, c, d);
    }

    private static List<Triangle> TriangleRing(int radius,List<Vertex_Hex> vertices)
    {
        List<Triangle> result = new List<Triangle>();
        List<Vertex_Hex> outVertex = Vertex_Hex.GetRingVertices(vertices, radius);
        List<Vertex_Hex> inVertex = Vertex_Hex.GetRingVertices(vertices, radius - 1);
        int outCount = outVertex.Count;
        int inCount = inVertex.Count;
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < radius; j++)
            {
                //绘制三角形  两个外顶点+一个内顶点 
                 var a = outVertex[(i * radius + j) % outCount];
                 var b = outVertex[(i * radius + j + 1) % outCount];
                 var c = inVertex[(i * (radius - 1) + j) % inCount];
                result.Add(new Triangle(a,b,c)); 
                
                //绘制三角形  两个内顶点+一个外顶点 第一圈没有内置三角形     且内圈三角形比外圈少一个
                if (radius > 1 && j!= radius-1)
                {
                    var d = inVertex[(i * (radius - 1) + j+1) % inCount];
                    result.Add(new Triangle(b,c,d));
                }
            }
        }
        return result;
    }

    public static List<Triangle> TriangleHex(int radius, List<Vertex_Hex> vertices)
    {
        List<Triangle> result = new List<Triangle>();
        for (int i = 1; i <= radius; i++)
        {
            result.AddRange(TriangleRing(i,vertices));
        }
        return result;
    }
    
    public List<SubQuad> SubDivide()
    {
        List<SubQuad> result = new List<SubQuad>();
        result.Add(new SubQuad(vertexHexA,edgeA.mid,center,edgeC.mid));
        result.Add(new SubQuad(vertexHexB,edgeB.mid,center,edgeA.mid));
        result.Add(new SubQuad(vertexHexC,edgeC.mid,center,edgeB.mid));
        return result;
    }
}

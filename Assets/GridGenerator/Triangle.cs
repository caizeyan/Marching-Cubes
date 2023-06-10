//三角形

using System.Collections.Generic;

public class Triangle
{
    public Vertex a;
    public Vertex b;
    public Vertex c;

    public Triangle(Vertex a, Vertex b, Vertex c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    private static List<Triangle> TriangleRing(int radius,List<Vertex> vertices)
    {
        List<Triangle> result = new List<Triangle>();
        List<Vertex> outVertex = Vertex.GetRingVertices(vertices, radius);
        List<Vertex> inVertex = Vertex.GetRingVertices(vertices, radius - 1);
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

    public static List<Triangle> TriangleHex(int radius, List<Vertex> vertices)
    {
        List<Triangle> result = new List<Triangle>();
        for (int i = 1; i <= radius; i++)
        {
            result.AddRange(TriangleRing(i,vertices));
        }
        return result;
    }
}

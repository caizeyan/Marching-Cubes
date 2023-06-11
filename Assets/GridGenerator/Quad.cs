using System.Collections.Generic;

public class Quad
{
    public Vertex_Hex vertexHexA;
    public Vertex_Hex vertexHexB;
    public Vertex_Hex vertexHexC;
    public Vertex_Hex vertexHexD;

    public Edge edgeAB;
    public Edge edgeBC;
    public Edge edgeCD;
    public Edge edgeDA;

    public Vertex_QuadCenter center;

    public Quad(Vertex_Hex hexA, Vertex_Hex hexB, Vertex_Hex hexC, Vertex_Hex hexD)
    {
        vertexHexA = hexA;
        vertexHexB = hexB;
        vertexHexC = hexC;
        vertexHexD = hexD;
        
        edgeAB = Edge.GetOrCreateEdge(hexA,hexB);
        edgeBC = Edge.GetOrCreateEdge(hexB,hexC);
        edgeCD = Edge.GetOrCreateEdge(hexC,hexD);
        edgeDA = Edge.GetOrCreateEdge(hexD,hexA);

        center = new Vertex_QuadCenter(this);
    }

    public List<SubQuad> SubDivide()
    {
        List<SubQuad> result = new List<SubQuad>();
        result.Add(new SubQuad(vertexHexA,edgeAB.mid,center,edgeDA.mid));
        result.Add(new SubQuad(vertexHexB,edgeBC.mid,center,edgeAB.mid));
        result.Add(new SubQuad(vertexHexC,edgeCD.mid,center,edgeBC.mid));
        result.Add(new SubQuad(vertexHexD,edgeDA.mid,center,edgeCD.mid));
        return result;
    }
}

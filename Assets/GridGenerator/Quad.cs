using System.Collections.Generic;

public class Quad
{
    public VertexHex vertexHexA;
    public VertexHex vertexHexB;
    public VertexHex vertexHexC;
    public VertexHex vertexHexD;

    public Edge edgeAB;
    public Edge edgeBC;
    public Edge edgeCD;
    public Edge edgeDA;

    public VertexQuadCenter center;

    public Quad(VertexHex hexA, VertexHex hexB, VertexHex hexC, VertexHex hexD)
    {
        vertexHexA = hexA;
        vertexHexB = hexB;
        vertexHexC = hexC;
        vertexHexD = hexD;
        
        edgeAB = Grid.Instant.GetOrCreateEdge(hexA,hexB);
        edgeBC = Grid.Instant.GetOrCreateEdge(hexB,hexC);
        edgeCD = Grid.Instant.GetOrCreateEdge(hexC,hexD);
        edgeDA = Grid.Instant.GetOrCreateEdge(hexD,hexA);

        center = new VertexQuadCenter(this);
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

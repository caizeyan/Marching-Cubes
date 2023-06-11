public class SubQuad
{
    public Vertex_Hex vertexA;
    public Vertex_Mid vertexB;
    public Vertex_Center vertexC;
    public Vertex_Mid vertexD;

    public SubQuad(Vertex_Hex a, Vertex_Mid b, Vertex_Center c, Vertex_Mid d)
    {
        vertexA = a;
        vertexB = b;
        vertexC = c;
        vertexD = d;
    }
}
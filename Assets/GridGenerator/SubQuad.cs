//细分的四边形

using UnityEngine;

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

    /// <summary>
    /// 网格平滑
    /// </summary>
    /// <param name="factor">平滑力度  为1则为完全的正边形 0-1 </param>
    public void SmoothToCube(float factor)
    {
        factor = Mathf.Clamp01(factor);
        Vector3 centerPos = (vertexA.CurPos + vertexB.CurPos + vertexC.CurPos + vertexD.CurPos)/4;
        Vector3 cubeAPos = ((vertexA.CurPos - centerPos) +
                           Quaternion.AngleAxis(-90, Vector3.forward) * (vertexB.CurPos - centerPos) +
                           Quaternion.AngleAxis(-180, Vector3.forward) * (vertexC.CurPos - centerPos) +
                           Quaternion.AngleAxis(-270, Vector3.forward) * (vertexD.CurPos - centerPos)) / 4;
        Vector3 cubeBPos = Quaternion.AngleAxis(90, Vector3.forward) * cubeAPos;
        Vector3 cubeCPos = Quaternion.AngleAxis(180, Vector3.forward) * cubeAPos;
        Vector3 cubeDPos = Quaternion.AngleAxis(270, Vector3.forward) * cubeAPos;
        vertexA.offset += (cubeAPos + centerPos - vertexA.CurPos) * factor;
        vertexB.offset += (cubeBPos + centerPos - vertexB.CurPos) * factor;
        vertexC.offset += (cubeCPos + centerPos - vertexC.CurPos) * factor;
        vertexD.offset += (cubeDPos + centerPos - vertexD.CurPos) * factor;
    }
}
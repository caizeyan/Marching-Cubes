//细分的四边形

using UnityEngine;

public class SubQuad
{
    public VertexHex vertexA;
    public VertexMid vertexB;
    public VertexCenter vertexC;
    public VertexMid vertexD;

    public SubQuad(VertexHex a, VertexMid b, VertexCenter c, VertexMid d)
    {
        vertexA = a;
        vertexB = b;
        vertexC = c;
        vertexD = d;
        
        //确保顺序为顺时针
        Vector3 vecAB = vertexA.initialPos - vertexB.initialPos;
        Vector3 vecAD = vertexA.initialPos - vertexD.initialPos;
        if (vecAB.x*vecAD.z < vecAB.z*vecAD.x)
        {
            vertexB = d;
            vertexD = b;
        }
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
                           Quaternion.AngleAxis(90, Vector3.up) * (vertexB.CurPos - centerPos) +
                           Quaternion.AngleAxis(180, Vector3.up) * (vertexC.CurPos - centerPos) +
                           Quaternion.AngleAxis(270, Vector3.up) * (vertexD.CurPos - centerPos)) / 4;
        Vector3 cubeBPos = Quaternion.AngleAxis(-90, Vector3.up) * cubeAPos;
        Vector3 cubeCPos = Quaternion.AngleAxis(-180, Vector3.up) * cubeAPos;
        Vector3 cubeDPos = Quaternion.AngleAxis(-270, Vector3.up) * cubeAPos;
        vertexA.offset += (cubeAPos + centerPos - vertexA.CurPos) * factor;
        vertexB.offset += (cubeBPos + centerPos - vertexB.CurPos) * factor;
        vertexC.offset += (cubeCPos + centerPos - vertexC.CurPos) * factor;
        vertexD.offset += (cubeDPos + centerPos - vertexD.CurPos) * factor;
        
      
    }
}
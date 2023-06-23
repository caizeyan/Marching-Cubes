//细分的四边形

using System;
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

    public Vector3 GetCenterPos()
    {
        return (vertexA.CurPos + vertexB.CurPos + vertexC.CurPos + vertexD.CurPos) / 4;
    }
}

public class SubQuadCueb
{
    private SubQuad quad;
    private int y;
    public VertexY[] vertexs = new VertexY[8];
    public int bits;

    public SubQuadCueb(SubQuad quad, int y)
    {
        this.quad = quad;
        this.y = y;
        InitVertexs();
        UpdateBits();
    }

    public void InitVertexs()
    {
        vertexs[0] = MyGrid.Instant.GetVertexY(quad.vertexA, y);
        vertexs[1] = MyGrid.Instant.GetVertexY(quad.vertexB, y);
        vertexs[2] = MyGrid.Instant.GetVertexY(quad.vertexC, y);
        vertexs[3] = MyGrid.Instant.GetVertexY(quad.vertexD, y);
        vertexs[4] = MyGrid.Instant.GetVertexY(quad.vertexA, y+1);
        vertexs[5] = MyGrid.Instant.GetVertexY(quad.vertexB, y+1);
        vertexs[6] = MyGrid.Instant.GetVertexY(quad.vertexC, y+1);
        vertexs[7] = MyGrid.Instant.GetVertexY(quad.vertexD, y+1);
    }

    public void UpdateBits()
    {
        bits = 0;
        for (int i = 0; i < 8; i++)
        {
            if (vertexs[i].isActive)
            {
                bits |= (1 << i);
            }
        }
    }

    public Vector3 GetCenterPos()
    {
        return quad.GetCenterPos() + Vector3.up * (y + 0.5f) * MyGrid.Instant.GetCellHeight();
    }

    public override string ToString()
    {
        return Convert.ToString(bits, 2).PadLeft(8, '0');
    }
}
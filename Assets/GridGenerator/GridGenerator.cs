using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


/// <summary>
/// 地图生成
/// </summary>
public class GridGenerator : MonoBehaviour
{
    [SerializeField]
    private int radius = 5;
    [SerializeField]
    private int cellSize = 1;
    private Grid grid;

    public int randomSeed = 10;

    private void Awake()
    {
        Random.InitState(randomSeed);
        grid = new Grid(radius,cellSize);
    }

    private void OnDrawGizmos()
    {
        if (grid != null)
        {                
            Gizmos.color = Color.white;

            //vertex
            foreach (var vertex in grid.hex_vertexes)
            {
                Gizmos.DrawSphere(vertex.GetWorldPos(),.1f);
            }
            
            Gizmos.color = Color.black;

            foreach (var quad in grid.quads)
            {
                Gizmos.DrawLine(quad.vertexHexA.GetWorldPos(),quad.vertexHexB.GetWorldPos());
                Gizmos.DrawLine(quad.vertexHexB.GetWorldPos(),quad.vertexHexC.GetWorldPos());
                Gizmos.DrawLine(quad.vertexHexC.GetWorldPos(),quad.vertexHexD.GetWorldPos());
                Gizmos.DrawLine(quad.vertexHexA.GetWorldPos(),quad.vertexHexD.GetWorldPos());
            }
            
            //triangle
            foreach (var triangle in grid.triangles)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(triangle.vertexHexA.GetWorldPos(),triangle.vertexHexB.GetWorldPos());
                Gizmos.DrawLine(triangle.vertexHexB.GetWorldPos(),triangle.vertexHexC.GetWorldPos());
                Gizmos.DrawLine(triangle.vertexHexA.GetWorldPos(),triangle.vertexHexC.GetWorldPos());
                //中心点
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere((triangle.vertexHexA.GetWorldPos()+triangle.vertexHexB.GetWorldPos()+triangle.vertexHexC.GetWorldPos())/3,.1f);
            }
            
            //triangle
            foreach (var subQuads in grid.subQuads)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(subQuads.vertexA.worldPos,subQuads.vertexB.worldPos);
                Gizmos.DrawLine(subQuads.vertexB.worldPos,subQuads.vertexC.worldPos);
                Gizmos.DrawLine(subQuads.vertexC.worldPos,subQuads.vertexD.worldPos);
                Gizmos.DrawLine(subQuads.vertexD.worldPos,subQuads.vertexA.worldPos);
            }


        }
      
    }
}

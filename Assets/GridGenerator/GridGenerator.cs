using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    private void Awake()
    {
        grid = new Grid(radius,cellSize);
    }

    private void OnDrawGizmos()
    {
        if (grid != null)
        {
            //triangle
            foreach (var triangle in grid.triangles)
            {
                Gizmos.DrawLine(triangle.a.GetWorldPos(),triangle.b.GetWorldPos());
                Gizmos.DrawLine(triangle.b.GetWorldPos(),triangle.c.GetWorldPos());
                Gizmos.DrawLine(triangle.a.GetWorldPos(),triangle.c.GetWorldPos());
                
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere((triangle.a.GetWorldPos()+triangle.b.GetWorldPos()+triangle.c.GetWorldPos())/3,.1f);

            }      
            
            //vertex
            foreach (var vertex in grid.vertexes)
            {
                Gizmos.DrawSphere(vertex.GetWorldPos(),.1f);
            }
        }
      
    }
}

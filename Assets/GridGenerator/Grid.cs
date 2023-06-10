using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地图
/// </summary>
public class Grid
{
   private int radius;
   private int cellSize = 1;
   public List<Vertex> vertexes ;
   public List<Triangle> triangles ;
   public Grid(int radius,int cellSize = 1)
   {
      this.radius = radius;
      this.cellSize = cellSize;
      vertexes =  Vertex.Hex(radius,cellSize);
      triangles = Triangle.TriangleHex(radius, vertexes);
   }
   
}

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
   public List<Vertex_Hex> vertexes ;
   public List<Edge> edges;
   public List<Triangle> triangles;
   public List<Quad> quads = new List<Quad>();
   public Grid(int radius,int cellSize = 1)
   {
      this.radius = radius;
      this.cellSize = cellSize;
      vertexes =  Vertex_Hex.Hex(radius,cellSize);
      triangles = Triangle.TriangleHex(radius, vertexes);
      MergeAllTriangles();
   }


   /// <summary>
   /// 将所有三角形合并成四边形  没有相邻三角形的不管
   /// </summary>
   private void MergeAllTriangles()
   {
      for (int i = 0; i < triangles.Count; i++)
      {
         //防止剩余三角形都在边界  需要从后往前遍历
         var temp = triangles[triangles.Count - i - 1];
         if (temp.FindAllNeighbors(triangles).Count != 0)
         {
            RandomlyMergeTriangle(temp);
            i--;
         }
      }
   }
   
   //合并四边形
   private void RandomlyMergeTriangle(Triangle triangle)
   {
      List<Triangle> neighbors = triangle.FindAllNeighbors(triangles);
      int randomNeighbor = Random.Range(0, neighbors.Count);
      Quad quad = triangle.MergeNeighborTriangle(neighbors[randomNeighbor]);
      quads.Add( quad);
      //移除三角形和被消除的边
      RemoveTriangle(triangle);
      RemoveTriangle(neighbors[randomNeighbor]);
      Edge.RemoveEdge(quad.vertexHexA,quad.vertexHexC);
   }

   /// <summary>
   /// 移除三角形
   /// </summary>
   /// <param name="triangle"></param>
   public void RemoveTriangle(Triangle triangle)
   {
      triangles.Remove(triangle);
   }
   
   
}

using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.UI;
using UnityEngine;

/// <summary>
/// 地图
/// </summary>
public class MyGrid
{
   public static MyGrid Instant;
   private int radius;
   private int cellSize;
   private int height ;
   private int cellHeight;
   public  Dictionary<int, Edge> edges = new Dictionary<int, Edge>();

   public List<Vertex> vertexs = new List<Vertex>();     //所有顶点
   
   public List<VertexHex> vertexes_hex ;  //六边形顶点
   public List<Triangle> triangles;
   public List<Quad> quads = new List<Quad>();
   //细分四边形
   public List<SubQuad> subQuads = new List<SubQuad>();
   public List<VertexY> vertexes_y = new List<VertexY>();
   public Dictionary<Vertex, VertexY[]> yMap = new Dictionary<Vertex, VertexY[]>();
   public List<SubQuadCueb> subQuadCuebs = new List<SubQuadCueb>();
   
   
   public MyGrid(int radius,int height,int cellSize ,int cellHeight )
   {
      Instant = this;
      this.radius = radius;
      this.cellSize = cellSize;
      this.height = height;
      this.cellHeight = cellHeight;
      vertexes_hex =  VertexHex.Hex(radius,cellSize);
      triangles = Triangle.TriangleHex(radius, vertexes_hex);
      MergeAllTriangles();
      SubDivide();
      InitVertexs();
      InitSubeQuadCubes();
   }

   public void InitVertexs()
   {
      foreach (var vertexHex in vertexes_hex)
      {
         vertexs.Add(vertexHex);
      }

      foreach (var edge in edges)
      {
         vertexs.Add(edge.Value.mid);
      }

      foreach (var triangle in triangles)
      {
         vertexs.Add(triangle.center);
      }
      foreach (var quad in quads)
      {
         vertexs.Add(quad.center);
      }
      
      for (int i = 0; i < height; i++)
      {
         for (int j = 0; j < vertexs.Count; j++)
         {
            var verY = new VertexY(vertexs[j], i);
            vertexes_y.Add(verY);
            if (!yMap.ContainsKey(vertexs[j]))
            {
               yMap.Add(vertexs[j],new VertexY[height]);
            }
            yMap[vertexs[j]][i] = verY;
         }
      }
   }
   

   public void InitSubeQuadCubes()
   {
      for (int i = 0; i < height-1; i++)
      {
         for (int j = 0; j < subQuads.Count; j++)
         {
            subQuadCuebs.Add(new SubQuadCueb(subQuads[j],i));
         }
      }
   }

   public VertexY GetVertexY(Vertex v, int y)
   {
      return yMap[v][y];
   }

   public int GetCellHeight()
   {
      return cellHeight;
   }

   //细分网格
   public void SubDivide()
   {
      foreach (var quad in quads)
      {
         subQuads.AddRange(quad.SubDivide());
      }
      foreach (var triangle in triangles)
      {
         subQuads.AddRange(triangle.SubDivide());
      }
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
      RemoveEdge(quad.vertexHexA,quad.vertexHexC);
   }

   /// <summary>
   /// 移除三角形
   /// </summary>
   /// <param name="triangle"></param>
   public void RemoveTriangle(Triangle triangle)
   {
      triangles.Remove(triangle);
   }

   public void SmoothGrid(int times, float smoothFactor)
   {
      for (int i = 0; i < times; i++)
      {
         //随机选择一个
         /*int index = Random.Range(0, subQuads.Count);
         subQuads[index].SmoothToCube(smoothFactor);*/
         //全体遍历一次
         foreach (var quad in subQuads)
         {
            quad.SmoothToCube(smoothFactor);  
         }
      }
   }
   
   
     
   public  Edge GetOrCreateEdge(VertexHex a, VertexHex b)
   {
      int uid =Edge.GetEdgeUID(a, b);
      if (!edges.ContainsKey(uid))
      {
         edges[uid] = new Edge(a, b);
      }
      return edges[uid];
   }
    
   public  Edge GetEdge(VertexHex a, VertexHex b)
   {
      int uid =Edge.GetEdgeUID(a, b);
      return edges[uid];
   }

   public void RemoveEdge(VertexHex a, VertexHex b)
   {
      int uid = Edge.GetEdgeUID(a, b);
      if (edges.ContainsKey(uid))
      {
         edges.Remove(uid);
      }
   }
   
}

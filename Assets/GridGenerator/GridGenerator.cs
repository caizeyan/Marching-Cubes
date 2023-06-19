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

    [SerializeField] private int cellHeight = 1;
    [SerializeField] private int height = 1;
    private Grid grid;

    public Transform activeTest;
    public Transform inactiveTest;

    public int smoothTimes = 0;
    public float smoothFactor = 0;
    public int randomSeed = 10;

    private void Awake()
    {
        Random.InitState(randomSeed);
        grid = new Grid(radius,height,cellSize,cellHeight);
        grid.SmoothGrid(smoothTimes,smoothFactor);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Random.InitState(randomSeed);
            grid = new Grid(radius,height,cellSize,cellHeight);
            grid.SmoothGrid(smoothTimes,smoothFactor);
        }

        foreach (var vertexY in grid.vertexes_y)
        {
            if (Vector3.Distance(vertexY.CurPos,activeTest.position)<2)
            {
                vertexY.isActive = true;
            }else if (Vector3.Distance(vertexY.CurPos,inactiveTest.position)<2)
            {
                vertexY.isActive = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (grid != null)
        {                
            Gizmos.color = Color.white;

            //vertex
            /*foreach (var vertex in grid.hex_vertexes)
            {
                Gizmos.DrawSphere(vertex.CurPos,.1f);
            }*/
            
            /*Gizmos.color = Color.black;

            foreach (var quad in grid.quads)
            {
                Gizmos.DrawLine(quad.vertexHexA.CurPos,quad.vertexHexB.CurPos);
                Gizmos.DrawLine(quad.vertexHexB.CurPos,quad.vertexHexC.CurPos);
                Gizmos.DrawLine(quad.vertexHexC.CurPos,quad.vertexHexD.CurPos);
                Gizmos.DrawLine(quad.vertexHexA.CurPos,quad.vertexHexD.CurPos);
            }
            
            //triangle
            foreach (var triangle in grid.triangles)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(triangle.vertexHexA.CurPos,triangle.vertexHexB.CurPos);
                Gizmos.DrawLine(triangle.vertexHexB.CurPos,triangle.vertexHexC.CurPos);
                Gizmos.DrawLine(triangle.vertexHexA.CurPos,triangle.vertexHexC.CurPos);
                //中心点
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere((triangle.vertexHexA.GetWorldPos()+triangle.vertexHexB.GetWorldPos()+triangle.vertexHexC.GetWorldPos())/3,.1f);
            }*/
            

            //subQuad
            /*foreach (var subQuads in grid.subQuads)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(subQuads.vertexA.CurPos,subQuads.vertexB.CurPos);
                Gizmos.DrawLine(subQuads.vertexB.CurPos,subQuads.vertexC.CurPos);
                Gizmos.DrawLine(subQuads.vertexC.CurPos,subQuads.vertexD.CurPos);
                Gizmos.DrawLine(subQuads.vertexD.CurPos,subQuads.vertexA.CurPos);
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(subQuads.vertexA.CurPos,.1f);
                Gizmos.DrawSphere(subQuads.vertexB.CurPos,.1f);
                Gizmos.DrawSphere(subQuads.vertexC.CurPos,.1f);
                Gizmos.DrawSphere(subQuads.vertexD.CurPos,.1f);
            }*/
            
            //
            foreach (var vertexY in grid.vertexes_y)
            {
                if (vertexY.isActive)
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.white;
                }
                Gizmos.DrawSphere(vertexY.CurPos,.1f);
            }
            

        }
      
    }
}

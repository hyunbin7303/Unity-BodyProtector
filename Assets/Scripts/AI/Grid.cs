﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform StartPosition;
    public LayerMask WallMask;

    public Vector2 gridWorldSize;
    public float nodeRadius;
    public float DistanceBetweenNodes;

    Node[,] grid;
    public List<Node> FinalPath;

    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;


    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);

                bool wall = true;
                if (Physics.CheckSphere(worldPoint, nodeRadius, WallMask))
                {
                    wall = false;
                }

                grid[x, y] = new Node(wall, worldPoint, x, y);
            }
        }
    }


    public Node NodeFromWorldPosition(Vector3 worldPosition)
    {
        float xpoint = ((worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float ypoint = ((worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y);

        xpoint = Mathf.Clamp01(xpoint);
        ypoint = Mathf.Clamp01(ypoint);

        int x = Mathf.RoundToInt((gridSizeX - 1) * xpoint);
        int y = Mathf.RoundToInt((gridSizeY - 1) * ypoint);

        return grid[x, y];
    }


    public List<Node> GetNeighboringNodes(Node node)
    {
        List<Node> neighboringNodes = new List<Node>();
        int xCheck;
        int yCheck;

        // Right side
        xCheck = node.gridX + 1;
        yCheck = node.gridY;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        // Left side
        xCheck = node.gridX - 1;
        yCheck = node.gridY;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        // Top side
        xCheck = node.gridX;
        yCheck = node.gridY + 1;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        // Bottom side
        xCheck = node.gridX;
        yCheck = node.gridY - 1;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        return neighboringNodes;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (grid != null)
        {
            foreach (Node node in grid)
            {
                if (node.IsWall)
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.yellow;
                }

                if (FinalPath != null)
                {
                    if (FinalPath.Contains(node))
                    {
                        Gizmos.color = Color.red;
                    }
                }

                Gizmos.DrawCube(node.Position, Vector3.one * (nodeDiameter - DistanceBetweenNodes));
            }
        }
    }
}

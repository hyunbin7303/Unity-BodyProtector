using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    // The grid
    public int gridX;
    public int gridY;

    public bool IsWall;
    public Vector3 Position;

    public Node Parent;

    public int gCost;
    public int hCost;

    public int FCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public Node(bool isWall, Vector3 pos, int gridX, int gridY)
    {
        this.IsWall = isWall;
        this.Position = pos;
        this.gridX = gridX;
        this.gridY = gridY;
    }
}

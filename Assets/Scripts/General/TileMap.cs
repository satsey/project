using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public void Init()
    {
        grid = new Tile[10, 10];
    }

    private Tile[,] grid;
}

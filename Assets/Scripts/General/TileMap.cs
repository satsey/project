using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class TileMap : MonoBehaviour
{
    public GameObject Tile;

    public int mapsize = 100;

    private int halfmapsize = 100 / 2;

    void Start()
    {
        grid = new GameObject[mapsize, mapsize];

        for (int i = 0; i < mapsize; i++)
        {
            for (int j = 0; j < mapsize; j++)
            {
                Instantiate(Tile, new Vector3(-halfmapsize + i * 2, 0, -halfmapsize + j * 2), Quaternion.identity);
            }
        }

        // recalculate graph
        GridGraph graphToScan = AstarPath.active.data.gridGraph;
        AstarPath.active.Scan(graphToScan);
    }

    private GameObject[,] grid;
}

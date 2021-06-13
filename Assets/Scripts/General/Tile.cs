using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public enum TowerType
{
    Laser,
    Fire
};

public class Tile : MonoBehaviour
{
    public GameObject LaserTower;
    public int color;
    public Vector3 tilePosition;

    public void SpawnTower(TowerType towerType)
    {
        if(towerType == TowerType.Laser)
        {
            Vector3 spawnpos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            Instantiate(LaserTower, spawnpos, Quaternion.identity);
            GridGraph graphToScan = AstarPath.active.data.gridGraph;
            AstarPath.active.Scan(graphToScan);
        }
    }

}

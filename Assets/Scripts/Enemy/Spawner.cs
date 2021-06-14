using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject enemy;
    public GameObject gamemanager;

    public Transform spawnlocation;
    public float spawnInterval = 3.5f;

    private float spawntime;
    // Start is called before the first frame update
    void Start()
    {
        spawntime = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        spawntime -= Time.deltaTime;
        if (spawntime < 0.1f)
        {
            Instantiate(enemy, spawnlocation.position, Quaternion.identity);
            spawntime = spawnInterval;
        }
    }
}

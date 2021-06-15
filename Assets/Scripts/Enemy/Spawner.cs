using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject enemy;
    public GameObject gamemanager;

    public Transform spawnlocation;
    public float spawnInterval = 3.5f;

    public float health = 10.0f;
    
    private float spawntime;

    private LightingManager lightingManager;
    // Start is called before the first frame update
    void Start()
    {
        spawntime = spawnInterval;
        lightingManager = gamemanager.GetComponent<LightingManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (lightingManager.TimeOfDay < 6.0f || lightingManager.TimeOfDay > 18.0f)
        {
            spawntime -= Time.deltaTime;
            if (spawntime < 0.1f)
            {
                Instantiate(enemy, spawnlocation.position, Quaternion.identity);
                spawntime = spawnInterval;
            }
        }
    }


    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0.0f)
        {
            Destroy(gameObject);
        }
    }


}

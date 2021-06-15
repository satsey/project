using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerBehaviour : MonoBehaviour
{
    float laserLength = 50f;
    int layerMask;
    int layerMask2;
    public Camera camera;
    public GameObject particle;
    public GameObject explosion;

    private Vector3 explodeposition;
    public GameObject Fence;
    //GameObject flashimage;
    //FlashImage flash;
    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Enemy");
        layerMask2 = LayerMask.GetMask("Building");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(Fence, transform.position + camera.transform.forward * 2 - camera.transform.up, Quaternion.identity);

            GridGraph graphToScan = AstarPath.active.data.gridGraph;
            AstarPath.active.Scan(graphToScan);
        }
        //Get the first object hit by the ray

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit[] hit = Physics.RaycastAll(camera.gameObject.transform.position, camera.transform.forward, laserLength, layerMask);

            foreach (var element in hit)
            {
                if (element.collider.gameObject.CompareTag("Enemy"))
                {

                    explodeposition = element.collider.gameObject.transform.position;

                    Instantiate(particle, explodeposition, Quaternion.identity);

                    Turtle enemy = element.collider.gameObject.GetComponent<Turtle>();
                    enemy.TakeDamage(1.0f);


                }




            }

            RaycastHit[] hit2 = Physics.RaycastAll(camera.gameObject.transform.position, camera.transform.forward, laserLength, layerMask2);
            foreach (var element in hit2)
            {
                if (element.collider.gameObject.CompareTag("Spawner"))
                {

                    explodeposition = element.collider.gameObject.transform.position;

                    Instantiate(explosion, explodeposition, Quaternion.identity);

                    Spawner enemy = element.collider.gameObject.GetComponent<Spawner>();
                    enemy.TakeDamage(1.0f);


                }
            }
        }




    }
}
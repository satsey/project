using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    float laserLength = 50f;
    int layerMask;
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {

        //Get the first object hit by the ray

        if (Input.GetKeyDown(KeyCode.A))
        {
            RaycastHit[] hit = Physics.RaycastAll(camera.gameObject.transform.position, camera.transform.forward, laserLength, layerMask);

            foreach (var element in hit)
            {
                element.collider.gameObject.CompareTag("Ground");
                Renderer renderer = element.collider.gameObject.GetComponent<Renderer>();
                renderer.material.color = Color.red;

                Tile tile = element.collider.gameObject.GetComponent<Tile>();
                if (tile != null)
                {
                    tile.SpawnTower(TowerType.Laser);
                }

                //Destroy(element.collider.gameObject);
                break;
            }

        }
    }
}

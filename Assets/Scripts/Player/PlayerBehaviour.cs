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
        layerMask = LayerMask.GetMask("Enemy");
    }

    // Update is called once per frame
    void Update()
    {

        //Get the first object hit by the ray

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit[] hit = Physics.RaycastAll(camera.gameObject.transform.position, camera.transform.forward, laserLength, layerMask);

            foreach (var element in hit)
            {
                element.collider.gameObject.CompareTag("Enemy");
                //Renderer renderer = element.collider.gameObject.GetComponent<Renderer>();
                //renderer.material.color = Color.red;

                Destroy(element.collider.gameObject);
                break;
            }

        }
    }
}

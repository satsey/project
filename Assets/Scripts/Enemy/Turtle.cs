using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    public GameObject player;
    private Animator anim;
    private float health = 5.0f;

    private float attackcooldown = 2.0f;

    private float currentcooldown = 0;
    private bool abletoattack = true;

    GameObject flashimage;
    FlashImage flash;

    private PlayerHealth playerHealth;
    public GameObject explodeParticle;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0.0f)
        {
            Destroy(gameObject);
            Instantiate(explodeParticle, transform.position, Quaternion.identity);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("IsAttacking", false);

        playerHealth = player.GetComponent<PlayerHealth>();
        flashimage = GameObject.FindGameObjectWithTag("Flash");
        //player = GameObject.FindGameObjectWithTag("Player");
        flash = flashimage.GetComponent<FlashImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 2.0f)
        {
            if (abletoattack)
            {
                anim.SetBool("IsAttacking", true);
                currentcooldown = attackcooldown;
                abletoattack = false;
              
                Invoke("StartFlash", 0.8f);

                Invoke("StopFlash", 1.8f);

                
            }
            else
            {
                currentcooldown -= Time.deltaTime;
                if (currentcooldown <= 0)
                {
                    abletoattack = true;
                }
            }

        }
        else
        {
            anim.SetBool("IsAttacking", false);
        }
    }

    private void StartFlash()
    {
        playerHealth.TakeDamage();
        flash.Flash(0.5f, 0.1f, 0.5f);
    }
    private void StopFlash()
    {
        flash.StopFlashLoop();
    }
}

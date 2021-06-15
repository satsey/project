using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 1295;
    public GameObject HealthBar;
    RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        rect = HealthBar.GetComponent<RectTransform>();
    }


    public void TakeDamage()
    {
        health -= 5;
        rect.sizeDelta = new Vector2(health, 45);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

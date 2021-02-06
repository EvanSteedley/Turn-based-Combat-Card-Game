using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Player p;
    [SerializeField]
    Turns t;
    [SerializeField]
    Text HealthValue;
    int health = 100;
    int damage = 10;
    public bool dead = false;
    [SerializeField]
    Slider SliderHealth;
    // Start is called before the first frame update
    void Start()
    {
        p = FindObjectOfType<Player>();
        SliderHealth.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!t.PlayerTurn && !dead)
        //    Attack();
        
    }

    public void TakeDamage(int d)
    {
        if (d >= health)
        {
            dead = true;
            Rigidbody rb = this.gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            rb.AddForce(new Vector3(500f, 400f, 0f));
            rb.AddTorque(new Vector3(5f, 50f, 35f));
        }
        health -= d;
        SliderHealth.value = health;
        HealthValue.text = health.ToString();
    }

    public void Attack()
    {
        p.TakeDamage(damage);
        //t.PlayerTurn = true;
    }
}

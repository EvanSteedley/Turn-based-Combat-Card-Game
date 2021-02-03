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
    int health = 100;
    int damage = 10;
    bool dead = false;
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
        if (!t.PlayerTurn && !dead)
            Attack();
        
    }

    public void TakeDamage(int d)
    {
        if (d >= health)
        {
            dead = true;
        }
        health -= d;
        SliderHealth.value = health;
    }

    public void Attack()
    {
        p.TakeDamage(damage);
        t.PlayerTurn = true;
    }
}

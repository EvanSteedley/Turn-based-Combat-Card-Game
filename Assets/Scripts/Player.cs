using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    int health = 100;
    int mana = 5;
    int damage = 20;
    [SerializeField]
    Enemy e;
    [SerializeField]
    Turns t;
    [SerializeField]
    Slider SliderHealth;
    [SerializeField]
    Text HealthValue;
    [SerializeField]
    Button AttackButton;

    // Start is called before the first frame update
    void Start()
    {
        e = FindObjectOfType<Enemy>();
        SliderHealth.value = health;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        if (t.PlayerTurn)
        {
            e.TakeDamage(damage);
            t.PlayerTurn = false;
        }

    }

    public void TakeDamage(int d)
    {
        health -= d;
        SliderHealth.value = health;
        HealthValue.text = health.ToString();
    }

    public void Buff()
    {
        if (t.PlayerTurn)
        { 
            damage += 30;
            t.PlayerTurn = false;
        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    int health = 100;
    public int startingMana = 5;
    public int mana;
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
    [SerializeField]
    Button EndTurnButton;
    [SerializeField]
    Text ManaValue;
    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        mana = startingMana;
        e = FindObjectOfType<Enemy>();
        SliderHealth.value = health;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AttackButtonPress()
    {
        StartCoroutine(Attack());
    }
    public IEnumerator Attack()
    {
        int manaCost = 1;
        if (t.PlayerTurn && !dead && mana >= manaCost)
        {
            mana -= manaCost;
            ManaValue.text = mana.ToString();
            //AttackButton.
            this.transform.Translate(new Vector3(1f, 0f, 0f));
            yield return StartCoroutine(t.EnemyDelay());
            this.transform.Translate(new Vector3(-1f, 0f, 0f));
            e.TakeDamage(damage);
            //StartCoroutine(t.EndPlayerTurn());
            int count = 0;
            foreach (Enemy e in t.enemies)
            {
                if (!e.dead)
                    count++;
            }
            if (count == 0)
                t.CamZoomOut();
        }
        yield return null;

    }

    public void TakeDamage(int d)
    {
        if (d >= health)
        {
            t.CamZoomOut();
            dead = true;
            Rigidbody rb = this.gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            rb.AddForce(new Vector3(-500f, 400f, 0f));
            rb.AddTorque(new Vector3(5f, 50f, 35f));
        }
        health -= d;
        SliderHealth.value = health;
        HealthValue.text = health.ToString();
    }

    public void BuffButtonPressed()
    {
        StartCoroutine(Buff());
    }

    public IEnumerator Buff()
    {
        int manaCost = 2;
        if (t.PlayerTurn && !dead && mana >= manaCost)
        {
            mana -= manaCost;
            ManaValue.text = mana.ToString();
            this.transform.Translate(new Vector3(1f, 0f, 0f));
            yield return StartCoroutine(t.EnemyDelay());
            this.transform.Translate(new Vector3(-1f, 0f, 0f));
            damage += 30;
            //StartCoroutine(t.EndPlayerTurn());
        }
    }

    public void EndTurnButtonPressed()
    {
        if (t.PlayerTurn && !dead)
            StartCoroutine(t.EndPlayerTurn());
    }

    public void StartTurn() 
    {
        mana = startingMana;
        ManaValue.text = mana.ToString();
    }

    
}

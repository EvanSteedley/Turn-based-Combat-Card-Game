using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCard : MonoBehaviour
{
    //Information about the cards
    public string cardName = "";
    public int value = 0;
    protected Player p;
    public int turns = 0;
    public Enemy e;
    
    public string description;
 
   

    //  Start is called before the first frame update
    void Start()
    {
        p = FindObjectOfType<Player>();
        e = GetComponent<Enemy>();
    }

 
    

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Action()
    {
        p.TakeDamage(value); 
       
    }

    public virtual void UpdateValues(int v, int turnsToLast)
    {
        value = v;
        turns = turnsToLast;
    }

    
}

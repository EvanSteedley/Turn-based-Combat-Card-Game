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
    
    
    public string description;
 
   

    //  Start is called before the first frame update
    void Start()
    {
        p = FindObjectOfType<Player>();

    }

 
    

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Action()
    {
        p.TakeDamage(value); 
       
    }

    
}

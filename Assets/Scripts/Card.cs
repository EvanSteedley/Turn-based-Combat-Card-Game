using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    //Information about the cards
    [SerializeField]
    public string cardName = "";
    [SerializeField]
    public int id = 0;
    [SerializeField]
    public int mana = 0;
    [SerializeField]
    public int damage = 0;


    //public Card(string n, int i, int m, int d)
    //{
    //    cardName = n;
    //    id = i;
    //    mana = m;
    //    damage = d;
    //}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /*public GameObject Selected
    {
        get { return this.selected; }
        set { this.selected = value; }
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}

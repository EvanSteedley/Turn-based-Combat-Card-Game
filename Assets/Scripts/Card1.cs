using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card1 : Card
{
    public Card1(string n, int i, int m, int d)
    {
        cardName = n;
        id = i;
        mana = m;
        damage = d;
    }
    // Start is called before the first frame update
    void Start()
    {
        Card1 thisCard = new Card1("Card1 Name", 1, 4, 25);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

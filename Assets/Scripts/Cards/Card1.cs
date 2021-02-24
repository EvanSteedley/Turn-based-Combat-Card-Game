using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card1 : Card
{
    //Here is an example of SerializeField potentially causing errors:  
    //You would think TestValue is whatever this number is, however since it was stored in the editor and isn't changed elsewhere,
    //It will not necessarily match this number.  (The ACTUAL TestValue number is 5, seen in the editor  It won't change even after hitting Play.)
   // [SerializeField]
    //int TestValue = 10;

    //"base" is C#'s equivalent of Java's "super"
    //public Card1(string n, int i, int m, int d) : base(n, i, m, d)
    //{

    //}

    // Start is called before the first frame update
    void Start()
    {
        //Card1 thisCard = new Card1("Card1 Name", 1, 4, 25);
        //This is already an "instance" of Card1 in Unity; there will be a copy of the Card1 script on each Card1.

        //"Instance" variables of the base class (Card) can still be accessed like so:
        cardName = "Card1";
        id = 1;
        mana = 4;
        damage = 25;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBuffCard : Card
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

    public List<Player> Targets;
    public Player p;

    // Start is called before the first frame update
    void Start()
    {
        //Card1 thisCard = new Card1("Card1 Name", 1, 4, 25);
        //This is already an "instance" of Card1 in Unity; there will be a copy of the Card1 script on each Card1.

        //"Instance" variables of the base class (Card) can still be accessed like so:
        id = 10000;
        mana = 2;
        value = 0;
        name = "";
        description = "";
        numberOfTargets = 1;
        Targeter = this.gameObject.GetComponent<SelectionGO>();
        Targeter.numberOfSelections = numberOfTargets;
        Targeter.exclusive = true;
        p = FindObjectOfType<Player>();
        SetInfo();
    }

    // Update is called once per frame
    void Update()
    {

    }

    override public void Action()
    {
        p.BuffDefense(value);
        Destroy(this.gameObject);
    }

    override public void HighlightTargets()
    {

    }

    override public void RemoveHighlightTargets()
    {

    }


    override public void ClearSelections()
    {
        Targeter.Selections = new List<GameObject>();
    }
}

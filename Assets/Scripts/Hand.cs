using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    Player p = FindObjectsOfType<Player>()[0];
    Card[] CurrentHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Card[] Draw(Card c)
    {
        CurrentHand[(CurrentHand.Length + 1)] = c;
        return CurrentHand;
    }
}

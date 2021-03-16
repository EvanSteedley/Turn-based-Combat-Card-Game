using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    Player p = FindObjectsOfType<Player>()[0];
    List<Card> CurrentHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Draw(List<Card> l, Card c)
    {
        l.Add(c);
    }
}

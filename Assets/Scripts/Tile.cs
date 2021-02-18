using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Card card;
    public int x, y;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CardPlayed(Card c)
    {
        card = c;
    }

    public void TileWalkedOn(GameObject o)
    {
        Enemy e = o.GetComponent<Enemy>();
        Player p = o.GetComponent<Player>();
        //if (e == null)
        //    p.TakeDamage(5);
        //Card.Action(o);
    }
}

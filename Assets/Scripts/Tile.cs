using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tile : MonoBehaviour
{
    private Card card;
    public bool occupied = false;
    public bool walkable = true;
    public int x, y;
    public string SceneToLoad;
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
        if(p != null)
        {
            if(SceneToLoad != "")
            {
                SceneManager.LoadScene(SceneToLoad);
            }
        }
        else if (e != null)
        {
            if (card != null)
            {
                card.Targeter.AddSelection(e.gameObject);
                card.Action();
            }
        }
    }

    public int F { get; set; }
    public int G { get; set; }
    public int H { get; set; }
    public Tile Parent { get; set; }

    }

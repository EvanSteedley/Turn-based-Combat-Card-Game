using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;



class Stun : StatusEffects
{

    //Turns t;
    void Start()
    {
        t = FindObjectOfType<Turns>();
        t.TurnEnded += Action;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Action(object sender, EventArgs e)
    {
        Enemy e2 = this.transform.GetComponent<Enemy>();
        Player p = this.transform.GetComponent<Player>();
        if (turnsLeft > 0)
        {
            if (p != null)
            {
                p.mana = 0;
                turnsLeft--;
            }

            if (e2!= null)
            {
                e2.isStunned = true; //adding stun effects for the enemy, had it for the player before
                turnsLeft--;

            }
        }
        else
        {
            t.TurnEnded -= Action;
            if (e2 != null)
            {
                e2.isStunned = false;
            }
            Destroy(this);
        }

        }
    }





    


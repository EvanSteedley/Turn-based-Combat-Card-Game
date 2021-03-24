using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


class Poison : StatusEffects
{

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void Action(object sender, EventArgs e)
    {

        Enemy e2 = this.transform.parent.GetComponent<Enemy>();
        Player p = this.transform.parent.GetComponent<Player>();

        if (p != null)
        {
            p.TakeDamage(20);

        }

        else if (e2 != null)
        {
            e2.TakeDamage(20);

        }

    }
}


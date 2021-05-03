using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class EnemyBuffDefense: EnemyCard
    {

        void Start()
        {
            value = 10;
            p = FindObjectOfType<Player>();
        e = GetComponent<Enemy>();
        cardName = "BuffDefense";
        }


        public override void Action()
        {
            e.BuffDefense(value);
        e.anim.SetTrigger("Power Up");
        }
    }


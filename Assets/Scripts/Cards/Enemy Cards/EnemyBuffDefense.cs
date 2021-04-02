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

        }


        public override void Action()
        {
            this.GetComponentInParent<Enemy>().BuffDefense(value);
        }
    }


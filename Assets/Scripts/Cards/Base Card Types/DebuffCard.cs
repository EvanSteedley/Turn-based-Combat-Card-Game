using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffCard : Card
{
    public GameObject ProjectilePrefab;
    public float timeToReach;


    // Start is called before the first frame update
    void Start()
    {
        id = 10000;
        value = 20;
        mana = 2;
        name = "";
        description = "";
        numberOfTargets = 1;
        Targeter = this.gameObject.GetComponent<SelectionGO>();
        Targeter.numberOfSelections = numberOfTargets;
        Targeter.exclusive = true;
        SetInfo();
    }

    override public void Action()
    {
        foreach (GameObject GO in Targeter.Selections)
        {
            Enemy e = GO.GetComponent<Enemy>();

            if (e != null)
            {
                LaunchProjectile(e.gameObject);
                //Replace <StatusEffects> with the actual name of the Status Effect to apply.
                e.gameObject.AddComponent<StatusEffects>();
            }
        }
        RemoveHighlightTargets();
        ClearSelections();
        Destroy(this.gameObject);

    }

    public virtual void LaunchProjectile(GameObject target)
    {
        if (ProjectilePrefab != null)
        {
            GameObject Projectile = Instantiate(ProjectilePrefab);
            Projectile.transform.position = FindObjectOfType<Player>().transform.position + Vector3.up;
            LerpTowardsTargets LTT = Projectile.GetComponent<LerpTowardsTargets>();
            LTT.Target = target;
            LTT.timeToMove = timeToReach;
        }
    }
}

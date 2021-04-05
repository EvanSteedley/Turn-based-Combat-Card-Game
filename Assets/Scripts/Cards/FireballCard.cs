using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCard : Card
{
    public List<Enemy> Targets;
    public GameObject FireballPrefab;
    public float timeToReach = 1f;


    // Start is called before the first frame update
    void Start()
    {
        id = 6;
        value = 50;
        mana = 2;
        name = "Fireball";
        description = "Shoot a 50 damage Fireball at one target.";
        numberOfTargets = 1;
        Targeter = this.gameObject.GetComponent<SelectionGO>();
        Targeter.numberOfSelections = numberOfTargets;
        Targeter.exclusive = true;
        SetInfo();
    }

    // Update is called once per frame
    void Update()
    {

    }

    override public void Action()
    {
        foreach (GameObject GO in Targeter.Selections)
        {
            GameObject Fireball = Instantiate(FireballPrefab);
            Fireball.transform.position = FindObjectOfType<Player>().transform.position + Vector3.up;
            LerpTowardsTargets LTT = Fireball.GetComponent<LerpTowardsTargets>();
            LTT.Target = GO;
            LTT.timeToMove = timeToReach;
            Enemy e = GO.GetComponent<Enemy>();
            if (e != null)
                e.TakeDamage(value);
        }
        RemoveHighlightTargets();
        ClearSelections();
        Destroy(this.gameObject);

    }

    override public void HighlightTargets()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy GO in enemies)
        {
            SelectableGO SGO = GO.GetComponentInParent<SelectableGO>();
            if (SGO != null)
            {
                SGO.enabled = true;
                if (SGO.ren == null)
                    SGO.ren = SGO.GetComponent<Renderer>();
                SGO.ren.material.color = Color.cyan;
                SGO.SGO = Targeter;
            }
        }
    }

    override public void RemoveHighlightTargets()
    {
        Enemy[] objects = FindObjectsOfType<Enemy>();
        foreach (Enemy GO in objects)
        {
            SelectableGO SGO = GO.GetComponent<SelectableGO>();
            if (SGO != null)
            {
                if (SGO.ren == null)
                    SGO.ren = SGO.GetComponent<Renderer>();
                SGO.ren.material.color = SGO.defaultColor;
                SGO.enabled = false;
            }
        }
    }
    override public void ClearSelections()
    {
        Targeter.Selections = new List<GameObject>();
    }
}

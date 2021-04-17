using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirOfHades : Card
{
    public List<Enemy> Targets;
    public GameObject WhiteSmoke;
    public float timeToReach = 1f;


    // Start is called before the first frame update
    void Start()
    {
        id = 17;
        value = 40;
        mana = 2;
        name = "Air of Hades";
        description = "Reduces Health by 40.";
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
            GameObject WhiteSmokeAttack = Instantiate(WhiteSmoke);
            WhiteSmokeAttack.transform.position = FindObjectOfType<Player>().transform.position + Vector3.up;
            LerpTowardsTargets LTT = WhiteSmokeAttack.GetComponent<LerpTowardsTargets>();
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



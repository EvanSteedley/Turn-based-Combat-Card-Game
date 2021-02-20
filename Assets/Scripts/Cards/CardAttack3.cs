using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAttack3 : Card
{
    public Enemy targetType;
    
    // Start is called before the first frame update
    void Start()
    {
        numberOfTargets = 3;
        Targeter = this.gameObject.AddComponent<SelectionGO>();
        Targeter.numberOfSelections = numberOfTargets;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public void Action()
    {
        foreach (GameObject GO in Targeter.Selections)
        {
            Enemy e = GO.GetComponent<Enemy>();
            if (e != null)
                e.TakeDamage(20);
        }
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
                SGO.s = Targeter;
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
                SGO.ren.material.color = SGO.defaultColor;
            }
        }
    }
}

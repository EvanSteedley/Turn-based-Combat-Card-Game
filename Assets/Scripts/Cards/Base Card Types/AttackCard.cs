using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCard : Card
{
    public GameObject ProjectilePrefab;
    public float timeToReach;
    public AudioClip SoundToPlay;



    // Start is called before the first frame update
    void Start()
    {
        id = 10000;
        value = 25;
        mana = 2;
        name = "";
        description = "";
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
            Enemy e = GO.GetComponent<Enemy>();
            if (e != null)
            {
                LaunchProjectile(e.gameObject);

                if(SoundToPlay != null)
                {
                    AudioSource source = FindObjectOfType<Player>().gameObject.AddComponent<AudioSource>();
                    source.clip = SoundToPlay;
                    source.volume = 0.1f;
                    source.Play();
                    Destroy(source, 10f);

                }

                e.TakeDamage(value);
            }
        }
        RemoveHighlightTargets();
        ClearSelections();
        this.gameObject.SetActive(false);
        Destroy(this.gameObject, 5f);

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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StunAura : PassiveItem
{
    public Turns t;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Tooltip.transform.position = Input.mousePosition + new Vector3(100f, -60f, 0);
    }

    private void sceneChange(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("Combat") || scene.name.Equals("Sample Combat") || scene.name.Substring(0, 4).Equals("Boss"))
        {
            t = FindObjectOfType<Turns>();
            t.CombatStarted += Effect;
        }
        else
        {

        }
    }

    private void Effect(object sender, EventArgs e)
    {
        foreach (Enemy enemy in t.enemies)
        {
            Stun s = enemy.gameObject.AddComponent<Stun>();
            s.UpdateValues(1, 1);
        }
    }

    public override void Activate()
    {
        SceneManager.sceneLoaded += sceneChange;
    }
}


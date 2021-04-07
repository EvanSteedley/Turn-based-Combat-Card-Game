using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PoisonAura : PassiveItem
{
    public Turns t;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += sceneChange;
    }

    private void sceneChange(Scene scene, LoadSceneMode mode)
    {
        if(scene.name.Equals("Combat") || scene.name.Equals("Sample Combat"))
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
            enemy.gameObject.AddComponent<Poison>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

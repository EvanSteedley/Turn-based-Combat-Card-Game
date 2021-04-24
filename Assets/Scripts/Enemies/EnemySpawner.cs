using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    public List<Enemy> AllEnemies = new List<Enemy>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Spawns new Enemies randomly
    /// </summary>
    /// <param name="n">Number of enemies to spawn</param>
    public List<Enemy> SpawnEnemies(int n)
    {
        List<Enemy> enemies = new List<Enemy>();
        for (int i = 0; i < n; i++)
        {
            enemies.Add(AllEnemies[UnityEngine.Random.Range(0, AllEnemies.Count)]);
        }
        return enemies;
    }
}

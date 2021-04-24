using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnsTile : MonoBehaviour
{
    //If true, then the player can take their turn.
    public bool PlayerTurn = true;
    [SerializeField]
    PlayerClickToMove p;
    //List of enemies in the current fight.
    [SerializeField]
    public List<Enemy> enemies = new List<Enemy>();
    //This controls how long the "animations" will last.
    [SerializeField]
    float delayBetweenTurns = .5f;
    //For changing the camera location when one side loses.
    GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = this.gameObject;
        //All enemies in the scene are added to the list of active enemies.
        //Enemy[] enemiesList = FindObjectsOfType<Enemy>();
        //foreach (Enemy e in enemiesList)
        //{
        //    enemies.Add(e);
        //}
        enemies = FindObjectOfType<TileMapGenerator>().enemies;
        p = FindObjectOfType<PlayerClickToMove>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator EndPlayerTurn()
    {
        PlayerTurn = false;
        //Coding transform changes is rough.
        ////p.transform.position.Set(p.transform.position.x + 1f, p.transform.position.y, p.transform.position.z);
        //p.transform.Translate(new Vector3(1f, 0f, 0f));
        //yield return StartCoroutine(EnemyDelay());
        //p.transform.Translate(new Vector3(-1f, 0f, 0f));
        //yield return StartCoroutine(EnemyDelay());
        //p.transform.position.Set(p.transform.position.x - 1f, p.transform.position.y, p.transform.position.z);
        int count = 0;
        foreach (Enemy e in enemies)
        {
            //Loops through all active enemies; if they aren't dead, then they attack the player and pause for the animation to play.
            if (!e.dead)
            {
                count++;
                //StartCoroutine(EnemyDelay());
                //StartCoroutine(EnemyDelay());
                //e.transform.Translate(new Vector3(-1f, 0f, 0f));
                //e.transform.position.Set(e.transform.position.x + 1f, e.transform.position.y, e.transform.position.z);
                //yield return StartCoroutine(EnemyDelay());
                //Invoke("e.Attack", delayBetweenTurns);
                //System.Threading.Thread.Sleep((int)(delayBetweenTurns * 1000));
                //e.EnemyBehaviour();
                //e.transform.Translate(new Vector3(1f, 0f, 0f));
                //e.transform.position.Set(e.transform.position.x - 1f, e.transform.position.y, e.transform.position.z);
                EnemyAI EAI = e.GetComponent<EnemyAI>();
                EAI.ResetMoves();
                StartCoroutine(EAI.AStar());
                yield return new WaitForSeconds(EAI.GetComponent<Movement>().timeToMove * EAI.movesDefault + 0.05f);
            }
        }
        //If there are enemies still alive and the player isn't dead, the Player can take their turn.
        if (count > 0)
        {
            PlayerTurn = true;
            p.StartTurn();
            p.EndTurnButton.interactable = true;
        }

        else
        {

        }

        yield return null;
    }

    //Counts the enemies still in the list, and not dead.
    public int EnemiesAlive()
    {
        int count = 0;
        foreach (Enemy e in enemies)
        {
            if (!e.dead)
                count++;
        }
        return count;
    }

}

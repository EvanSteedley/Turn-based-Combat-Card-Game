using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turns : MonoBehaviour
{
    public bool PlayerTurn = true;
    [SerializeField]
    Player p;
    [SerializeField]
    public List<Enemy> enemies = new List<Enemy>();
    [SerializeField]
    float delayBetweenTurns = .5f;
    GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = this.gameObject;
        Enemy[] enemiesList = FindObjectsOfType<Enemy>();
        foreach (Enemy e in enemiesList)
        {
            enemies.Add(e);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator EndPlayerTurn()
    {
        PlayerTurn = false;
        ////p.transform.position.Set(p.transform.position.x + 1f, p.transform.position.y, p.transform.position.z);
        //p.transform.Translate(new Vector3(1f, 0f, 0f));
        //yield return StartCoroutine(EnemyDelay());
        //p.transform.Translate(new Vector3(-1f, 0f, 0f));
        yield return StartCoroutine(EnemyDelay());
        //p.transform.position.Set(p.transform.position.x - 1f, p.transform.position.y, p.transform.position.z);
        int count = 0;
        foreach (Enemy e in enemies)
        {
            if (!e.dead)
            {
                count++;
                //StartCoroutine(EnemyDelay());
                //StartCoroutine(EnemyDelay());
                e.transform.Translate(new Vector3(-1f, 0f, 0f));
                //e.transform.position.Set(e.transform.position.x + 1f, e.transform.position.y, e.transform.position.z);
                yield return StartCoroutine(EnemyDelay());
                //Invoke("e.Attack", delayBetweenTurns);
                //System.Threading.Thread.Sleep((int)(delayBetweenTurns * 1000));
                e.Attack();
                e.transform.Translate(new Vector3(1f, 0f, 0f));
                //e.transform.position.Set(e.transform.position.x - 1f, e.transform.position.y, e.transform.position.z);
            }
        }
        if (count > 0)
        {
            PlayerTurn = true;
            p.StartTurn();
        }

        yield return null;
    }

    public IEnumerator EnemyDelay()
    {
        float initialTime = Time.time;
        //Debug.Log("Started EnemyDelay at timestamp : " + Time.time);
        while(Time.time - initialTime < delayBetweenTurns)
        {
            //Debug.Log(Time.time - initialTime);
            yield return null; 
        }
        //Debug.Log("Finished EnemyDelay at timestamp : " + Time.time);
    }

    public void CamZoomOut() 
    {
        //cam.transform.Translate(0, 6, -8);
        cam.transform.position = new Vector3(0, 9, -12);
    }

}

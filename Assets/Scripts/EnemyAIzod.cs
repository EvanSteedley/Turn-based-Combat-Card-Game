using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class EnemyAIzod : MonoBehaviour
{

    //Reference to the Player - will probably only need one of these.

    [SerializeField]
    Turns t;
    [SerializeField]
    Text EnemyDefenseValue;
    Movement movement;
    //Enemy's health; if <= 0, they die.
    int health = 100;
    //How much damage the Enemy will do on its turn
    int damage = 10;
    public bool dead = false;
    int defense = 0;
    Player player;
    Enemy e1;

    public int movesLeft = 2;
    int movesDefault = 2;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        //This is inefficient!  Calling the Attack through a Coroutine or by any other method is much better.
        //"Once per frame" is VERY OFTEN; around 60 times per second, usually.
        //if (!t.PlayerTurn && !dead)
        //    Attack();
    }



    public void EnemyMovement()
    {
        float x1, x2, y1, y2, x3, y3, xint, yint;
        float rx, ry, m1 = 0.0f, m2;
        float shortestdistsq, theta = 0.0f;
        float[] arr = { -1f, 1f };
        float randomDir = arr[Random.Range(0, 1)];

        float obstacleMinDist = 1.0f;
        float thetaIncrement = 5.0f;

        x1 = player.GetComponentInParent<Movement>().currentTile.transform.position.x;
        y1 = player.GetComponentInParent<Movement>().currentTile.transform.position.y;
        x2 = player.GetComponentInParent<Movement>().currentTile.transform.position.x;
        y2 = player.GetComponentInParent<Movement>().currentTile.transform.position.y;

        rx = x1 - x2;
        ry = y1 - y2;

        if (y1 == y2) theta = 0.0f;
        else if (x1 == x2) theta = 3.1415f * 0.5f;
        else m1 = ((y1 - y2) / (x1 - x2));

        List<Tile> Obstacles = FindObjectOfType<TileMapGenerator>().Obstacles;

        bool obstacleFlag = true;
        while (obstacleFlag)
        {
            obstacleFlag = false;
            foreach (Tile t in Obstacles)
            {

                x3 = t.transform.position.x;
                y3 = t.transform.position.y;

                if (theta == 0.0) //case of m1 equals 0
                {
                    xint = x3;
                    yint = y2;
                    shortestdistsq = (y3 - yint) * (y3 - yint);
                }

                else if (theta == 3.1415 * 0.5)  //case of m1 equals infinity
                {
                    xint = x2;
                    yint = y3;
                    shortestdistsq = (x3 - xint) * (x3 - xint);
                }

                else
                {
                    theta = Mathf.Atan(m1);
                    m2 = -1.0f / (m1);
                    xint = ((y3 - (m2 * x3)) - (y2 - (m1 * x2))) / (m1 - m2);
                    yint = (y2 + (m1 * (xint - x2)));
                    shortestdistsq = (((x3 - xint) * (x3 - xint)) + ((y3 - yint) * (y3 - yint)));
                }

                if (shortestdistsq <= obstacleMinDist * obstacleMinDist) ; //obstacleFlag = true;
            }

            if (obstacleFlag)
            {
                // choose a random number +1 or -1: randomDir = 
                theta = theta + (randomDir) * (thetaIncrement * 3.1415f / 180f);
                if ((theta != 0) && (theta != 3.1415 * 0.5)) m1 = Mathf.Tan(theta);
            }
        }



        // ----------------------------------------------------------
        // Determining the tile direction where the enemy moves next
        // ----------------------------------------------------------
        // CASE 1: if 0<=theta<45 or 315<theta<360: move RIGHT
        if ((theta >= 0 && theta < 0.25 * 3.1415) || (theta > 1.75 * 3.1415 && theta < 2 * 3.1415))
        {
            // MOVE RIGHT
            movement.MoveRight();
            movesLeft--;
        }

        // CASE 2: if 45<=theta<135: move UP
        else if (theta >= 0.25 * 3.1415 && theta < 0.75 * 3.1415)
        {
            // MOVE UP
            movement.MoveUp();
            movesLeft--;
        }

        // CASE 3: if 135<=theta<225: move LEFT
        else if (theta >= 0.75 * 3.1415 && theta < 1.25 * 3.1415)
        {
            // MOVE LEFT
            movement.MoveLeft();
            movesLeft--;
        }

        // CASE 4: if 225<=theta<315: move LEFT
        else if (theta >= 1.25 * 3.1415 && theta < 1.75 * 3.1415)
        {
            // MOVE DOWN
            movement.MoveDown();
            movesLeft--;
        }

    }

    //different points on the path, determine the earliest point from which the raycast hits the player 
    //the enemy will travel until that point and then change directions
    //change directions with the same process - choose the angle and then change position ^ the pseudocode above


    //---------------------------------------------
    // ATTACK WHEN PLAYER AND ENEMY ARE CLOSE ENOUGH
    // ---------------------------------------------
    //x1 = p.transform.getposition.x or p.gameObject.transform.position.x; //xcoordinates player
    //y1 = p.transform.getposition.y or p.gameObject.transform.position.y //ycoordinates
    //z1 = p.transform.getposition.z or p.gameObject.transform.position.z //zcoordinates

    //x2 = e.transform.getposition.x or p.gameObject.transform.position.x; //xcoordinates enemy
    //y2 = e.transform.getposition.y or p.gameObject.transform.position.y; //ycoordinates
    //z2 = e.transform.getposition.z or p.gameObject.transform.position.z; //xcoordinates

    //rmin = 10 
    //distsq = (x2-x1)*(x2-x1) + (y2-y1)*(y2-y1) + (z2-z1)*(z2-z1) //square of the distance from the enemy to the player
    // if distsq < ((rmin)*(rmin))  //compare with a minimum distance, if less than minimum distance, the enemy will attack
    //then call to attack function 
    //

    // ----------------------------------------------------
    // ATTACK WHEN PLAYER IS LOITERING AROUND A FIXED POINT
    // ----------------------------------------------------
    // 1. Extract player positions in the last 50 iterations (i.e. frames)
    // 2. Find the average position in these iterations: xAvg = (sum of x)/50, yAvg = (sum of y)/50, zAvg = (sum of z)/50
    // 3. Find standard deviation of (x,y,z) positions from (xAvg,yAvg,zAvg): 
    // stdDev = sqrt(sum( (x-xAvg)^2 + (y-yAvg)^2 + (z-zAvg)^2 ))
    // if stdDev < 5: then attack


    public void StartTurn()
    {
        movesLeft = movesDefault;
    }

}









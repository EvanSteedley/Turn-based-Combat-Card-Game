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
        float x1 = 0.0f, x2 = 0.0f, y1 = 0.0f, y2 = 0.0f, x3 = 0.0f, y3 = 0.0f, xint = 0.0f, yint = 0.0f;
        float rx = 0.0f, ry = 0.0f, m1 = 0.0f, m2 = 0.0f;
        float shortestdistsq = 10.0f, theta = 0.0f;
        float[] arr = { -1f, 1f };
        float randomDir = arr[Random.Range(0, 1)];

        float obstacleMinDist = 1.0f;
        float thetaIncrement = 5.0f;

        x1 = player.GetComponentInParent<Movement>().currentTile.transform.position.x;
        y1 = player.GetComponentInParent<Movement>().currentTile.transform.position.z;
        x2 = GetComponentInParent<Movement>().currentTile.transform.position.x;
        y2 = GetComponentInParent<Movement>().currentTile.transform.position.z;

        rx = x1 - x2;
        ry = y1 - y2;

        bool horizontalFlag = false;    // flag to indicate horizontal line-of-sight
        bool verticalFlag = false;      // flag to indicate vertical line-of-sight

        if (y1 == y2)
        {   // if line-of-sight is horizontal, set theta = 0 or 180 deg
            horizontalFlag = true;
            if (x1 >= x2) theta = 0.0f;
            if (x1 < x2) theta = 3.1415f;
        }
        else if (x1 == x2)
        {   // if line-of-sight is vertical, set theta = 90 or 270 deg
            verticalFlag = true;
            if (y1 >= y2) theta = 3.1415f * 0.5f;
            if (y1 < y2) theta = 3.1415f * 1.5f;
        }
        else if (horizontalFlag == false && verticalFlag == false)
        {
            m1 = ((y1 - y2) / (x1 - x2));          // else set slope of line-of-sight = (delta y)/(delta x)
            theta = Mathf.Atan(m1);                // calculate theta from arctan of slope
            // adjust value of theta between 0 and 360 degrees                
            if ((y1 > y2) && (x1 > x2))
                theta = theta;
            else if ((y1 > y2) && (x1 < x2))
                theta = 3.1415f + theta;
            else if ((y1 < y2) && (x1 < x2))
                theta = 3.1415f + theta;
            else if ((y1 < y2) && (x1 > x2))
                theta = 2f * 3.1415f + theta;
        }

        List<Tile> Obstacles = FindObjectOfType<TileMapGenerator>().Obstacles;

        bool obstacleFlag = true;
        while (obstacleFlag)
        {
            obstacleFlag = false;
            foreach (Tile t in Obstacles)
            {
                x3 = t.transform.position.x;
                y3 = t.transform.position.z;

                // CASE 1: line-of-sight is horizontal
                // shortest distance between obstacle and line-of-sight is (delta y)
                if (horizontalFlag)
                {
                    xint = x3;
                    yint = y2;
                    shortestdistsq = (y3 - yint) * (y3 - yint);
                }

                // CASE 2: line-of-sight is vertical
                // shortest distance between obstacle and line-of-sight is (delta x)
                else if (verticalFlag)
                {
                    xint = x2;
                    yint = y3;
                    shortestdistsq = (x3 - xint) * (x3 - xint);
                }

                // CASE 3: else
                // find perpendicular line from obstacle location to line-of-sight
                // find intersection point of perpendicular line with line-of-sight
                // set (delta x) = x_intersection - x_obstacle
                // set (delta x) = y_intersection - y_obstacle
                // shortest distance between obstacle and line-of-sight is SQRT[(delta y)^2 + (delta x)^2]
                else if (horizontalFlag == false && verticalFlag == false)
                {
                    m2 = -1.0f / (m1);
                    xint = ((y3 - (m2 * x3)) - (y2 - (m1 * x2))) / (m1 - m2);
                    yint = (y2 + (m1 * (xint - x2)));
                    shortestdistsq = (((x3 - xint) * (x3 - xint)) + ((y3 - yint) * (y3 - yint)));
                }

                // if shortest distance from obstacle to line-of-sight is less than the distance specified earlier
                // then set obstacle flag to TRUE to repeat while loop with a new path
                if (shortestdistsq <= obstacleMinDist * obstacleMinDist)
                {
                    //obstacleFlag = true;
                };

                Debug.Log(
                    "Shortest dist squared = " + shortestdistsq +
                    "\nObstacleMinDist squared = " + obstacleMinDist * obstacleMinDist +
                    "\n(x3, y3) = " + x3 + ", " + y3 +
                    "\n(x1, y1) = " + x1 + ", " + y1 +
                    "\n(x2, y2) = " + x2 + ", " + y2 +
                    "\ntheta = " + theta +
                    "\nm1 = " + m1 +
                    "\nm2 = " + m2
                    );

            }

            // if obstacle flag was TRUE (i.e. at least one obstacle blocks the line-of-sight),
            // then randomly increment or decrement angle of previous path by THETA 
            // check for horizontal or vertical paths
            // re-calculate slope m1 if required
            // and repeat the above process inside loop to determine if any obstacle blocks this new path
            if (obstacleFlag)
            {
                // reset horizontal and vertical path flags
                horizontalFlag = false;
                verticalFlag = false;

                theta = theta + (randomDir) * (thetaIncrement * 3.1415f / 180f);

                if ((theta == 0.0f) || (theta == 3.1415f))
                    horizontalFlag = true;
                else if ((theta == 3.1415f * 0.5f) || (theta == 3.1415f * 1.5f))
                    verticalFlag = true;
                else
                    m1 = Mathf.Tan(theta);

                // if theta deviates outside [0, 360) degrees range, adjust theta to return it inside the range
                if (theta >= 2f * 3.1415f) theta = theta - 2f * 3.1415f;
                if (theta < 0) theta = 2f * 3.1415f + theta;
            }
        }


        // ----------------------------------------------------------
        // Determining the tile direction where the enemy moves next
        // ----------------------------------------------------------
        // CASE 1: if 0<=theta<45 or 315<theta<360: move RIGHT
        if ((theta >= 0.0f && theta < 0.25f * 3.1415f) || (theta > 1.75f * 3.1415f && theta < 2f * 3.1415f))
        {
            // MOVE RIGHT
            movement.MoveDown();
            movesLeft--;
        }

        // CASE 2: if 45<=theta<135: move UP
        else if (theta >= 0.25f * 3.1415f && theta < 0.75f * 3.1415f)
        {
            // MOVE UP
            movement.MoveRight();
            movesLeft--;
        }

        // CASE 3: if 135<=theta<225: move LEFT
        else if (theta >= 0.75f * 3.1415f && theta < 1.25f * 3.1415f)
        {
            // MOVE LEFT
            movement.MoveUp();
            movesLeft--;
        }

        // CASE 4: if 225<=theta<315: move LEFT
        else if (theta >= 1.25f * 3.1415f && theta < 1.75f * 3.1415f)
        {
            // MOVE DOWN
            movement.MoveLeft();
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





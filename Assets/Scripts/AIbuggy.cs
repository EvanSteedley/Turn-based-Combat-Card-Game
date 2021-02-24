using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class AIbuggy : MonoBehaviour
{

    //Reference to the Player - will probably only need one of these.

    [SerializeField]
    Turns t;
    [SerializeField]
    Text EnemyAttackValue;
    [SerializeField]
    Text EnemyDefenseValue;
    //Enemy's health; if <= 0, they die.
    int health = 100;
    //How much damage the Enemy will do on its turn
    int damage = 10;
    public bool dead = false;
    int defense = 0;
    Player p1 = FindObjectOfType<Player>();
    Enemy e1 = FindObjectOfType<Enemy>();


    enum states
    {

        Fight, Defend, Buff

    }

    //UI elements.  Will need to be re-done if multiple Enemies.
    [SerializeField]
    Slider SliderHealth;
    [SerializeField]
    Text HealthValue;

    // Start is called before the first frame update
    void Start()
    {
        p1 = FindObjectOfType<Player>();
        SliderHealth.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        //This is inefficient!  Calling the Attack through a Coroutine or by any other method is much better.
        //"Once per frame" is VERY OFTEN; around 60 times per second, usually.
        //if (!t.PlayerTurn && !dead)
        //    Attack();

    }

    public void TakeDamage(int d)
    {
        if (d - defense >= health)

        {
            dead = true;
            //Ragdoll effect!
            Rigidbody rb = this.gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            rb.AddForce(new Vector3(500f, 400f, 0f));
            rb.AddTorque(new Vector3(5f, 50f, 35f));
        }
        if (defense < d)
            health -= (d - defense);
        defense = 0;
        if (health < 0)
        {
            health = 0;
        }
        SliderHealth.value = health;
        HealthValue.text = health.ToString();
    }

    public void Attack()
    {
        p1.TakeDamage(damage);
        //This is controlled by the Turns class now.
        //t.PlayerTurn = true;


    }

    public void EnemyMovement()
    {
        public float x1, x2, y1, y2, x3, y3, xint, yint;
    public float rx, ry, m1, m2;
    public float shortestdistsq, theta;

    float obstacleMinDist = 1.0;    // longest distance between obstacle and line-of-sight that blocks the line-of-sight
    float thetaIncrement = 5.0;     // angle increment to choose new path for enemy

    x1 = player.GetComponentInParent<Movement>().currentTile.transform.position.x;  // extract PLAYER x-coordinate
        y1 = player.GetComponentInParent<Movement>().currentTile.transform.position.y;  // extract PLAYER y-coordinate
        x2 = player.GetComponentInParent<Movement>().currentTile.transform.position.x;  // extract ENEMY x-coordinate
        y2 = player.GetComponentInParent<Movement>().currentTile.transform.position.y;  // extract ENEMY y-coordinate
       
        rx = x1-x2;
        ry = y1-y2;

        bool horizontalFlag = false;    // flag to indicate horizontal line-of-sight
        bool verticalFlag = false;      // flag to indicate vertical line-of-sight

        if (y1 == y2) {         // if line-of-sight is horizontal, set theta = 0 deg
            horizontalFlag = true;
            if (x1 >= x2) theta = 0.0;                  
            if (x1 < x2) theta = 3.1415;
        }
        else if (x1 == x2) {    // if line-of-sight is vertical, set theta = 90 deg
            verticalFlag = true;
            if (y1 >= y2) theta = 3.1415 * 0.5;
            if (y1 < y2) theta = 3.1415 * 1.5;
        }
        else {
            m1 = ((y1 - y2) / (x1 - x2));          // else set slope of line-of-sight = (delta y)/(delta x)
            theta = Math.Atan(m1);                 // calculate theta from arctan of slope
            // adjust value of theta between 0 and 360 degrees                
            if ((y1 > y2) && (x1 > x2))
                theta = theta;
            if ((y1 > y2) && (x1 < x2))
                theta = 3.1415 + theta;
            if ((y1 < y2) && (x1 < x2))
                theta = 3.1415 + theta;
            if ((y1 < y2) && (x1 > x2))
                theta = 2 * 3.1415 + theta;
        }

        public List<Tile> Obstacles;

        bool obstacleFlag = true;   // flag to indicate whether obstacle blocks player-enemy line-of-sight 

        // select a path and check for obstacles
        while (obstacleFlag = true) 
        {
            obstacleFlag = false;   // re-initiate flag

            // loop over list of obstacles to check if any of them blocks line-of-sight
            foreach (Tile t in Obstacles){  
        
                // extract coordinates of each obstacle
                x3 = t.transform.position.x;    
                y3 = t.transform.position.y;

                // CASE 1: line-of-sight is horizontal
                // shortest distance between obstacle and line-of-sight is (delta y)
                if (horizontalFlag == true) 
                {
                   xint = x3;
                   yint = y2;
                   shortestdistsq = (y3 - yint)*(y3-yint);
                }

                // CASE 2: line-of-sight is vertical
                // shortest distance between obstacle and line-of-sight is (delta x)
                else if (verticalFlag == true)
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
                else
                {
                    m2 = -1.0 / (m1);
                    xint = ((y3 - (m2 * x3)) - (y2 - (m1 * x2))) / (m1 - m2);
                    yint = (y2 + (m1 * (xint - x2)));
                    shortestdistsq = (((x3 - xint) * (x3 - xint)) + ((y3 - yint) * (y3 - yint)));
                }

                // if shortest distance from obstacle to line-of-sight is less than the distance specified earlier
                // then set obstacle flag to TRUE to repeat while loop with a new path
                if (shortestdistsq <= obstacleMinDist * obstacleMinDist) obstacleFlag = true;
            }

            // if obstacle flag was TRUE (i.e. at least one obstacle blocks the line-of-sight),
            // then randomly increment or decrement angle of previous path by THETA 
            // re-calculate slope m1 if required
            // and repeat the above process to determine if any obstacle blocks this new path
            if (obstacleFlag = true) {
        horizontalFlag == false;
        verticalFlag =
                // choose a random number +1 or -1: randomDir = 
                theta = theta + (randomDir) * (thetaIncrement * 3.1415 / 180);
                if ((theta == 0) || (theta == 3.1415 * 0.5)) horizontalFlag = true;
                if ((theta != 0) && (theta != 3.1415 * 0.5)) m1 = Math.Tan(theta);
            }
        }

        // Determining the tile where the enemy moves next
        // CASE 1: if 

    }

            //different points on the path, determine the earliest point from which the raycast hits the player 
            //the enemy will travel until that point and then change directions
            //change directions with the same process - choose the angle and then change position ^ the pseudocode above

  public void EnemyBehaviour()
{

    System.Random random = new System.Random();
    int num = random.Next(System.Enum.GetNames(typeof(states)).Length);
    if (num == (int)states.Fight)
    {
        Attack();
    }

    if (num == (int)states.Defend)
    {
        Defend(1);
    }

    if (num == (int)states.Buff)
    {
        Buff();
    }
}

---------------------------------------------
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


public void Defend(int d)
    {
        defense += 20;
        EnemyDefenseValue.text = defense.ToString();

    }

    public void Buff()
    {

        damage += 20;
        EnemyAttackValue.text = damage.ToString();
    }



}









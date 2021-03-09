using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Tutorial followed for the Move method, and these first 5 variables:
    // https://www.youtube.com/watch?v=AiZ4z4qKy44
    bool isMoving = false;
    public Tile currentTile, destinationTile;
    public Vector3 currentPos, destinationPos;
    public float timeToMove = 0.15f;
    Vector3 offset = new Vector3(0, 0, 0);

    TileMapGenerator tmg;
    //Vector3 up = Vector3.zero,
    //    right = new Vector3(0, 90, 0),
    //    down = new Vector3(0, 180, 0),
    //    left = new Vector3(0, 270, 0);
    //Vector3 currentDirection = Vector3.zero;
    //float speed = 5f;

    //Vector3 nextPos, destination, direction;
    // Start is called before the first frame update
    void Start()
    {
        tmg = FindObjectOfType<TileMapGenerator>();
        //currentDirection = up;
        //nextPos = Vector3.forward;
        //destination = transform.position;
        //originalPos = originalTile.transform.position;
        //destinationPos = destinationTile.transform.position;
        offset = new Vector3(0, this.transform.localScale.y, 0);
        //Debug.Log(offset);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator Move(Vector3 direction)
    {
        //transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        //transform.localEulerAngles = currentDirection;
        isMoving = true;

        float elapsedTime = 0f;
        currentPos = transform.position;
        //destinationPos = originalPos + direction;
        destinationPos = direction;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(currentPos, destinationPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        

        transform.position = destinationPos;
        currentTile.occupied = false;
        currentTile = destinationTile;
        currentTile.occupied = true;
        currentTile.TileWalkedOn(this.gameObject);
        isMoving = false;
    }

    //Actual Movement directions, adapted from the tutorial to work without User Input & with a 2D array of Tiles:
    public void MoveUp()
    {
        //If not already moving & moving up won't go off the grid;
        if(!isMoving && currentTile.x > 0)
        {
            //Set destinationTile to the next tile above the current one
            destinationTile = tmg.Tiles[currentTile.x - 1, currentTile.y];
            //Start the Move method Coroutine to move to that tile
            StartCoroutine(Move(destinationTile.transform.position + offset));
        }
    }
    public void MoveRight()
    {
        //If not already moving & moving Right won't go off the grid;
        if (!isMoving && currentTile.y < tmg.tileLength-1)
        {
            destinationTile = tmg.Tiles[currentTile.x, currentTile.y + 1];
            StartCoroutine(Move(destinationTile.transform.position + offset));
        }
    }
    public void MoveLeft()
    {
        //If not already moving & moving Left won't go off the grid;
        if (!isMoving && currentTile.y > 0)
        {
            destinationTile = tmg.Tiles[currentTile.x, currentTile.y - 1];
            StartCoroutine(Move(destinationTile.transform.position + offset));
        }
    }
    public void MoveDown()
    {
        //If not already moving & moving Down won't go off the grid;
        if (!isMoving && currentTile.x < tmg.tileWidth-1)
        {
            destinationTile = tmg.Tiles[currentTile.x + 1, currentTile.y];
            StartCoroutine(Move(destinationTile.transform.position + offset));
        }
    }

}

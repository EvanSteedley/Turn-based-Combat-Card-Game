using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    bool isMoving = false;
    public Tile currentTile, destinationTile;
    public Vector3 currentPos, destinationPos;
    float timeToMove = 0.15f;
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
        currentTile = destinationTile;
        isMoving = false;
    }

    public void MoveUp()
    {
        if(!isMoving && currentTile.x > 0)
        {
            //StartCoroutine(Move(Vector3.up));
            destinationTile = tmg.Tiles[currentTile.x - 1, currentTile.y];
            StartCoroutine(Move(destinationTile.transform.position + offset));
        }
    }
    public void MoveRight()
    {
        if (!isMoving && currentTile.y < tmg.tileLength-1)
        {
            destinationTile = tmg.Tiles[currentTile.x, currentTile.y + 1];
            StartCoroutine(Move(destinationTile.transform.position + offset));
        }
    }
    public void MoveLeft()
    {
        if (!isMoving && currentTile.y > 0)
        {
            destinationTile = tmg.Tiles[currentTile.x, currentTile.y - 1];
            StartCoroutine(Move(destinationTile.transform.position + offset));
        }
    }
    public void MoveDown()
    {
        if (!isMoving && currentTile.x < tmg.tileWidth-1)
        {
            destinationTile = tmg.Tiles[currentTile.x + 1, currentTile.y];
            StartCoroutine(Move(destinationTile.transform.position + offset));
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClickToMove : MonoBehaviour
{
    [SerializeField]
    int movesLeft = 3;
    [SerializeField]
    int movesDefault = 3;
    public Tile current;
    public Tile finalDestination;
    TileMapGenerator TMG;
    TileSelection TS;
    Movement PlayerMovement;
    // Start is called before the first frame update
    void Start()
    {
        TMG = FindObjectOfType<TileMapGenerator>();
        TS = FindObjectOfType<TileSelection>();
        PaintReachableTiles();
        PlayerMovement = GetComponentInParent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PaintReachableTiles() 
    {
        if(current == null)
            current = this.GetComponentInParent<Movement>().currentTile;
        foreach (Tile t in TMG.Tiles)
        {
            if (t != TS.Selected && (Mathf.Abs(current.x - t.x) + Mathf.Abs(current.y - t.y)) <= movesLeft)
            {
                TileSelectable tempTile = t.GetComponent<TileSelectable>();
                tempTile.ren.material.color = Color.cyan;
                tempTile.enabled = true;
            }
            else if (t != TS.Selected)
            {
                TileSelectable tempTile = t.GetComponent<TileSelectable>();
                tempTile.ren.material.color = tempTile.defaultColor;
                tempTile.enabled = false;
            }
        }
    }

    public void CalculateMove()
    {
        if (current != finalDestination && movesLeft > 0)
        {
            GameObject GO = new GameObject();
            GO.transform.position = current.transform.position;
            GO.transform.LookAt(finalDestination.transform);
            float angle = GO.transform.localEulerAngles.y;
            Destroy(GO);

            Debug.Log(angle);
            if (angle >= 225 && angle <= 315 && !TMG.Tiles[current.x - 1, current.y].occupied)
            {
                PlayerMovement.MoveUp();
                movesLeft--;
                current = PlayerMovement.destinationTile;
            }
            else if (angle >= 135 && angle < 225 && !TMG.Tiles[current.x, current.y - 1].occupied)
            {
                PlayerMovement.MoveLeft();
                movesLeft--;
                current = PlayerMovement.destinationTile;
            }
            else if (angle >= 45 && angle < 135 && !TMG.Tiles[current.x + 1, current.y].occupied)
            {
                PlayerMovement.MoveDown();
                movesLeft--;
                current = PlayerMovement.destinationTile;
            }
            else if (!TMG.Tiles[current.x, current.y + 1].occupied)
            {
                PlayerMovement.MoveRight();
                movesLeft--;
                current = PlayerMovement.destinationTile;
            }
            
            
            PaintReachableTiles();
        }
    }

    public void MoveToTile() 
    {
        //StartCoroutine(CalculateMove());
    }

    public void EndTurn()
    {
        movesLeft = movesDefault;
        PaintReachableTiles();
    }
}

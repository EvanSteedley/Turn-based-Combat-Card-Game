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
            current = this.GetComponentInParent<Movement>().originalTile;
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
            if (angle >= 225 && angle <= 315)
                PlayerMovement.MoveUp();
            else if (angle >= 135 && angle < 225)
                PlayerMovement.MoveLeft();
            else if (angle >= 45 && angle < 135)
                PlayerMovement.MoveDown();
            else
                PlayerMovement.MoveRight();

            movesLeft--;
            current = PlayerMovement.destinationTile;
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

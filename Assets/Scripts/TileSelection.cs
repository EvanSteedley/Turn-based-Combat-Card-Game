using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TileSelection : MonoBehaviour
{
    public Tile selected;
    public bool somethingSelected = false;
    [SerializeField]
    public Button MoveButton;

    public Tile Selected
    {
        get { return this.selected; }
        set { this.selected = value; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{
    public GameObject selected;
    public bool somethingSelected = false;
    public InputField IFX;
    public InputField IFZ;
    public InputField IFY;
    public Text SelectionName;

    public GameObject Selected
    {
        get { return this.selected; }
        set { this.selected = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        IFX.gameObject.SetActive(false);
        IFY.gameObject.SetActive(false);
        IFZ.gameObject.SetActive(false);
        //IFY.onEndEdit.AddListener(YSelectionUpdated);
        //IFX.onEndEdit.AddListener(XSelectionUpdated);
        //IFZ.onEndEdit.AddListener(ZSelectionUpdated);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void YSelectionUpdated(string s)
    {
        selected.transform.localScale += new Vector3(0, float.Parse(IFY.text), 0);
    }
    public void XSelectionUpdated(string s)
    {
        selected.transform.localScale += new Vector3(float.Parse(IFX.text), 0, 0);
    }
    public void ZSelectionUpdated(string s)
    {
        selected.transform.localScale += new Vector3(0, 0, float.Parse(IFZ.text));
    }

}

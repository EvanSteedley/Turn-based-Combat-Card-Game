using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class PassiveItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject Tooltip;
    public Transform par;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            Tooltip.transform.position.Set(Input.mousePosition.x + 300f, Input.mousePosition.y -60f, Input.mousePosition.z);
            Debug.Log("Tooltip pos: " + Tooltip.transform.position);
            Debug.Log("Mouse pos: " + Input.mousePosition);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip.SetActive(true);
        par = Tooltip.transform.parent;
        Tooltip.transform.SetParent(FindObjectsOfType<Canvas>().Last<Canvas>().transform);
        Tooltip.transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.SetActive(false);
        Tooltip.transform.SetParent(par);
    }

    public virtual void Activate()
    { }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckGUI : MonoBehaviour
{
    public Deck Deck;
    public GameObject CardHolder;
    public List<Card> InstantiatedCards = new List<Card>();
    public List<Button> Buttons = new List<Button>();
    public Button DestroyButton;

    // Start is called before the first frame update
    void Awake()
    {
        Deck = FindObjectOfType<Deck>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Scale = *100
    //300 X between each Card
    //600 Y between each Card
    //Up to 6 on each Row
    public void PopulateCardHolder()
    {
        for (int i  = 0; i < Deck.FullDeck.Count; i++)
        {
            Card added = Instantiate(Deck.FullDeck[i]);
            added.transform.parent = CardHolder.transform;
            InstantiatedCards.Add(added);
            added.gameObject.transform.localScale = new Vector3(1000, 10000, 15000);
            added.GetComponent<Collider>().enabled = false;
            added.GetComponentInChildren<Canvas>().overrideSorting = true;
            added.transform.eulerAngles = new Vector3(-90, 0, -90);
        }
        int startx = -800;
        int starty = 350;
        float sumy = 0;
        float center = 0f;
        for(int i = 0; i < InstantiatedCards.Count; i++)
        {
            InstantiatedCards[i].gameObject.transform.localPosition = new Vector3(startx, starty, 0);
            startx += 300;
            sumy += starty;
            center += InstantiatedCards[i].transform.localPosition.y;
            if ((i+1) % 6 == 0)
            {
                startx = -800;
                starty -= 600;
            }
            InstantiatedCards[i].gameObject.transform.parent = null;
        }
        sumy /= InstantiatedCards.Count;
        center /= 21;
        RectTransform RT = CardHolder.GetComponent<RectTransform>();
        RT.sizeDelta = new Vector2(RT.rect.width, (InstantiatedCards.Count / 6) * 600);
        RT.anchoredPosition = new Vector2(0, center);
        for(int i = 0; i < InstantiatedCards.Count; i++)
        {
            InstantiatedCards[i].gameObject.transform.parent = CardHolder.transform;
            Button destroy = Instantiate(DestroyButton, this.transform);
            Buttons.Add(destroy);
            int index = i;
            destroy.onClick.AddListener(() =>
            {
                DestroyCard(index);
            });
            //destroy.GetComponent<RectTransform>().anchoredPosition = new Vector2(InstantiatedCards[i].transform.localPosition.x, InstantiatedCards[i].transform.localPosition.y - 300);
            destroy.transform.SetParent(InstantiatedCards[i].transform);
            destroy.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, -0.02f);
        }
    }

    public void DestroyCard(int i)
    {
        if(i >= 0 && i < InstantiatedCards.Count)
        {
            Card removed = InstantiatedCards[i];
            Deck.RemoveCard(removed);
            InstantiatedCards.Remove(removed);
            Destroy(removed.gameObject);
            int c = Buttons.Count;
            for(int k = 0; k < c; k++)
            {
                Buttons[0].transform.SetParent(null);
                Button r = Buttons[0];
                Buttons.RemoveAt(0);
                Destroy(r.gameObject);
            }
            ResetPositions();
        }
    }

    public void ResetPositions()
    {
        int startx = -800;
        int starty = 350;
        float sumy = 0;
        float center = 0f;
        for (int i = 0; i < InstantiatedCards.Count; i++)
        {
            InstantiatedCards[i].gameObject.transform.localPosition = new Vector3(startx, starty, 0);
            startx += 300;
            sumy += starty;
            center += InstantiatedCards[i].transform.localPosition.y;
            if ((i + 1) % 6 == 0)
            {
                startx = -800;
                starty -= 600;
            }
            InstantiatedCards[i].gameObject.transform.parent = null;
        }
        sumy /= InstantiatedCards.Count;
        center /= 21;
        RectTransform RT = CardHolder.GetComponent<RectTransform>();
        RT.sizeDelta = new Vector2(RT.rect.width, (InstantiatedCards.Count / 6) * 600);
        RT.anchoredPosition = new Vector2(0, center);
        for (int i = 0; i < InstantiatedCards.Count; i++)
        {
            InstantiatedCards[i].gameObject.transform.parent = CardHolder.transform;
            Button destroy = Instantiate(DestroyButton, this.transform);
            Buttons.Add(destroy);
            int index = i;
            destroy.onClick.AddListener(() =>
            {
                DestroyCard(index);
            });
            //destroy.GetComponent<RectTransform>().anchoredPosition = new Vector2(InstantiatedCards[i].transform.localPosition.x, InstantiatedCards[i].transform.localPosition.y - 300);
            destroy.transform.SetParent(InstantiatedCards[i].transform);
            destroy.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, -0.02f);
        }
    }
}

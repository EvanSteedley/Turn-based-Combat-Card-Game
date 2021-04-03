using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionBottle :MonoBehaviour

{
    GameObject gameObject;

   
    public float sec = 5f;

    
    
    void Start()
    {

        StartCoroutine(RemoveAfterSeconds(5, gameObject));
    }

    IEnumerator RemoveAfterSeconds(int seconds, GameObject obj)
    {
        yield return new WaitForSeconds(2);
        obj.SetActive(false);
    }


public void MoveCork()
{
        GameObject cork = gameObject; 
 
  cork.transform.Translate(0, 1 * Time.deltaTime, 0);

}
// Update is called once per frame
void Update()
    {
        print("swag");
        
    }
}

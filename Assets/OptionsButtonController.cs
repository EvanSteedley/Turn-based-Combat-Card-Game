using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsButtonController : MonoBehaviour
{
    public void SelectButton()
    {
        StartCoroutine("SelectPlayer");


    }
    public void ReturnToMenu()

    {

        StartCoroutine("GoToMenu");
    }

    private IEnumerator SelectPlayer()
    {
        yield return new WaitForSeconds(.05f);
        SceneManager.LoadScene("Sample Combat");
    }

    
    private IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(.05f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame()
    {
        StartCoroutine(GetMyRoutine());
        
        
    }

    private IEnumerator GetMyRoutine()
    {
        yield return new WaitForSeconds(.05f);
        SceneManager.LoadScene("PlayerSelection");
    }

    //Quits game 
    public void QuitGame()
    {
        Application.Quit();
    }
}

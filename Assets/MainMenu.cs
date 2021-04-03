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
        SceneManager.LoadScene("Sample Combat");
    }

    // Update is called once per frame
    public void QuitGame()
    {
        Application.Quit();
    }
}

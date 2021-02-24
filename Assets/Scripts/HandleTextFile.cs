//Code modified from https://support.unity.com/hc/en-us/articles/115000341143-How-do-I-read-and-write-data-from-a-text-file-

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using System.Linq;
using System;

public class HandleTextFile : MonoBehaviour
{
    public Transform contentWindow;
    public GameObject recallTextObject;

    /*  static void ReadString()
      {


          //Read the text from directly from the test.txt file
          /*  StreamReader reader = new StreamReader(path); 
            Debug.Log(reader.ReadToEnd());
            reader.Close();
          
}
   */


    void Start() 
    {
        //getting the text file from the directory within Unity and giving it a string value. 
        string path = Application.streamingAssetsPath + "/Resources" + "CardList" + ".txt.";
        //reads every single line withing CardList.txt and putting it in a list. 
        List<string> fileLines = File.ReadAllLines(path).ToList();

        foreach (string line in fileLines) {
            Instantiate(recallTextObject, contentWindow);
            recallTextObject.GetComponent<Text>().text = line;
        }


        /*static void Instantiate(GameObject recallTextObject, Transform contentWindow)
        {
            throw new NotImplementedException();
        }*/
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SetVolume : MonoBehaviour
{
   public AudioMixer audioMixer;
   // Start is called before the first frame update
   public void SetVol(float slideValue)
   {
        //setting audioMixer to a logorimithic value which will take .0001 -1 slide value and turn it into a value
        //-80 and 0 on a log scale. 
        audioMixer.SetFloat("MusicMixer", Mathf.Log10(slideValue) * 20);
    }

}

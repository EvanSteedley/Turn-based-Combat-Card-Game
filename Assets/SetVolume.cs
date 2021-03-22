using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SetVolume : MonoBehaviour
{
//declare the audio mixer 
   public AudioMixer audioMixer;
 
   public void SetVol(float slideValue)
   {
        /*SetFloat takes name and the value,take .0001 -1 slide value and turn it into a value
        btween   -80 and 0 on a log scale.
        represents the slider value as a log to the base of 10 and multiply by factor of 20
    */
        audioMixer.SetFloat("MusicMixer", Mathf.Log10(slideValue) * 20);
    }

}

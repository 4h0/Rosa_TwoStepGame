using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundVolumeUpdate_Khoa : MonoBehaviour
{
    public AudioSource[] audioSound;



    public void VolumeChange(float soundVolume)
    {
        foreach (AudioSource tempAudio in audioSound)
        {
            tempAudio.volume = soundVolume;
        }
    }
}

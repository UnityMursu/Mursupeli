using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolumeMaster (float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
        audioMixer.SetFloat("MusicVolume", volume);
    }

       public void SetVolumeMusic (float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }
}

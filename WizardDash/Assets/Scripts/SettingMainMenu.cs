using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMainMenu : MonoBehaviour
{
    //[SerializeField] private AudioMixer MyMixer;
    //[SerializeField] private Slider MusicSlider;

    //private void Start()
    //{
    //    if(PlayerPrefs.HasKey("musicVolume"))
    //    {
    //        LoadVolume();
    //    } else
    //    {
    //        SetVolume();
    //    }
    //}

    //public void SetVolume()
    //{
    //    float volume = MusicSlider.value;
    //    MyMixer.SetFloat("volume", volume);
    //}

    //public void LoadVolume()
    //{
    //    MusicSlider.value = PlayerPrefs.GetFloat("musicVolume");
    //    SetVolume();
    //}

    public AudioMixer audioMixer;
    public Slider volumeSlider;

    public void SetVolume(float volume)
    {
        //audioMixer.SetFloat("volume", volumeSlider.value);
        Debug.Log(volume);
    }
}

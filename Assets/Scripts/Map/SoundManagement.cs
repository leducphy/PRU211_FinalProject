using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManagement : MonoBehaviour

{
    [SerializeField] Slider volumeSlider;
    [SerializeField] Image OffIcon;
    [SerializeField] Image OnIcon;
    private bool muted = false;

    private AudioSource source;
    public static SoundManagement instance { get; private set; }

    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();   
    }
    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }


    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            Load();
        }
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            LoadMute();
        }
        if (PlayerPrefs.HasKey("muted"))
        {

            LoadMute();
        }

        AudioListener.pause = muted;
    }

    // Update is called once per frame
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    public void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");

    }
    public void Save()
    {
        PlayerPrefs.GetFloat("musicVolume", volumeSlider.value); ;
    }

    public void OnbuttonPress()
    {
        if (muted == false)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }
        SaveMute();
    }



    private void LoadMute()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }
    private void SaveMute()
    {
        PlayerPrefs.GetInt("muted", muted ? 1 : 0);
    }

}

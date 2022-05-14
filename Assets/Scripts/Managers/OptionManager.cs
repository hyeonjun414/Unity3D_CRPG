using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionManager : Singleton<OptionManager>
{
    public GameObject optionUI;
    public Slider effectVolume;
    public Slider musicVolume;

    public AudioMixer mixer;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    private void Start()
    {
        float effect = PlayerPrefs.GetFloat("Effect");
        float bgm = PlayerPrefs.GetFloat("BGM");

        mixer.SetFloat("Effect", effect);
        mixer.SetFloat("BGM", bgm);
        effectVolume.value = effect;
        musicVolume.value = bgm;
    }

    public void OptionBtn()
    {
        optionUI.SetActive(!optionUI.activeSelf);
    }

    public void SetEffectVolume(float value)
    {
        float sound = effectVolume.value;

        if (sound == -40f) mixer.SetFloat("Effect", -80);
        else mixer.SetFloat("Effect", sound);

        PlayerPrefs.SetFloat("Effect", sound);
    }
    public void SetBGMVolume(float value)
    {
        float sound = musicVolume.value;

        if (sound == -40f) mixer.SetFloat("BGM", -80);
        else mixer.SetFloat("BGM", sound);

        PlayerPrefs.SetFloat("BGM", sound);
    }


}

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
        // 저장된 설정값을 불러와 볼륨 믹서와 슬라이더를 설정해준다.
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
        GameManager.Instance.IsPause = optionUI.activeSelf;
    }

    // 효과음 볼륨 조절 -> 효과음 슬라이더에 부착
    public void SetEffectVolume(float value)
    {
        float sound = effectVolume.value;

        if (sound == -40f) mixer.SetFloat("Effect", -80);
        else mixer.SetFloat("Effect", sound);

        PlayerPrefs.SetFloat("Effect", sound);
    }
    // 배경음 볼륨 조절 -> 배경음 슬라이더에 부착
    public void SetBGMVolume(float value)
    {
        float sound = musicVolume.value;

        if (sound == -40f) mixer.SetFloat("BGM", -80);
        else mixer.SetFloat("BGM", sound);

        PlayerPrefs.SetFloat("BGM", sound);
    }


}

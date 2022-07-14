using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    public List<AudioClip> effectSounds;
    public AudioSource BGM;
    public AudioSource effectSfxPlayer;


    public Slider effectVolume;
    public Slider musicVolume;

    public AudioMixer mixer;

    private void Start()
    {
        float effect = PlayerPrefs.GetFloat("Effect");
        float bgm = PlayerPrefs.GetFloat("BGM");

        mixer.SetFloat("Effect", effect);
        mixer.SetFloat("BGM", bgm);
        effectVolume.value = effect;
        musicVolume.value = bgm;
    }
    public void InGameStart()
    {
        StartCoroutine("FadeOut");
        Invoke("SceneChange", 1f);
    }
    public void ExitBtn()
    {
        StartCoroutine("FadeOut");
        Invoke("GameExit", 1f);

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
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public void PlayEffectSound(AudioClip sfx)
    {
        effectSfxPlayer.PlayOneShot(sfx);
    }

    public void PlayBGMSound(AudioClip sfx)
    {
        BGM.clip = sfx;
        BGM.loop = true;
        BGM.Play();
    }

    public void AddEffectSfx(AudioClip sfx)
    {
        if (effectSounds.Contains(sfx))
        {
            return;
        }
        else
        {
            effectSounds.Add(sfx);
        }
    }
    public void EraseEffectSfx(AudioClip sfx)
    {
        int index = effectSounds.FindIndex(item => item.name == sfx.name);
        if (index == -1) return;
        effectSounds.RemoveAt(index);
    }
}

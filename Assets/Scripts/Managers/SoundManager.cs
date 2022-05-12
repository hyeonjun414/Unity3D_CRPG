using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public List<AudioClip> effectSounds;
    public AudioSource BGM;
    public AudioSource effectSfxPlayer;
    private void Awake()
    {
        _instance = this;
    }

    public void PlayEffectSound(AudioClip sfx)
    {
        //int idx = effectSounds.FindIndex(item => item.name == sfx.name);
        //effectSfxPlayer.PlayOneShot(effectSounds[idx]);
        effectSfxPlayer.PlayOneShot(sfx);
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

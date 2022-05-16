using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkill : Skill
{
    [SerializeField]
    private AudioClip soundClip;
    private void Start()
    {
        soundClip = GetComponent<AudioSource>().clip;
        SoundManager.Instance.AddEffectSfx(soundClip);
        SoundManager.Instance.PlayEffectSound(soundClip);

    }
    private void OnDestroy()
    {
        SoundManager.Instance.EraseEffectSfx(soundClip);
    }

   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "Data/Sound")]
public class SoundData : ScriptableObject
{
    public AudioClip EnemyBGM;
    public AudioClip ShopBGM;
    public AudioClip EventBGM;
    public AudioClip RestBGM;

}

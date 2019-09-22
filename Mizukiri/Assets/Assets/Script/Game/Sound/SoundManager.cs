using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundEffect
{
    Start,
    Bound,
    Boost,
}

public class SoundManager : MonoBehaviour {

    public AudioClip[] soundEffects;


    public AudioClip SetSoundEffect(SoundEffect type)
    {
        return soundEffects[(int)type];
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;
    public static GameAssets i
    {
        get
        {
            if (_i = null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));

            return _i;
        }
    }

    public SoundAudioClip[] soundAudioClipArray;

    [Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip clip;
    }

    public LoopAudioClip[] loopAudioClipArray;

    [Serializable]
    public class LoopAudioClip
    {
        public SoundManager.Loop loop;
        public AudioClip clip;
    }
}

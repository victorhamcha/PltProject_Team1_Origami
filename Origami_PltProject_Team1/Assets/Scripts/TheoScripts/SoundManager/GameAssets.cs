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
            if (_i == null)
            {
                _i = FindObjectOfType<GameAssets>();
                if (_i == null)
                {
                    _i = new GameObject("GameAssets", typeof(GameAssets)).GetComponent<GameAssets>();
                }
            }

            return _i;
        }

        private set
        {
            _i = value;
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

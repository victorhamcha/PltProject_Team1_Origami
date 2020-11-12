using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SoundManager : MonoBehaviour
{

    private static SoundManager _i;
    public static SoundManager i
    {
        get
        {
            if (_i == null)
            {
                _i = FindObjectOfType<SoundManager>();
                if (_i == null)
                {
                    _i = new GameObject("SoundManager", typeof(SoundManager)).AddComponent<SoundManager>();
                }
            }

            return _i;
        }

        private set
        {
            _i = value;
        }
    }

    public enum Sound
    {
        PlayerMove,
        PlayerAttack
    }

    public enum Loop
    {
        Forest,
        Lake,
        Village
    }

    private static AudioSource oneShotSource;
    private static AudioSource loopSource;
    private static AudioSource musicSource1;
    private static AudioSource musicSource2;

    private static Dictionary<Sound, float> lastTimesSound;

    private void Awake()
    {
        lastTimesSound = new Dictionary<Sound, float> { [Sound.PlayerMove] = 0f };

        oneShotSource = this.gameObject.AddComponent<AudioSource>();
        loopSource = this.gameObject.AddComponent<AudioSource>();
        musicSource1 = this.gameObject.AddComponent<AudioSource>();
        musicSource2 = this.gameObject.AddComponent<AudioSource>();

        loopSource.loop = true;
        musicSource1.loop = true;
        musicSource2.loop = true;
    }

    public void PlaySound(Sound sound)
    {
        if (!CanPlaySound(sound)) return;

        oneShotSource.PlayOneShot(GetAudioClip(sound));
    }

    public void PlaySound(Sound sound, float volume)
    {
        if (!CanPlaySound(sound)) return;

        oneShotSource.PlayOneShot(GetAudioClip(sound), volume);
    }

    public void PlayLoop(Loop loop)
    {
        loopSource.clip = GetAudioClip(loop);
        loopSource.Play();
    }

    public void PlayMusic(Loop loop)
    {
        AudioSource activeSource = musicSource1.isPlaying ? musicSource2 : musicSource1;

        activeSource.clip = GetAudioClip(loop);
    }

    public void PlayMusicWithFade(Loop loop, float transitionTime)
    {
        AudioSource activeSource = musicSource1.isPlaying ? musicSource2 : musicSource1;

        StartCoroutine(UpdateMusicWithFade(activeSource, GetAudioClip(loop), transitionTime));
    }

    public void SetVolumeMusic(float volume)
    {
        musicSource1.volume = volume;
        musicSource2.volume = volume;
    }

    public void SetVolumeSFX(float volume)
    {
        loopSource.volume = volume;
        oneShotSource.volume = volume;
    }

    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        float t = 0f;

        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = 1 - (t / transitionTime);
            yield return null;
        }

        activeSource.clip = newClip;

        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = t / transitionTime;
            yield return null;
        }
    }

    private bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;
            case Sound.PlayerMove:
                if (lastTimesSound.ContainsKey(sound))
                {
                    float cdSoundTimer = .05f;
                    if (lastTimesSound[sound] + cdSoundTimer < Time.time)
                    {
                        lastTimesSound[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
        }
    }

    private AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.clip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }

    private AudioClip GetAudioClip(Loop loop)
    {
        foreach (GameAssets.LoopAudioClip loopAudioClip in GameAssets.i.loopAudioClipArray)
        {
            if (loopAudioClip.loop == loop)
            {
                return loopAudioClip.clip;
            }
        }
        Debug.LogError("Loop " + loop + " not found!");
        return null;
    }
}


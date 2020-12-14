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
                    _i = new GameObject("SoundManager", typeof(SoundManager)).GetComponent<SoundManager>();
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
        FoldsHandling,
        FoldsSucced,
        FoldsDrop,
        SFX_UI_Support,
        SFX_UI_Return,
        SFX_UI_Transition,
        SFX_UI_NextDialogue,
        SFX_Origami_Boat_Succed,
        SFX_Origami_Flower_Succed,
        SFX_Origami_Bird_Succed,
        SFX_Origami_Bone_Succed,
        SFX_Origami_StoneBridge_Succed,
        SFX_Origami_Mill_Succed,

    }

    public enum Loop
    {
        FoldsMove,
        FoldsPressure,
        // music
        MusicFold1,
        MusicFold2,
        MusicFold3,
        MusicFold4,
        MusicFold5,
        MusicRadio,
        MusicVillage
    }

    private static AudioSource oneShotSource;
    private static AudioSource loopSource;
    private static AudioSource musicSource1;
    private static AudioSource musicSource2;

    private static float musicVolume = 1f;
    private static float sfxVolume = 1f;

    //private static Dictionary<Sound, float> lastTimesSound;

    private void Awake()
    {
        //lastTimesSound = new Dictionary<Sound, float> { };

        oneShotSource = this.gameObject.AddComponent<AudioSource>();
        loopSource = this.gameObject.AddComponent<AudioSource>();
        musicSource1 = this.gameObject.AddComponent<AudioSource>();
        musicSource2 = this.gameObject.AddComponent<AudioSource>();

        SetVolumeSFX(0.5f);

        musicSource1.Stop();
        musicSource2.Stop();
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
        AudioClip newClip = GetAudioClip(loop);
        if (!loopSource.isPlaying || loopSource.clip.name != newClip.name)
        {
            loopSource.clip = newClip;
            loopSource.Play();
        }
    }

    public void PlayLoop(Loop loop, float avgPercent, float framePercent)
    {
        loopSource.volume = sfxVolume * (Mathf.Abs(1 - framePercent / avgPercent) + .5f);

        PlayLoop(loop);
    }

    public void StopOrigamiLoop()
    {
        loopSource.Stop();
    }

    public void PlayMusic(Loop loop)
    {
        AudioSource activeSource = musicSource1.isPlaying ? musicSource1 : musicSource2;

        activeSource.clip = GetMusicAudioClip(loop);
        activeSource.Play();
    }

    public void ReplaceMusic(Loop loop)
    {
        AudioSource activeSource = musicSource1.isPlaying ? musicSource1 : musicSource2;
        AudioSource inactiveSource = musicSource1.isPlaying ? musicSource2 : musicSource1;

        inactiveSource.clip = GetMusicAudioClip(loop);
        inactiveSource.time = activeSource.time;
        inactiveSource.Play();
        activeSource.Stop();
    }

    public void PlayMusicWithFade(Loop loop, float transitionTime)
    {
        AudioSource activeSource = musicSource1.isPlaying ? musicSource1 : musicSource2;

        StartCoroutine(UpdateMusicWithFade(activeSource, GetMusicAudioClip(loop), transitionTime));
    }

    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        for (float t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (1 - (t / transitionTime)) * musicVolume;
            yield return null;
        }

        activeSource.clip = newClip;
        activeSource.Play();

        for (float t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = musicVolume * t / transitionTime;
            yield return null;
        }
    }

    public void PlayOrigamiMusic(int pliCount, int indexPliage)
    {
        if (indexPliage % 2 == 0)
        {
            switch (pliCount / 2)
            {
                case 2:
                    PlayMusic2Folds(indexPliage);
                    break;
                case 4:
                    PlayMusic4Folds(indexPliage);
                    break;
                case 5:
                    PlayMusic5Folds(indexPliage);
                    break;
                case 8:
                    PlayMusic8Folds(indexPliage);
                    break;
                case 10:
                    PlayMusic10Folds(indexPliage);
                    break;
                case 14:
                    PlayMusic14Folds(indexPliage);
                    break;
            }
        }
    }

    #region RepartitionMusiquePlis

    private void PlayMusic2Folds(int indexPliage)
    {
        if (indexPliage < 2)
        {
            PlayMusic(Loop.MusicFold2);
            return;
        }
        if (indexPliage < 4)
        {
            ReplaceMusic(Loop.MusicFold4);
        }
        else
        {
            ReplaceMusic(Loop.MusicFold5);
        }
    }

    private void PlayMusic4Folds(int indexPliage)
    {
        if (indexPliage < 2)
        {
            PlayMusic(Loop.MusicFold2);
            return;
        }
        if (indexPliage < 4)
        {
            ReplaceMusic(Loop.MusicFold3);
            return;
        }
        if (indexPliage < 6)
        {
            ReplaceMusic(Loop.MusicFold4);
        }
        else
        {
            ReplaceMusic(Loop.MusicFold5);
        }
    }

    private void PlayMusic5Folds(int indexPliage)
    {
        if (indexPliage < 2)
        {
            PlayMusic(Loop.MusicFold1);
            return;
        }
        if (indexPliage < 4)
        {
            ReplaceMusic(Loop.MusicFold2);
            return;
        }
        if (indexPliage < 6)
        {
            ReplaceMusic(Loop.MusicFold3);
            return;
        }
        if (indexPliage < 8)
        {
            ReplaceMusic(Loop.MusicFold4);
        }
        else
        {
            ReplaceMusic(Loop.MusicFold5);
        }
    }

    private void PlayMusic8Folds(int indexPliage)
    {
        if (indexPliage < 2)
        {
            PlayMusic(Loop.MusicFold1);
            return;
        }
        if (indexPliage < 4)
        {
            ReplaceMusic(Loop.MusicFold2);
            return;
        }
        if (indexPliage < 8)
        {
            ReplaceMusic(Loop.MusicFold3);
            return;
        }
        if (indexPliage < 12)
        {
            ReplaceMusic(Loop.MusicFold4);
        }
        else
        {
            ReplaceMusic(Loop.MusicFold5);
        }
    }

    private void PlayMusic10Folds(int indexPliage)
    {
        if (indexPliage < 4)
        {
            PlayMusic(Loop.MusicFold1);
            return;
        }
        if (indexPliage < 8)
        {
            ReplaceMusic(Loop.MusicFold2);
            return;
        }
        if (indexPliage < 12)
        {
            ReplaceMusic(Loop.MusicFold3);
            return;
        }
        if (indexPliage < 16)
        {
            ReplaceMusic(Loop.MusicFold4);
        }
        else
        {
            ReplaceMusic(Loop.MusicFold5);
        }
    }

    private void PlayMusic14Folds(int indexPliage)
    {
        if (indexPliage < 4)
        {
            PlayMusic(Loop.MusicFold1);
            return;
        }
        if (indexPliage < 10)
        {
            ReplaceMusic(Loop.MusicFold2);
            return;
        }
        if (indexPliage < 16)
        {
            ReplaceMusic(Loop.MusicFold3);
            return;
        }
        if (indexPliage < 22)
        {
            ReplaceMusic(Loop.MusicFold4);
        }
        else
        {
            ReplaceMusic(Loop.MusicFold5);
        }
    }

    #endregion


    public void SetVolumeMusic(float volume)
    {
        musicVolume = volume;
        musicSource1.volume = volume;
        musicSource2.volume = volume;
    }

    public void SetVolumeSFX(float volume)
    {
        sfxVolume = volume;
        loopSource.volume = volume;
        oneShotSource.volume = volume;
    }

    private bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;
                //case Sound.FoldsMove:
                //    if (lastTimesSound.ContainsKey(sound))
                //    {
                //        float cdSoundTimer = .05f;
                //        if (lastTimesSound[sound] + cdSoundTimer < Time.time)
                //        {
                //            lastTimesSound[sound] = Time.time;
                //            return true;
                //        }
                //        else
                //        {
                //            return false;
                //        }
                //    }
                //    else
                //    {
                //        return false;
                //    }
        }
    }

    private AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClips)
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
        foreach (GameAssets.LoopAudioClip loopAudioClip in GameAssets.i.loopAudioClips)
        {
            if (loopAudioClip.loop == loop)
            {
                return loopAudioClip.clip;
            }
        }
        Debug.LogError("Loop " + loop + " not found!");
        return null;
    }

    private AudioClip GetMusicAudioClip(Loop loop)
    {
        foreach (GameAssets.LoopAudioClip musicAudioClip in GameAssets.i.musicAudioClips)
        {
            if (musicAudioClip.loop == loop)
            {
                return musicAudioClip.clip;
            }
        }
        Debug.LogError("Loop " + loop + " not found!");
        return null;
    }

    public void StopMusic()
    {
        AudioSource active = musicSource1.isPlaying ? musicSource1 : musicSource2;
        active.Stop();
    }
}


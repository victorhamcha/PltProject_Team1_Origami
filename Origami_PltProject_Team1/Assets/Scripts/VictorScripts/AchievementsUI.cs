using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
public class AchievementsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text[] titleTxTs;
    [SerializeField] private TMP_Text[] descTxTs;
    [SerializeField] private Image[] logos;
    [SerializeField] private Sprite locks;
    [SerializeField] private Achievements[] achievements;

    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle sfxToggle;

    private bool next = false;
    private bool onetTime = false;
    private float _timer = 0f;
    private float _timerCinematic = 0.0f;
    private AsyncOperation asyncOperation = null;

    [SerializeField] private GameObject _cinematicCanvas = null;
    [SerializeField] private GameObject _frontScene = null;
    [SerializeField] private GameObject _background = null;
    [SerializeField] private VideoClip _cinematicClip = null;
    [SerializeField] private DestroyObjectEndVIdeo _endVideo = null;

    private void Awake()
    {
        DontDestroyOnLoad(_cinematicCanvas);
    }

    void Start()
    {
        for (int i = 0; i < achievements.Length; i++)
        {
            titleTxTs[i].text = achievements[i].nameSucces;
            descTxTs[i].text = achievements[i].descriptionSucces;
            logos[i].sprite = achievements[i]._sprtSucces;
            if (PlayerPrefs.HasKey(achievements[i].nameSucces))
            {
                if (PlayerPrefs.GetInt(achievements[i].nameSucces) == 1)
                    logos[i].sprite = locks;
                else
                    logos[i].sprite = locks;
            }
            else
            {
                logos[i].sprite = locks;
            }
        }
        SetMusicVolume();
        SetSFXVolume();
        SoundManager.i.PlayMusic(SoundManager.Loop.MusicMenu);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 2f && !onetTime)
        {
            onetTime = true;
            StartCoroutine("LoadSceneI");
        }
    }

    public void PlayUISound(string sound)
    {
        switch (sound)
        {
            case "SFX_UI_Support":
                SoundManager.i.PlaySound(SoundManager.Sound.SFX_UI_Support);
                break;
            case "SFX_UI_Return":
                SoundManager.i.PlaySound(SoundManager.Sound.SFX_UI_Return);
                break;
            case "SFX_UI_Transition":
                SoundManager.i.PlaySound(SoundManager.Sound.SFX_UI_Transition);
                break;
            case "SFX_UI_NextDialogue":
                SoundManager.i.PlaySound(SoundManager.Sound.SFX_UI_NextDialogue);
                break;
        }
    }

    public void SetMusicVolume()
    {
        bool isOn = !musicToggle.isOn;
        float volume = isOn ? 1f : 0f;
        PlayerPrefs.SetFloat("MusicVolume", volume);
        SoundManager.i.SetVolumeMusic(volume);
    }

    public void SetSFXVolume()
    {
        bool isOn = !sfxToggle.isOn;
        float volume = isOn ? 1f : 0f;
        PlayerPrefs.SetFloat("SFXVolume", volume);
        SoundManager.i.SetVolumeSFX(volume);
    }

    public void LoadScene(string sceneName)
    {
        next = true;
    }

    IEnumerator LoadSceneI()
    {
        yield return null;
        asyncOperation = SceneManager.LoadSceneAsync(1);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            if (next && asyncOperation.progress >= 0.9f)
            {
                _timerCinematic += Time.deltaTime;
                
                _endVideo.gameObject.SetActive(true);
                _frontScene.SetActive(false);
                _background.SetActive(false);
                SoundManager.i.StopMusic();
                _endVideo.gameObject.GetComponent<VideoPlayer>().SetDirectAudioVolume(0, PlayerPrefs.GetFloat("MusicVolume"));
                _endVideo.isPlaying = true;

                if (_timerCinematic > (_cinematicClip.length - 12.0f))
                {
                    asyncOperation.allowSceneActivation = true;
                    StopCoroutine("LoadSceneI");
                }
            }
            yield return null;
        }
    }

}

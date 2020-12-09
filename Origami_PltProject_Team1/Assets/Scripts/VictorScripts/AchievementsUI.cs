﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class AchievementsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text[] titleTxTs;
    [SerializeField] private TMP_Text[] descTxTs;
    [SerializeField] private Image[] logos;
    [SerializeField] private Image[] locks;
    [SerializeField] private Achievements[] achievements;
    private bool next = false;
    private bool onetTime = false;
    private float _timer = 0f;

    void Start()
    {
        for (int i = 0; i < achievements.Length; i++)
        {
            titleTxTs[i].text = achievements[i].nameSucces;
            descTxTs[i].text = achievements[i].descriptionSucces;
            logos[i].sprite = achievements[i]._sprtSucces;
            if (achievements[i]._isLock)
            {
                locks[i].gameObject.SetActive(true);
            }
            else
            {
                locks[i].gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer>2f && !onetTime){
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


    public void LoadScene(string sceneName)
    {
        next = true;
    }

    IEnumerator LoadSceneI()
    {
        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("LDScene");
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            if (next)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}

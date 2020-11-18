using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AchievementsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text[] titleTxTs;
    [SerializeField] private TMP_Text[] descTxTs;
    [SerializeField] private Image[] logos;
    [SerializeField] private Image[] locks;
    [SerializeField] private Achievements[] achievements;


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

    // Update is called once per frame
    void Update()
    {

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
}

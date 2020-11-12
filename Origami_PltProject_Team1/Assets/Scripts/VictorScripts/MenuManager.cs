using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
     [SerializeField] private Swiping swiping;
    [SerializeField] private TMP_Text achievementTxt;
    [SerializeField] private GameObject achievementsLock;
    [SerializeField] private GameObject achievementsUnlock;

  
    
    void Start()
    {
       // AchievementShow(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (swiping.SwipeLeft)
        {
            
            //AchievementShow(true);
        }
            
        else if (swiping.SwipeRight)
        {
            
           // AchievementShow(false);
        }
            

       
    }


    //public void AchievementShow(bool isLock)
    //{
    //    if(isLock)
    //    {
    //        //Show Locked Achievements
    //        achievementTxt.text = "Locked";
    //        achievementsLock.SetActive(true);
    //        achievementsUnlock.SetActive(false);
    //    }

    //    else
    //    {
    //        //Show Unlocked Achievements
    //        achievementTxt.text = "Unlocked";
    //        achievementsUnlock.SetActive(true);
    //        achievementsLock.SetActive(false);
    //    }
    //}
}

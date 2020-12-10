using UnityEngine;
using GooglePlayGames;

public class GPSAchievements : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowAchievemnts()
    {
        VerifyConnection();
        Social.ShowAchievementsUI();
       
    }

    public void IncrementSucces(string id)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, 1, null);
        Debug.Log("Increment" + id);
    }

    public static void UnlockSucces(string id)
    {
        Social.ReportProgress(id, 100f, null);
        Debug.Log("Unlock");
    }

    public void VerifyConnection()
    {
        if(PlayerPrefs.GetInt("aut")==0)
        {
            Social.Active.localUser.Authenticate(succes =>
            {
                if (succes)
                {
                    PlayerPrefs.SetInt("aut", 1);
                }
                else
                {
                    PlayerPrefs.SetInt("aut", 0);
                }
                PlayerPrefs.Save();
            });
        }
    }
}

using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;


public class GPSauthentification : MonoBehaviour
{

    public static PlayGamesPlatform platform;

    // Start is called before the first frame update
    void Start()
    {
        if(platform==null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            platform = PlayGamesPlatform.Activate();


        }
        if(PlayerPrefs.HasKey("aut"))
        {
            if(PlayerPrefs.GetInt("aut")==1)
            {
                Authentificate();
            }
        }
        else
        {
            Authentificate();
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Authentificate()
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

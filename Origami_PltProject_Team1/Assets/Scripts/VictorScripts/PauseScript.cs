using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    static bool paused;
    // Start is called before the first frame update
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    public void Pause ()
    {
        paused = !paused;

        if(paused)
        {
            Time.timeScale = 0;
            Debug.Log("Paused");
        }
        else
        {
            Time.timeScale = 1;
            Debug.Log("Unpaused");
        }
    }
}

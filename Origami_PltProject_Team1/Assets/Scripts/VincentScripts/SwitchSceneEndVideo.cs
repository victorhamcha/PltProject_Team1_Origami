using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SwitchSceneEndVideo : MonoBehaviour
{

    private VideoPlayer _videoPlayer = null;
    private float timer = 0f;

    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > _videoPlayer.length)
        {
            SceneManager.LoadScene(0);
            timer = -10000;
        }
    }

}

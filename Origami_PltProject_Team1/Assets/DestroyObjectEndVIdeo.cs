using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DestroyObjectEndVIdeo : MonoBehaviour
{
    public bool isPlaying = false;
    [SerializeField] private Canvas _cinematicCanvas = null;
    [SerializeField] private VideoClip _cinematicClip = null;

    private float timer = 0f;

    void Update()
    {
        if (isPlaying)
        {
            timer += Time.deltaTime;
            if (timer > (_cinematicClip.length - 8f))
            {
                Destroy(_cinematicCanvas.gameObject);
            }
        }
        Debug.Log("Timer : " + timer);
    }
}

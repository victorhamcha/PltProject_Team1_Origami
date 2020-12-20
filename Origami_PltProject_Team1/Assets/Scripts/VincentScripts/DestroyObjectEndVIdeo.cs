using UnityEngine;
using UnityEngine.Video;

public class DestroyObjectEndVIdeo : MonoBehaviour
{
    public bool isPlaying = false;
    [SerializeField] private GameObject _cinematiqueObject = null;
    [SerializeField] private VideoClip _cinematicClip = null;

    private float timer = 0f;

    void Update()
    {
        if (isPlaying)
        {
            timer += Time.deltaTime;
            if (timer > (_cinematicClip.length - 8f))
            {
                Destroy(_cinematiqueObject);
            }
        }
    }
}

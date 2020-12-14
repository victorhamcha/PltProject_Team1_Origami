using UnityEngine;
using UnityEngine.Video;

public class AnimCelebrations : MonoBehaviour
{
    [SerializeField] private Animator _animator = null;
    [SerializeField] private AnimationClip _animFadeOut = null;
    [SerializeField] private VideoPlayer _videoPlayer = null;
    private bool oneTime = false;

    private void Start()
    {
        _animator.speed = 0;
        _videoPlayer.Play();
        oneTime = true;
    }

    private void Update()
    {
        if (oneTime)
        {
            oneTime = false;
            _videoPlayer.Pause();
        }
    }

    public void PlayAnimButtonFadeOut()
    {
        _animator.speed = 1;
        _animator.Play(_animFadeOut.name);

        _videoPlayer.Play();
    }

    public float GetNormalizedTimeAnimator()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

}

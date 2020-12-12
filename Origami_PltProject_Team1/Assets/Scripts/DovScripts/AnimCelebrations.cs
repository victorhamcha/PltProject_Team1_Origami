using UnityEngine;
using UnityEngine.Video;

public class AnimCelebrations : MonoBehaviour
{
    [SerializeField] private Animator _animator = null;
    [SerializeField] private AnimationClip _animFadeOut = null;
    [SerializeField] private VideoPlayer _videoPlayer = null;

    private void Start()
    {
        _animator.speed = 0;
    }

    public void PlayAnimButtonFadeOut()
   {
        _animator.speed = 1;
        _animator.Play(_animFadeOut.name);

        _videoPlayer.targetTexture.Release();
        _videoPlayer.Play();
   }

    public float GetNormalizedTimeAnimator()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

}

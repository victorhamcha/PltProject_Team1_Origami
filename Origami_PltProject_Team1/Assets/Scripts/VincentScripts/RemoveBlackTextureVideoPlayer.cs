using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class RemoveBlackTextureVideoPlayer : MonoBehaviour
{

    private VideoPlayer _videoPlayer = null;
    private Texture _textureTransparente = null;

    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _textureTransparente = GameManager.Instance.GetTextureTransparente();

        Graphics.Blit(_textureTransparente, _videoPlayer.targetTexture);
    }

}

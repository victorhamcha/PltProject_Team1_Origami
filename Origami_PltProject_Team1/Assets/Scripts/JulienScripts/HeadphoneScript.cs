using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HeadphoneScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _headphoneText;
    [SerializeField] private Image _headphoneImage;
    [SerializeField] private Image _headphoneBackground;

    [SerializeField] private float _fadeDuration = 2.0f;
    [SerializeField] private float _idleDuration = 5.0f;

    private float _lerpTimer = 0.0f;

    private bool _isFadeIn = false;
    private bool _isFadeOut = false;
    private bool _isFadeBackground = false;

    void Awake()
    {
        StartCoroutine("SwitchFade");
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFadeIn) FadeIn();
        else if (_isFadeOut) FadeOut();
        else if (_isFadeBackground) FadeBackground();
        else _lerpTimer = 0.0f;
    }

    private void FadeIn()
    {
        if (_lerpTimer < _fadeDuration && _isFadeIn)
        {
            //Debug.Log("FadeIn");
            _lerpTimer += Time.deltaTime;
            _headphoneText.faceColor = new Color(_headphoneText.faceColor.r, _headphoneText.faceColor.g, _headphoneText.faceColor.b, Mathf.Lerp(0, 1, _lerpTimer / _fadeDuration));
            _headphoneImage.color = new Color(_headphoneImage.color.r, _headphoneImage.color.g, _headphoneImage.color.b, Mathf.Lerp(0, 1, _lerpTimer / _fadeDuration));
        }
        else
        {
            //Debug.Log("ici");
            _lerpTimer = 0.0f;
            _isFadeIn = false;
        }
    }

    private void FadeOut()
    {
        if (_lerpTimer < _fadeDuration && _isFadeOut)
        {
            //Debug.Log("FadeOut");
            _lerpTimer += Time.deltaTime;
            _headphoneText.faceColor = new Color(_headphoneText.faceColor.r, _headphoneText.faceColor.g, _headphoneText.faceColor.b, Mathf.Lerp(1, 0, _lerpTimer / _fadeDuration));
            _headphoneImage.color = new Color(_headphoneImage.color.r, _headphoneImage.color.g, _headphoneImage.color.b, Mathf.Lerp(1, 0, _lerpTimer / _fadeDuration));
        }
        else
        {
            _lerpTimer = 0.0f;
            _isFadeOut = false;
            //Debug.Log("ici");
        }
    }

    private void FadeBackground()
    {
        if (_lerpTimer < _fadeDuration && _isFadeBackground)
        {
            //Debug.Log("FadeBackGround");
            _lerpTimer += Time.deltaTime;
            _headphoneBackground.color = new Color(_headphoneBackground.color.r, _headphoneBackground.color.g, _headphoneBackground.color.b, Mathf.Lerp(1, 0, _lerpTimer / _fadeDuration));
            //Debug.Log(_headphoneBackground.color.a);
        }
        else
        {
            _lerpTimer = 0.0f;
            _isFadeBackground = false;
        }

        
    }

    IEnumerator SwitchFade()
    {
        _isFadeIn = true;
        yield return new WaitForSeconds(_fadeDuration + 0.01f);
        _isFadeIn = false;
        yield return new WaitForSeconds(_idleDuration + 0.01f);
        _isFadeOut = true;
        yield return new WaitForSeconds(_fadeDuration + 0.01f);
        _isFadeOut = false;
        _isFadeBackground = true;
        yield return new WaitForSeconds(_fadeDuration + 0.01f);
        _isFadeBackground = false;
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }
}

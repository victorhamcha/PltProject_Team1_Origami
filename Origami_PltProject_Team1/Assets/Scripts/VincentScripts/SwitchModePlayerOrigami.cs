using UnityEngine;

public class SwitchModePlayerOrigami : MonoBehaviour
{
    [SerializeField] private Animator _animatorFadeOrigami = null;
    [SerializeField] private GameObject _pliageToDo = null;
    [SerializeField] private Transform _origamie = null;

    [SerializeField] private Transform _posPliageToFaceCam = null;
    [SerializeField] private AnimationClip _animFadeIn = null;

    [SerializeField] private Material _blurMaterial = null;
    [SerializeField] private float _maxBlurValue = 0.01f;
    [SerializeField] private float _minBlurValue = 0f;

    private bool _switchModeOrigami = false;
    private bool _isOnModeOrigami = false;
    private bool _isOnReverseAnim = false;

    private float _timerAnim = 0f;
    private float _timerAnimReverse = 0f;

    private Entity _movementPlayer;
    private float _speedMax = 0f;

    void Start()
    {
        _movementPlayer = GetComponent<Entity>();
        _blurMaterial.SetFloat("BlurValue", 0);

        _speedMax = _movementPlayer._speedMax;
        _animatorFadeOrigami.speed = 0;
    }

    void Update()
    {
        //Si l'animation en cours est fini et qu'on est en train de jouer l'anamitions en mode reverse
        if (_animatorFadeOrigami.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && _isOnReverseAnim)
        {
            _pliageToDo.SetActive(false);
        }

        if (_isOnModeOrigami && _blurMaterial.GetFloat("BlurValue") != _maxBlurValue)
        {
            _timerAnim += Time.deltaTime;
            _blurMaterial.SetFloat("BlurValue", Mathf.Lerp(_minBlurValue, _maxBlurValue, _timerAnim));
            _timerAnimReverse = 0f;
        }
        else if (!_isOnModeOrigami && _blurMaterial.GetFloat("BlurValue") != _minBlurValue)
        {
            _timerAnimReverse += Time.deltaTime;
            _blurMaterial.SetFloat("BlurValue", Mathf.Lerp(_maxBlurValue, _minBlurValue, _timerAnimReverse));
            _timerAnim = 0f;
        }

        if (_switchModeOrigami)
        {
            _switchModeOrigami = false;
            _isOnModeOrigami = !_isOnModeOrigami;

            if (_isOnModeOrigami)
            {
                _origamie.position = _posPliageToFaceCam.position;
                _pliageToDo.transform.rotation = _posPliageToFaceCam.rotation;
                _movementPlayer._isMovingToDestination = false;
                _movementPlayer._speedMax = 0f;
                _animatorFadeOrigami.Play(_animFadeIn.name, -1, 0);
                _animatorFadeOrigami.speed = 1;
                _pliageToDo.SetActive(true);
                _isOnReverseAnim = false;
            }
            else
            {
                _movementPlayer._speedMax = _speedMax;
                _animatorFadeOrigami.Play(_animFadeIn.name+"_reverse", -1, 0);
                _isOnReverseAnim = true;
            }
        }

    }

    public void SwitchMode()
    {
        _switchModeOrigami = true;
    }
}

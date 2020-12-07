using UnityEngine;

public class SwitchModePlayerOrigami : MonoBehaviour
{
    [SerializeField] private Animator _animatorFadeOrigami = null;
    [SerializeField] public GameObject _pliageToDo = null;
    [SerializeField] private Transform _origami = null;

    [SerializeField] private Transform _posPliageToFaceCam = null;
    [SerializeField] private AnimationClip _animFadeIn = null;

    [SerializeField] private Material _blurMaterial = null;
    [SerializeField] private float _maxBlurValue = 0.01f;
    [SerializeField] private float _minBlurValue = 0f;

    private PliageManager _pliageManager = null;

    [HideInInspector] public bool _switchModeOrigami = false;
    [HideInInspector] public bool _isOnModeOrigami = false;
    private bool _isOnReverseAnim = false;
    public bool _OnModeEnd = false;

    private float _timerAnim = 0f;
    private float _timerAnimReverse = 0f;

    private Entity _movementPlayer;
    private float _speedMax = 0f;

    [SerializeField] private Transform _posCelebration = null;
    [SerializeField] private GameObject _celebration = null;
    [SerializeField] private GameObject _celebrationBateau = null;
    [SerializeField] private GameObject _celebrationOiseau = null;
    [SerializeField] private GameObject _celebrationFleur = null;
    [SerializeField] private GameObject _canvaCelebration = null;
    [SerializeField] private AnimationClip _animationClipFadeOutButton = null;
    private bool _iscelebrate = false;
    private GameObject _ob = null;
    private bool asClickToContinue = false;
    private AnimCelebrations _objAnimCelebration = null;

    void Start()
    {
        _movementPlayer = GetComponent<Entity>();
        _blurMaterial.SetFloat("BlurValue", 0);

        _pliageManager = _pliageToDo.GetComponent<PliageManager>();
        if (_pliageManager == null)
        {
            Debug.LogError("PliageManager not found on ''PliageToDo'' object.");
        }

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
                ActiveOrigami();
            }
            else
            {
                _movementPlayer.MovePlay();
                _animatorFadeOrigami.Play(_animFadeIn.name + "_reverse", -1, 0);
                _isOnReverseAnim = true;
            }
        }
        if (_OnModeEnd != _iscelebrate && _isOnModeOrigami)
        {
            _iscelebrate = _OnModeEnd;

            if (_pliageToDo.name == "pliage_bateau")
            {
                _ob = Instantiate(_celebrationBateau, _posCelebration);
            }
            else if (_pliageToDo.name == "pliage_fleur")
            {
                _ob = Instantiate(_celebrationFleur, _posCelebration);
            }
            else if (_pliageToDo.name == "pliage_oiseau")
            {
                _ob = Instantiate(_celebrationOiseau, _posCelebration);
            }
            else
            {
                _ob = Instantiate(_celebration, _posCelebration);
            }
            _objAnimCelebration = _ob.GetComponent<AnimCelebrations>();
            _canvaCelebration.SetActive(true);
        }

        if (_OnModeEnd)
        {
            _pliageToDo.SetActive(false);
        }

        if (asClickToContinue)
        {
            asClickToContinue = false;
            if (_objAnimCelebration != null)
            {
                _objAnimCelebration.PlayAnimButtonFadeOut();
            }
            _canvaCelebration.GetComponent<Animator>().Play(_animationClipFadeOutButton.name);
        }

        if (_objAnimCelebration != null && _objAnimCelebration.GetNormalizedTimeAnimator() >= 1)
        {
            Destroy(_ob);
            _canvaCelebration.SetActive(false);
            _OnModeEnd = false;
            _iscelebrate = false;
            SwitchMode();
        }

    }

    public void ClickToContinue()
    {
        asClickToContinue = true;
    }

    public void SwitchMode()
    {
        _switchModeOrigami = true;
    }

    public void ActiveOrigami()
    {
        _origami.position = _posPliageToFaceCam.position;
        _pliageToDo.transform.rotation = _posPliageToFaceCam.rotation;
        _movementPlayer.MoveStop();
        _animatorFadeOrigami.Play(_animFadeIn.name, -1, 0);
        _animatorFadeOrigami.speed = 1;
        _pliageToDo.SetActive(true);
        _isOnReverseAnim = false;
        _pliageManager = _pliageToDo.GetComponent<PliageManager>();
        _pliageManager.SetUpCurrentPliage();
        _isOnModeOrigami = true;
    }
}


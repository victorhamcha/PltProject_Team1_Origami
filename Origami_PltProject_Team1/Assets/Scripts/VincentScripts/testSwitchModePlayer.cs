using UnityEngine;

public class testSwitchModePlayer : MonoBehaviour
{
    [SerializeField] private GameObject _pliageBoat = null;
    [SerializeField] private Transform _posPliage = null;
    [SerializeField] private PliageManager _pliageManager = null;
    [SerializeField] private bool _activeModeOrigami = false;

    [SerializeField] private GameObject _origamiePos = null;
    [SerializeField] private Animator _animator = null;
    [SerializeField] private AnimationClip _animFadeIn = null;

    [SerializeField] private Material _blurMat = null;

    private float timer = 0f;
    private float timerReverse = 0f;

    private bool _onModeOrigami = false;
    private bool _reverseAnim = false;

    private Entity _movementPlayer;
    private float _speedMax = 0f;

    void Start()
    {
        _movementPlayer = GetComponent<Entity>();
        _speedMax = _movementPlayer._speedMax;
        _animator.speed = 0;
        _blurMat.SetFloat("BlurValue", 0);
    }

    void Update()
    {
        if (_pliageManager.PliageIsFinish())
        {
            //_activeModeOrigami = true;
            //_pliageManager.ResetPliage();
        }

        _origamiePos.transform.position = _posPliage.position;

        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && _reverseAnim)
        {
            _pliageBoat.SetActive(false);
        }

        if (_onModeOrigami)
        {
            timer += Time.deltaTime;
            _blurMat.SetFloat("BlurValue", Mathf.Lerp(0f, 0.01f, timer));
            timerReverse = 0f;
        }
        else if (!_onModeOrigami && _reverseAnim)
        {
            timerReverse += Time.deltaTime;
            _blurMat.SetFloat("BlurValue", Mathf.Lerp(0.01f, 0f, timerReverse));
            timer = 0f;
        }

        if (_activeModeOrigami)
        {
            _activeModeOrigami = false;
            _onModeOrigami = !_onModeOrigami;

            _pliageBoat.transform.rotation = _posPliage.rotation;

            if (_onModeOrigami)
            {
                _movementPlayer._speedMax = 0f;
                _animator.Play(_animFadeIn.name, -1, 0);
                _pliageBoat.SetActive(true);
                _animator.speed = 1;
                _reverseAnim = false;
            }
            else
            {
                _movementPlayer._speedMax = _speedMax;
                _animator.Play(_animFadeIn.name+"_reverse", -1, 0);
                _reverseAnim = true;
            }
        }

    }
}

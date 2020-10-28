using UnityEngine;

public class testSwitchModePlayer : MonoBehaviour
{
    [SerializeField] private GameObject _pliageBoat = null;
    [SerializeField] private Transform _posPliage = null;
    [SerializeField] private PliageManager _pliageManager = null;
    [SerializeField] private bool _activeModeOrigami = false;
    private bool _onModeOrigami = false;

    private TopDownEntity _movementPlayer;
    private float _acceleration = 0f;
    private float _speedMax = 0f;

    void Start()
    {
        _movementPlayer = GetComponent<TopDownEntity>();
        _acceleration = _movementPlayer.acceleration;
        _speedMax = _movementPlayer.speedMax;
    }

    void Update()
    {

        if (_pliageManager.PliageIsFinish())
        {
            _activeModeOrigami = true;
            _pliageManager.ResetPliage();
        }

        if (_activeModeOrigami)
        {
            _activeModeOrigami = false;
            _onModeOrigami = !_onModeOrigami;
            if (_onModeOrigami)
            {
                _movementPlayer.acceleration = 0f;
                _movementPlayer.speedMax = 0f;
            }
            else
            {
                _movementPlayer.acceleration = _acceleration;
                _movementPlayer.speedMax = _speedMax;
            }
            _pliageBoat.SetActive(_onModeOrigami);
            _pliageBoat.transform.position = _posPliage.position;
            _pliageBoat.transform.rotation = _posPliage.rotation;
        }
    }
}

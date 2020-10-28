using UnityEngine;

public class testSwitchModePlayer : MonoBehaviour
{
    [SerializeField] private GameObject pliageBoat = null;
    [SerializeField] private Transform posPliage = null;

    [SerializeField] private bool _activeModeOrigami = false;
    private bool _onModeOrigami = false;

    private TopDownEntity movementPlayer;
    private float _acceleration = 0f;
    private float _speedMax = 0f;

    void Start()
    {
        movementPlayer = GetComponent<TopDownEntity>();
        _acceleration = movementPlayer.acceleration;
        _speedMax = movementPlayer.speedMax;
    }

    void Update()
    {
        if (_activeModeOrigami)
        {
            _activeModeOrigami = false;
            _onModeOrigami = !_onModeOrigami;
            if (_onModeOrigami)
            {
                movementPlayer.acceleration = 0f;
                movementPlayer.speedMax = 0f;
            }
            else
            {
                movementPlayer.acceleration = _acceleration;
                movementPlayer.speedMax = _speedMax;
            }
            pliageBoat.SetActive(_onModeOrigami);
            pliageBoat.transform.position = posPliage.position;
            pliageBoat.transform.rotation = posPliage.rotation;
        }
    }
}

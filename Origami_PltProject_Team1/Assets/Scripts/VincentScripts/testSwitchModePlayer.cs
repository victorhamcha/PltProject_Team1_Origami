using Rewired;
using UnityEngine;

public class testSwitchModePlayer : MonoBehaviour
{

    private Player _rewiredPlayer = null;


    [SerializeField] private GameObject pliageBoat;
    [SerializeField] private GameObject targerSprite;
    [SerializeField] private Transform posPliage;

    public bool _activePliage = false;

    private bool _onModeOrigami = false;

    void Start()
    {
        _rewiredPlayer = ReInput.players.GetPlayer("Player0");
        _rewiredPlayer.controllers.maps.LoadMap(ControllerType.Joystick, 0, "Default", "Default", !_onModeOrigami);
        _rewiredPlayer.controllers.maps.LoadMap(ControllerType.Joystick, 0, "Default", "Origami", _onModeOrigami);
    }

    // Update is called once per frame
    void Update()
    {
        if (_activePliage)
        {
            _activePliage = false;
            _onModeOrigami = !_onModeOrigami;
            pliageBoat.SetActive(_onModeOrigami);
            pliageBoat.transform.position = posPliage.position;
            pliageBoat.transform.rotation = posPliage.rotation;
            targerSprite.transform.rotation = posPliage.rotation;
            _rewiredPlayer.controllers.maps.LoadMap(ControllerType.Joystick, 0, "Default", "Default", !_onModeOrigami);
            _rewiredPlayer.controllers.maps.LoadMap(ControllerType.Joystick, 0, "Default", "Origami", _onModeOrigami);
        }
    }
}

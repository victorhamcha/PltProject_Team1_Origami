using Rewired;
using UnityEngine;

public class testSwitchModePlayer : MonoBehaviour
{

    private Player _rewiredPlayer = null;

    public bool _switchMode = false;
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
        if (_switchMode)
        {
            _switchMode = false;
            _onModeOrigami = !_onModeOrigami;
            _rewiredPlayer.controllers.maps.LoadMap(ControllerType.Joystick, 0, "Default", "Default", !_onModeOrigami);
            _rewiredPlayer.controllers.maps.LoadMap(ControllerType.Joystick, 0, "Default", "Origami", _onModeOrigami);
        }
    }
}

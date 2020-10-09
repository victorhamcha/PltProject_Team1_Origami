using Rewired;
using UnityEngine;

public class testSwitchModePlayer : MonoBehaviour
{

    private Player _rewiredPlayer = null;


    [SerializeField] private GameObject pliageBoat;
    [SerializeField] private Transform cam;


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
        if (_rewiredPlayer.GetButtonDown("GoToPliage"))
        {
            _onModeOrigami = !_onModeOrigami;
            pliageBoat.SetActive(_onModeOrigami);
            pliageBoat.transform.rotation = cam.rotation;
            _rewiredPlayer.controllers.maps.LoadMap(ControllerType.Joystick, 0, "Default", "Default", !_onModeOrigami);
            _rewiredPlayer.controllers.maps.LoadMap(ControllerType.Joystick, 0, "Default", "Origami", _onModeOrigami);
        }
            pliageBoat.transform.position = cam.position;
    }
}

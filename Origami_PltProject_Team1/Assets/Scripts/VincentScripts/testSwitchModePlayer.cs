using UnityEngine;

public class testSwitchModePlayer : MonoBehaviour
{
    [SerializeField] private GameObject pliageBoat;
    [SerializeField] private Transform posPliage;

    [SerializeField] private bool _activeModeOrigami = false;

    private bool _onModeOrigami = false;

    // Update is called once per frame
    void Update()
    {
        if (_activeModeOrigami)
        {
            _activeModeOrigami = false;
            _onModeOrigami = !_onModeOrigami;
            pliageBoat.SetActive(_onModeOrigami);
            pliageBoat.transform.position = posPliage.position;
            pliageBoat.transform.rotation = posPliage.rotation;
        }
    }
}

using UnityEngine;

public class Rotator : MonoBehaviour
{

    public float period = 0.05f;
    public float offset = 0.5f;

    private float _timer = 0f;
    private float _offsetvalue = 0f;

    void Update()
    {

        _timer += Time.deltaTime;

        float t = Mathf.PingPong(_timer, period);
        float r = t / period;

        Vector3 eulerAngles = transform.localEulerAngles;

        eulerAngles.z = _offsetvalue;
        _offsetvalue = Mathf.Lerp(-offset, offset,r);
        eulerAngles.z += _offsetvalue;

        transform.localEulerAngles = eulerAngles;

    }
}

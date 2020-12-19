using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescaleBySizeScreen : MonoBehaviour
{
    private Vector2 _screenSize = new Vector2(1080, 1920);
    private Vector3 _initScale = new Vector3();

    private void Awake()
    {
        _initScale = transform.localScale;
        float facteurScreenSize = (Screen.width / _screenSize.x);
        transform.localScale = new Vector3(facteurScreenSize * _initScale.x, facteurScreenSize * _initScale.y, facteurScreenSize * _initScale.z);
    }
}

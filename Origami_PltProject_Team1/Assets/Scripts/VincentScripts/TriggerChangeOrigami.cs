using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChangeOrigami : MonoBehaviour
{
    [SerializeField] ChangePliage _changePLiage = null;
    [SerializeField] private string _namePliage = null;
    private bool _isTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_isTrigger)
        {
            _isTrigger = true;
            _changePLiage.SetUpPliage(_namePliage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _isTrigger = false;
        }
    }
}

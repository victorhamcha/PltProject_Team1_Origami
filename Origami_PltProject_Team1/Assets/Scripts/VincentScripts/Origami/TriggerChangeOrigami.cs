using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChangeOrigami : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager = null;
    [SerializeField] private GameObject _bulleOrigami = null;
    [SerializeField] private string _namePliage = null;
    private bool _isTrigger = false;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_isTrigger)
        {
            _isTrigger = true;
            _bulleOrigami.SetActive(true);
            _gameManager.SetUpPliage(_namePliage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _bulleOrigami.SetActive(false);
            _isTrigger = false;
        }
    }
}

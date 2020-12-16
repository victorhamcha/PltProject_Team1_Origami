using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerChangeOrigami : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager = null;
    [SerializeField] private GameObject _bulleOrigami = null;
    [SerializeField] private string _namePliage = null;
    [SerializeField] private int _collectibleNeeded = 0;
    [SerializeField] private TextMeshPro _collectibleText = null;
    private bool _isTrigger = false;
    public LayerMask layerBubuleOrigami;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_isTrigger)
        {
            //_collectibleText.gameObject.SetActive(true);
            _collectibleText.text = _gameManager.GetEntity().collectibles + " / " + _collectibleNeeded;
            _isTrigger = true;
            _bulleOrigami.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _collectibleText.gameObject.SetActive(false);
            _bulleOrigami.SetActive(false);
            _isTrigger = false;
        }
    }

    private void ClickClickBubule()
    {
        if (_gameManager.GetEntity().VerifyCollectibles(_collectibleNeeded))
        {
            _gameManager.SetUpPliage(_namePliage);
        }
    }

    private void Update()
    {
        ClickClickManager.Instance.RaycastClick(layerBubuleOrigami);
        if (ClickClickManager.Instance.isTouch && ClickClickManager.Instance.isTouchTarget && _isTrigger && !_gameManager.GetSwitchModePlayerOrigami()._isOnModeOrigami)
        {
            ClickClickBubule();
        }
    }
    
}

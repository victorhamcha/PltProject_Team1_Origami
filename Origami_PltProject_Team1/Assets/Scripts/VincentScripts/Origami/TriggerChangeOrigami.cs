using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChangeOrigami : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager = null;
    [SerializeField] private GameObject _bulleOrigami = null;
    [SerializeField] private string _namePliage = null;
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
            _isTrigger = true;
            _bulleOrigami.SetActive(true);
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

    private void ClickClickBubule()
    {
        _gameManager.SetUpPliage(_namePliage);
    }

    private void Update()
    {
        ClickClickManager.Instance.RaycastClick(layerBubuleOrigami);
        Debug.Log(_gameManager.GetDialogueManager().inDialogue);
        if (ClickClickManager.Instance.isTouch && ClickClickManager.Instance.isTouchTarget && !_gameManager.GetDialogueManager().inDialogue && _isTrigger)
        {
            ClickClickBubule();

        }
    }
}

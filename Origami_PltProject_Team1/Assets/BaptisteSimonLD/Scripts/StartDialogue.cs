using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager = null;
    private int timesTalked = 0;
    private bool onTrigger;
    public List<Vector2> startlistIndexDialogue = null;
    public List<Vector2> endlistIndexDialogue = null;
    public GameObject bubule;
    public LayerMask layerBubule;
    private string _namePliage = "pliage_";
    private bool _tValue = false;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            bubule.SetActive(true);
            onTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            bubule.SetActive(false);
            onTrigger = false;
        }
    }

    private void ClickClickBubule()
    {
        _gameManager.GetDialogueManager().inDialogue = true;
        _gameManager.pliagesAreFinish.TryGetValue(_namePliage, out _tValue);

        if (!_tValue)
        {
            _gameManager.GetDialogueManager().StartDialogue((int)startlistIndexDialogue[timesTalked].x, (int)startlistIndexDialogue[timesTalked].y);
            if (timesTalked < startlistIndexDialogue.Count - 1)
            {
                timesTalked++;
            }
        }
        else
        {
            _gameManager.GetDialogueManager().StartDialogue((int)endlistIndexDialogue[timesTalked].x, (int)endlistIndexDialogue[timesTalked].y);
            if (timesTalked < endlistIndexDialogue.Count - 1)
            {
                timesTalked++;
            }
        }
    }

    private void Update()
    {
        ClickClickManager.Instance.RaycastClick(layerBubule);
        if (ClickClickManager.Instance.isTouch && ClickClickManager.Instance.isTouchTarget && !_gameManager.GetDialogueManager().inDialogue && onTrigger)
        {
            ClickClickBubule();
        }
    }
}

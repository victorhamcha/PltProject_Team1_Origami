using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager = null;
    private int timesTalked = 0;
    private bool onTrigger;
    public List<Vector2> listIndexDialogue = null;
    public GameObject bubule;
    public LayerMask layerBubule;

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

        _gameManager.GetDialogueManager().StartDialogue((int)listIndexDialogue[timesTalked].x, (int)listIndexDialogue[timesTalked].y);
        if (timesTalked < listIndexDialogue.Count - 1)
        {
            timesTalked++;
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

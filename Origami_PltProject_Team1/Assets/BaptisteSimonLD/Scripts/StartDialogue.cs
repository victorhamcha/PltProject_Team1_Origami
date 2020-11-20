using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager = null;
    private int timesTalked = 0;
    public GameObject bubule;
    public List<Vector2> listIndexDialogue = null;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            bubule.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            bubule.SetActive(false);
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
        ClickClickManager.Instance.RaycastClick(bubule.layer);
        if (ClickClickManager.Instance.isTouch && ClickClickManager.Instance.isTouchTarget)
        {
            ClickClickBubule();
            Debug.Log("Sa rentre");
            
        }
    }
}

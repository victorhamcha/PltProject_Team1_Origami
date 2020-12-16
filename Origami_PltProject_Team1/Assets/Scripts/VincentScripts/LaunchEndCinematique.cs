using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchEndCinematique : MonoBehaviour
{

    private StartDialogue _dialogue = null;
    private DialoguesManager _dialogueManager = null;
    [SerializeField] private Canvas _canvas = null;

    private void Start()
    {
        _dialogue = GetComponent<StartDialogue>();
        _dialogueManager = GameManager.Instance.GetDialogueManager();
    }

    void Update()
    {
        if (!_canvas.gameObject.activeSelf && _dialogue.OrigamiPnjFinish() && _dialogue.EndedAllConversation() && !_dialogueManager.inDialogue)
        {
            _canvas.gameObject.SetActive(true);
        }


    }
}

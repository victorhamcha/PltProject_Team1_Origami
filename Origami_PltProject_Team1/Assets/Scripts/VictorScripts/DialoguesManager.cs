﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Rewired;

public class DialoguesManager : MonoBehaviour
{
    [SerializeField]
    private List<Dialogue> dialogues;
    [SerializeField]
    private TextMeshProUGUI nameTxt, sentenceTxt;
    [SerializeField]
    private GameObject dialogueGui;

    private Player _rewiredPlayer = null;
    private float timerSwitchDialogue = 0.5f;

    private int lastDialogue;
    private int nextdialogue;
    bool inDialogue = false;
    bool inTag = false;
    void Start()
    {
        _rewiredPlayer = ReInput.players.GetPlayer("Player0");
    }

    // Update is called once per frame
    void Update()
    {
        timerSwitchDialogue -= Time.deltaTime;
        if (_rewiredPlayer.GetButton("ActionDialogue") && timerSwitchDialogue <= 0)
        {
            timerSwitchDialogue = 0.5f;
            if (inDialogue)
            {
                NextDialogue();
            }
            else
            {
                StartDialogue(0, 2);
            }

        }
        /*if (Input.GetKeyDown(KeyCode.J))
        {
            StartDialogue(1);
        }*/
    }

    public void StartDialogue(int nextDialogue, int lastdialogue)
    {
        if(!inDialogue)
        {
            inDialogue = true;
            nextdialogue = nextDialogue;
            lastDialogue = lastdialogue;
            dialogueGui.SetActive(true);
            dialogueGui.GetComponent<Image>().color = dialogues[nextDialogue].chrColor;
            nameTxt.text = dialogues[nextDialogue].chrName;
            StartCoroutine(TypeSentence(dialogues[nextDialogue].sentence));
        }
       
       
    }
    public void StartDialogue(int nextDialogue)
    {
        if (!inDialogue)
        {
            inDialogue = true;
            nextdialogue = nextDialogue;
            lastDialogue = nextDialogue;
            dialogueGui.SetActive(true);
            dialogueGui.GetComponent<Image>().color = dialogues[nextdialogue].chrColor;
            nameTxt.text = dialogues[nextDialogue].chrName;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(dialogues[nextDialogue].sentence));
        }


    }
    public void NextDialogue()
    {
       
        nextdialogue++;
        if(nextdialogue<=lastDialogue)
        {
            dialogueGui.GetComponent<Image>().color = dialogues[nextdialogue].chrColor;
            nameTxt.text = dialogues[nextdialogue].chrName;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(dialogues[nextdialogue].sentence));
        }
        else
        {
            StopAllCoroutines();
            dialogueGui.SetActive(false);
            inDialogue = false;
            
        }
       
        
    }

    IEnumerator TypeSentence(string sentence)
    {
        sentenceTxt.text = "";
        string tag = "";
        foreach (char letter in sentence.ToCharArray())
        {
            if(!inTag)
            {
                if (letter == '<')
                {

                    inTag = true;
                    Debug.Log("entering");
                }
                else
                {
                    sentenceTxt.text += letter;
                    yield return new WaitForSeconds(0.05f);
                }
            }
            else
            {
                if (letter == '>')
                {

                    inTag = false;
                    Debug.Log("exiting");
                    Debug.Log(tag);
                    //fonctions du parser ICI
                    tag = "";
                }
                else
                {
                    tag += letter;
                }
            }
            
            
        }
    }

   

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Rewired;

public class DialoguesManager : MonoBehaviour
{
    [SerializeField]
    private List<Dialogue> dialogues = null;
    [SerializeField]
    private TextMeshProUGUI nameTxt = null, sentenceTxt = null;
    [SerializeField]
    private GameObject dialogueGui = null;
    [SerializeField]
    private Image arrow = null;
    [SerializeField]
    private Image point = null;
    [SerializeField]
    private Image character = null;
    private float timerSwitchDialogue = 0.5f;
    private bool oneTime = false;

    private Entity playerEntity = null;

    private int lastDialogue;
    private int nextdialogue;
    bool inTyping = false;
    bool inTag = false;
    public bool inDialogue;

    private void Awake()
    {
       
       

        playerEntity = GameManager.Instance.GetEntity();
    }

    // Update is called once per frame
    void Update()
    {
        if (inDialogue)
        {
            timerSwitchDialogue -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.H) && timerSwitchDialogue <= 0)
            {
                timerSwitchDialogue = 0.5f;
                if (inTyping)
                {
                    NextDialogue();
                }
                else
                {
                    StartDialogue(0, 7);
                }

            }
            if (Input.GetMouseButtonDown(0))
            {
                if (inTyping)
                    ShowFullDialogue();
                else
                    NextDialogue();
            }
            /*if (Input.GetKeyDown(KeyCode.J))
            {
                StartDialogue(1);
            }*/
        }
    }

    public void StartDialogue(int nextDialogue, int lastdialogue)
    {
        if(!inTyping)
        {
            playerEntity.MoveStop();
            character.gameObject.SetActive(true);
            arrow.gameObject.SetActive(true);
            point.gameObject.SetActive(false);
            inTyping = true;
            nextdialogue = nextDialogue;
            lastDialogue = lastdialogue;
            dialogueGui.SetActive(true);
            //dialogueGui.GetComponent<Image>().color = dialogues[nextDialogue].chrColor;
            nameTxt.text = dialogues[nextDialogue].chrName;
            StartCoroutine(TypeSentence(dialogues[nextDialogue].sentence));
        }
       
       
    }
    public void StartDialogue(int nextDialogue)
    {
        if (!inTyping)
        {
            character.gameObject.SetActive(true);
            arrow.gameObject.SetActive(true);
            point.gameObject.SetActive(false);
            inTyping = true;
            nextdialogue = nextDialogue;
            lastDialogue = nextDialogue;
            dialogueGui.SetActive(true);
            //dialogueGui.GetComponent<Image>().color = dialogues[nextdialogue].chrColor;
            nameTxt.text = dialogues[nextDialogue].chrName;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(dialogues[nextDialogue].sentence));
        }


    }
    public void NextDialogue()
    {
       
        nextdialogue++;
        inTyping = true;
        if(nextdialogue<=lastDialogue)
        {
            //dialogueGui.GetComponent<Image>().color = dialogues[nextdialogue].chrColor;
            nameTxt.text = dialogues[nextdialogue].chrName;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(dialogues[nextdialogue].sentence));
            if(nextdialogue==lastDialogue)
            {
                arrow.gameObject.SetActive(false);
                point.gameObject.SetActive(true);
            }
            
        }
        else
        {
            inDialogue = false;
            playerEntity.MovePlay();
            StopAllCoroutines();
            dialogueGui.SetActive(false);
            inTyping = false;
            character.gameObject.SetActive(false);
        }
       
        
    }

    public void ShowFullDialogue()
    {
        StopAllCoroutines();
        sentenceTxt.text = "";
        string sentences= dialogues[nextdialogue].sentence;
        string tag = "";
        foreach (char letter in sentences.ToCharArray())
        {
            if (!inTag)
            {
                if (letter == '<')
                {

                    inTag = true;/*
                    Debug.Log("entering");*/
                }
                else
                {
                    sentenceTxt.text += letter;
                    
                }
            }
            else
            {
                if (letter == '>')
                {

                    inTag = false;
/*                    Debug.Log("exiting");
                    Debug.Log(tag);*/
                    //fonctions du parser ICI
                    //StartFunction(tag);
                    tag = "";
                }
                else
                {
                    tag += letter;
                }
            }


        }
        inTyping = false;
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
                    //Debug.Log("entering");
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
                    //Debug.Log("exiting");
                    //Debug.Log(tag);
                    //fonctions du parser ICI
                    //StartFunction(tag);
                    tag = "";
                }
                else
                {
                    tag += letter;
                }
            }
            
            
        }

        inTyping = false;
    }


    public void StartFunction(string function)
    {
        switch (function[0])
        {
            case 'E':
                
                function = function.Remove(0, 2);
                if(function=="SAD")
                {
                    Debug.Log("emotion : SAD");
                }
                break;

            case 'S':
                Debug.Log("soundmanager");
                function = function.Remove(0, 2);
                SoundManager.i.PlaySound((SoundManager.Sound)System.Enum.Parse(typeof(SoundManager.Sound),function)); 
                break;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialoguesManager : MonoBehaviour
{
    [SerializeField]
    private List<Dialogue> dialogues;
    [SerializeField]
    private TextMeshProUGUI nameTxt, sentenceTxt;
    [SerializeField]
    private GameObject dialogueGui;
    private int lastDialogue;
    private int nextdialogue;
    bool inDialogue = false;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            StartDialogue(0, 2);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartDialogue(1);
        }
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
            nameTxt.text = dialogues[nextDialogue].name;
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
            nameTxt.text = dialogues[nextDialogue].name;
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
            nameTxt.text = dialogues[nextdialogue].name;
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
        foreach(char letter in sentence.ToCharArray())
        {
            sentenceTxt.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

}

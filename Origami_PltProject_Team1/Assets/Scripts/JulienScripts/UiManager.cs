using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Rewired;

public class UiManager : MonoBehaviour
{
    #region Variables

    private string txtBackup = "Play";
    [HideInInspector] public static bool isFirstChange = true;
    // Rewired variables
    private Player player;
    private int playerID = 0;

    [SerializeField] private List<TextMeshProUGUI> textList = new List<TextMeshProUGUI>();
    private int listIterator = 0;
    private bool isChangingButton;

    #endregion

    private void Awake()
    {
        
        player = ReInput.players.GetPlayer(playerID);
        
    }

    private void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            textList[0].text = "Play";
            textList[1].text = "Settings";
            textList[2].text = "Credits";
            textList[3].text = "Quit Game";

        }

        if(ButtonBehaviour.usingMouse)
        {
            ButtonBehaviour.usingMouse = false;
            listIterator = 0;
            txtBackup = "Play";
        }
        float vertical = player.GetAxisRaw("Move Vertical");
        if (vertical < -0.2f && !isChangingButton)
        {
            if(isFirstChange)
            {
                Debug.Log("firstchange 1");
                isChangingButton = true;
                StartCoroutine("FirstChange");
            }
            else
            {
                Debug.Log("Down");
                isChangingButton = true;
                StartCoroutine("ChangeButtonDownFromList");
            }

        }
        else if(vertical > 0.2f && !isChangingButton)
        {
            if (isFirstChange)
            {
                Debug.Log("firstchange 2");
                isChangingButton = true;
                StartCoroutine("FirstChange");
            }
            else
            {
                Debug.Log("Up");
                isChangingButton = true;
                StartCoroutine("ChangeButtonUpFromList");
            }
        }

    }

    IEnumerator ChangeButtonDownFromList()
    {
        LockMouse();
        textList[listIterator].text = txtBackup;
        listIterator++;

        if(listIterator < textList.Count)
        {
            txtBackup = textList[listIterator].text;
            textList[listIterator].text = " > " + textList[listIterator].text + " < ";
        }
        else
        {
            listIterator = 0;
            txtBackup = textList[listIterator].text;
            textList[listIterator].text = " > " + textList[listIterator].text + " < ";
           
        }

        yield return new WaitForSeconds(0.2f);
        isChangingButton = false;
    }

    IEnumerator ChangeButtonUpFromList()
    {
        LockMouse();
        textList[listIterator].text = txtBackup;
        listIterator--;

        if (listIterator >= 0)
        {
            txtBackup = textList[listIterator].text;
            textList[listIterator].text = " > " + textList[listIterator].text + " < ";
        }
        else
        {
            listIterator = (textList.Count -1);
            txtBackup = textList[listIterator].text;
            textList[listIterator].text = " > " + textList[listIterator].text + " < ";
        }

        yield return new WaitForSeconds(0.2f);
        isChangingButton = false;
    }

    IEnumerator FirstChange()
    {
        LockMouse();
        isFirstChange = false;
        //ButtonBehaviour.usingMouse = false;
        txtBackup = "Play";
        listIterator = 0;
        textList[listIterator].text = " > Play <";
        yield return new WaitForSeconds(0.2f);
        isChangingButton = false;
    }    
    
    private void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

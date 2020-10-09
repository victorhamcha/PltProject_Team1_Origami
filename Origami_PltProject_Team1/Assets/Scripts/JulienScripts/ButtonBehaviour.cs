using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public static bool usingMouse = false;

    [SerializeField] private TextMeshProUGUI playText;
    [SerializeField] private TextMeshProUGUI settingsText;
    [SerializeField] private TextMeshProUGUI creditsText;
    [SerializeField] private TextMeshProUGUI quitText;


    // Start is called before the first frame update
    void Awake()
    {
       // txt = GetComponentInChildren<TextMeshProUGUI>().text;
    }
    private void Update()
    {
        
    }
    //Need to be moved in another script that will be attached to a button
    #region MouseUI 
    //When highlighted (mouse)
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        SwitchingMode();

        // Debug.Log("highlighted !");
        //GetComponentInChildren<TextMeshProUGUI>().text = " > " + GetComponentInChildren<TextMeshProUGUI>().text + " < ";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       // GetComponentInChildren<TextMeshProUGUI>().text = txt;
    }
    #endregion  

    public void SwitchingMode()
    {
        UiManager.isFirstChange = true;
        usingMouse = true;
        playText.text = "Play";
        settingsText.text = "Settings";
        creditsText.text = "Credits";
        quitText.text = "Quit Game";
    }
}

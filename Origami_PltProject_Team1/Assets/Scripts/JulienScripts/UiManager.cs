using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private string txt = "";
    private void Start()
    {
        txt = GetComponentInChildren<TextMeshProUGUI>().text;
    }
    //When highlighted
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("highlighted !");
        GetComponentInChildren<TextMeshProUGUI>().text = " > " + GetComponentInChildren<TextMeshProUGUI>().text + " < ";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = txt;
    }
}

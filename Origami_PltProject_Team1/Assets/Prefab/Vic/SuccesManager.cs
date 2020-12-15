using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccesManager : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GetSucces(string succes)
    {
        PlayerPrefs.SetInt(succes, 1);
    }
}

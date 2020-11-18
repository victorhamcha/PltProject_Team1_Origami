using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    private int timesTalked = 0;
    private bool isBoatFixed;

    //[SerializeField]
    //private Entity plyrtity;
    
    [SerializeField]
    private DialoguesManager dialMngr;

/*    void Start()
    {
        
    }
    
    void Update()
    {
        
    }*/

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Oui");
        dialMngr.inDialogue = true;

        if(timesTalked == 0)
        {
            //plyrtity.MoveStop();
            dialMngr.StartDialogue(0,3);
            timesTalked++;
        }
        else if(timesTalked == 1)
        {
            //plyrtity.MoveStop();
            dialMngr.StartDialogue(4,6);
            timesTalked++;
        }
        else
        {
            //plyrtity.MoveStop();
            dialMngr.StartDialogue(7, 7);
        }
    }
}

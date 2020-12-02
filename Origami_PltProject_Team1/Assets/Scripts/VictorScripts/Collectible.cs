using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public GameObject prefabCollectible;
    public GameObject test;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        //pas prendre en compte juste pour test
        if(Input.GetKeyDown(KeyCode.A))
        {
            InstantiateCol(true, Vector3.zero,test);
        }
        
    }

    public void InstantiateCol(bool firstTime,Vector3 offset,GameObject parabol)
    {
        if(firstTime)
        {
          GameObject col= Instantiate(prefabCollectible, transform.position + offset, Quaternion.identity);
          col.GetComponent<ParabolaController>().ParabolaRoot = parabol;
        }
    }

   
}

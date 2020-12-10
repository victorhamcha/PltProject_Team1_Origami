using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public GameObject prefabCollectible;
    public GameObject collectibleCurve;


    public void InstantiateCol(bool firstTime,Vector3 offset,GameObject parabol)
    {
        if(firstTime)
        {
          GameObject col= Instantiate(prefabCollectible, transform.position + offset, Quaternion.identity);
          col.GetComponent<ParabolaController>().ParabolaRoot = parabol;
        }
    }

   
}

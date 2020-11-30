using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    GameObject prefabCollectible;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateCol(bool firstTime,Vector3 offset)
    {
        if(firstTime)
        {
            Instantiate(prefabCollectible, transform.position + offset, Quaternion.identity);
        }
    }

   
}

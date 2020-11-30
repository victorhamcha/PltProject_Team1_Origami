using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectibles : MonoBehaviour
{
    [HideInInspector] public int collectibles = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Collectible")
        {
            Destroy(collision.gameObject);
            GetCollectible();
        }
    }


    public void GetCollectible()
    {
        collectibles++;
    }

    //vérifer si assez de collectibles pour origami mettre sur le script d'intéraction avec les origamis
    public bool VerifyCollectibles(int colNeeded)
    {
        if(collectibles>=colNeeded)
        {
            collectibles -= colNeeded;
            return true;
        }
        else
        {
            return false;
        }


    }

}

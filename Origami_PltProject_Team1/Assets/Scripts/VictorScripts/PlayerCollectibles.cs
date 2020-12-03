using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectibles : MonoBehaviour
{
    [HideInInspector] public int collectibles = 0;
    
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

    
    public bool VerifyCollectibles(int colNeeded)
    {
        if(collectibles >= colNeeded)
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

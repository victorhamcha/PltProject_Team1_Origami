using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectibles : MonoBehaviour
{
    [HideInInspector] public int collectibles = 0;
    Entity entity;

    private void Awake()
    {
        entity = gameObject.GetComponent<Entity>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collectible")
        {
            Destroy(other.gameObject);
            GetCollectible();
        }
    }

    public void GetCollectible()
    {
        entity.collectibles++;
    }

    
    public bool VerifyCollectibles(int colNeeded)
    {
        if(collectibles >= colNeeded)
        {
            entity.collectibles -= colNeeded;
            return true;
        }
        else
        {
            return false;
        }
    }

}

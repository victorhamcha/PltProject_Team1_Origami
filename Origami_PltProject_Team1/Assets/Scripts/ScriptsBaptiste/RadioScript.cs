using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioScript : MonoBehaviour
{
    [SerializeField]
    private AudioSource RadioSource;

    [SerializeField]
    private AudioClip[] RadioClips;
   
    // Faire commencer la radio


    private void RadioOn()
    {
        int ClipIndex = Random.Range(0, RadioClips.Length);
        RadioSource.PlayOneShot(RadioClips[ClipIndex]);
    }
}

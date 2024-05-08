using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    [SerializeField] AudioClip bossFight;
    AudioSource src;
    void Start()
    {
        src = GetComponent<AudioSource>();
    }

    public void PlayBossFight(){
        src.clip = bossFight;
        src.Play();
    }
}

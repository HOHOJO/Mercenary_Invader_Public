using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSound : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();
    //AudioSource audioSource;

    public enum Sound
    {
        idle,
        walk,
        run,
        Attack1,
        Attack2,
        Attack3,
        Hit,
        MaxCount,
    }
    // Start is called before the first frame update
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void PlaySound()
    //{
    //    audioSource.Play();
    //}

    //public void PlayStop()
    //{
    //    audioSource.Stop();
    //}

    //public void ClipSet(string s)
    //{
    //    audioSource.Stop();
    //    switch (s) 
    //    {
    //        case "idle":
    //            audioSource.clip = audioClips[0];
    //            break;
    //        case "walk":
    //            audioSource.clip = audioClips[1];
    //            break;
    //        case "run":
    //            audioSource.clip = audioClips[2];
    //            break;
    //        case "Attack1":
    //            audioSource.clip = audioClips[3];
    //            break;
    //        case "Attack2":
    //            audioSource.clip = audioClips[4];
    //            break;
    //        case "Attack3":
    //            audioSource.clip = audioClips[5];
    //            break;

    //    }
    //}
}

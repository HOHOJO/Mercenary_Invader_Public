using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioClip[] bgmList;

    public static SoundManager Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        BGMPlay(bgmList[0]);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < bgmList.Length; i++)
        {
            if (arg0.name == bgmList[i].name)
            {
                BGMPlay(bgmList[i]);
            }
        }
    }

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();

        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(go, clip.length);
    }

    public void BGMPlay(AudioClip clip)
    {
        if (bgm == null)
        {
            GameObject bgmGO = new GameObject("BGM");
            bgm = bgmGO.AddComponent<AudioSource>();
            bgm.loop = true;
            bgm.volume = 0.8f;
        }

        bgm.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Music")[0];

        bgm.clip = clip;
        bgm.Play();
    }

}

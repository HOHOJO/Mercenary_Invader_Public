using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICredits : MonoBehaviour
{
    [SerializeField] private Button btnBack;
    [SerializeField] AudioClip clickSound;

    private void Start()
    {
        btnBack.onClick.AddListener(Back_Credits);
    }

    void Back_Credits()
    {
        SoundManager.Instance.SFXPlay("btnClick", clickSound);
        var animUI = gameObject.GetComponent<DotweenUI>();
        if (animUI != null)
        {
            animUI.MinFade(gameObject);
        }
    }
}

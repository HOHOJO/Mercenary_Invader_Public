using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    [SerializeField] private Button btnBack;

    private void Start()
    {
        btnBack.onClick.AddListener(Back_Shop);
    }

    void Back_Shop()
    {
        var animUI = gameObject.GetComponent<DotweenUI>();
        if (animUI != null)
        {
            animUI.MinFade(gameObject);
        }
    }
}

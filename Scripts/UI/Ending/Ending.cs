using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    [SerializeField] private Button btnEnd;

    private void Start()
    {
        btnEnd.onClick.AddListener(OpenScene_Lobby);
    }

    void OpenScene_Lobby()
    {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.Lobby);
    }
}

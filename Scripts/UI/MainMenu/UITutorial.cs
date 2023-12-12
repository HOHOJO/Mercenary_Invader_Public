using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITutorial : MonoBehaviour
{
    [SerializeField] private Button btnCancel;
    [SerializeField] AudioClip clickSound;

    private void Start()
    {
        btnCancel.onClick.AddListener(OnCancelCreate);
    }

    void OnCancelCreate()
    {
        SoundManager.Instance.SFXPlay("btnClick", clickSound);
        var animUI = gameObject.GetComponent<DotweenUI>();
        if (animUI != null)
        {
            animUI.MinFade(gameObject);
        }
    }
}

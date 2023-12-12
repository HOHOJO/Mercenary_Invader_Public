using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICreatePlayer : MonoBehaviour
{
    [SerializeField] private Button btnEnterLobby;
    [SerializeField] private Button btnCancel;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] AudioClip clickSound;
    private UISlots slots;

    private void Start()
    {
        slots = FindObjectOfType<UISlots>();

        btnEnterLobby.onClick.AddListener(OpenPopup_EnterLobby);
        btnCancel.onClick.AddListener(OnCancelCreate);

        btnEnterLobby.interactable = false;
        inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
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

    void OpenPopup_EnterLobby()
    {
        UIPopup popup = UIManager.Instance.OpenUI<UIPopup>(1);
        popup.SetPopup("플레이어 생성", "플레이어를 생성하시겠습니까?", () => { slots.EnterLobby(); });
    }

    void OnInputFieldValueChanged(string value)
    {
        btnEnterLobby.interactable = !string.IsNullOrEmpty(value);
    }
}

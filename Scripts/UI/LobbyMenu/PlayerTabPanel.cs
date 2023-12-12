using UnityEngine;
using UnityEngine.UI;

public class PanelControl : MonoBehaviour
{
    public GameObject[] panels;
    public Button[] tabButtons;

    private void Start()
    {
        for (int i = 0; i < tabButtons.Length; i++)
        {
            int panelIndex = i;
            tabButtons[i].onClick.AddListener(() => TogglePanel(panelIndex));
        }
    }

    void TogglePanel(int panelIndex)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }

        panels[panelIndex].SetActive(true);
    }
}

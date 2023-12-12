using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabPanel : MonoBehaviour
{
    [SerializeField] private List<TabBtn> tabBtns;
    [SerializeField] private List<GameObject> contentPanels;

    private void Start()
    {
        Tab(0);    
    }

    public void Tab(int id)
    {
        for (int i = 0; i < contentPanels.Count; i++)
        {
            if(i == id)
            {
                contentPanels[i].SetActive(true);
                tabBtns[i].Selected();
            }
            else
            {
                contentPanels[i].SetActive(false);
                tabBtns[i].UnSelected();
            }
        }
    }
}

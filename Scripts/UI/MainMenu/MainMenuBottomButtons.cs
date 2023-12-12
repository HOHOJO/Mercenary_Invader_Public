using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBottomButtons : MonoBehaviour
{
    public List<RectTransform> btnList;

    private void Start()
    {
        StartCoroutine(IE_Display(0.3f, 0.1f));
    }

    IEnumerator IE_Display(float delayStart, float delayDisplay)
    {
        yield return new WaitForSeconds(delayStart);
        for (int i = 0; i < btnList.Count; i++)
        {
            btnList[i].DOMoveY(80, 0.6f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(delayDisplay);
        }
    }
}

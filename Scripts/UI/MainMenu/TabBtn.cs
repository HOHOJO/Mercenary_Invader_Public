using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabBtn : MonoBehaviour
{
    private Image bgImage;
    [SerializeField] private Sprite originalImage;
    [SerializeField] private Sprite selectedImage;

    private void Start()
    {
        bgImage = GetComponent<Image>();
    }

    public void Selected()
    {
        if (bgImage != null)
        {
            bgImage.sprite = selectedImage;
        }
    }

    public void UnSelected()
    {
        if (bgImage != null)
        {
            bgImage.sprite = originalImage;
        }
    }
}

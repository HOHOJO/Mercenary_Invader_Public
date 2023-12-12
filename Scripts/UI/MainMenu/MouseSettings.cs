using ProBuilder.Examples;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MouseSettings : MonoBehaviour
{
    [SerializeField] private Slider speedSlider;
    [SerializeField] private TMP_Text speedLabel;
    [SerializeField] private MoveTowards cameraControl;

    void Start()
    {
        if (speedSlider != null)
        {
            speedSlider.minValue = 1.0f;
            speedSlider.maxValue = 100.0f;
            speedSlider.value = 50.0f;
        }

        UpdateSpeedLabel();
    }

    void Update()
    {
        if (cameraControl != null)
        {
            float sliderValue = speedSlider.value / 100.0f;
            cameraControl.xSpeed = sliderValue * cameraControl.maxSpeed;
            cameraControl.ySpeed = sliderValue * cameraControl.maxSpeed;
        }

        UpdateSpeedLabel();
    }

    void UpdateSpeedLabel()
    {
        if (speedLabel != null)
        {
            speedLabel.text = Mathf.RoundToInt(speedSlider.value).ToString();
        }
    }
}

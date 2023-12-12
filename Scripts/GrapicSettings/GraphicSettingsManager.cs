using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static UnityEngine.Rendering.DebugUI;

public class GraphicSettingsManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown textureQualityDropdown;
    [SerializeField] private TMP_Dropdown antialiasingDropdown;
    [SerializeField] private TMP_Dropdown shadowQualityDropdown;

    [SerializeField] private GameGraphicSettings gameGraphicSettings;

    [SerializeField] private UniversalRenderPipelineAsset urpAsset;

    private void OnEnable()
    {
        gameGraphicSettings = new GameGraphicSettings();

        urpAsset = GraphicsSettings.renderPipelineAsset as UniversalRenderPipelineAsset;

        textureQualityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        antialiasingDropdown.onValueChanged.AddListener(delegate { OnAntialiasingChange(); });
        shadowQualityDropdown.onValueChanged.AddListener(delegate { OnShadowQualityChange(); });
    }

    public void OnTextureQualityChange()
    {
        QualitySettings.globalTextureMipmapLimit = textureQualityDropdown.value;
        gameGraphicSettings.textureQuality = textureQualityDropdown.value;
    }

    public void OnAntialiasingChange()
    {
        int antialiasingValue = (int)Mathf.Pow(2f, antialiasingDropdown.value);
        gameGraphicSettings.antialiasing = antialiasingValue;
        urpAsset.msaaSampleCount = antialiasingValue;
    }

    public void OnShadowQualityChange()
    {
        int shadowQuality = shadowQualityDropdown.value;
        gameGraphicSettings.shadowQuality = shadowQuality;

        switch (shadowQuality)
        {
            case 0:
                urpAsset.shadowCascadeCount = 4;
                break;
            case 1:
                urpAsset.shadowCascadeCount = 2;
                break;
            case 2:
                urpAsset.shadowCascadeCount = 1;
                break;
        }
    }
}

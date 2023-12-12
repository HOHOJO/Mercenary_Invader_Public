using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;
using static UnityEngine.Rendering.DebugUI;

public class HPbar : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    [SerializeField] private Player player;
    public Text HP;

    public void SetMaxHealth(float health)
    {
        _slider.maxValue = health;
        _slider.value = health;
    }
    public void SetValue(float value)
    {
        _slider.value = value;
    }

    public void Start()
    {
        player = Player.FindAnyObjectByType<Player>();
        _slider.maxValue = player.maxHealth;
        _slider.value = player.health;
    }

    public void Update()
    {
        _slider.value = player.health;
    }

    private void LateUpdate()
    {
        HP.text = player.health + "/" + player.maxHealth;
    }
}

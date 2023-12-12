using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Monster_HpBar : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    [SerializeField] private Monster monster;
    [SerializeField] private Monster2 monster2;

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
        monster = Monster.FindAnyObjectByType<Monster>();
        if(monster == null ) 
        { 
            monster2 = Monster2.FindAnyObjectByType<Monster2>();
            _slider.maxValue = monster2.Data.hp;
            _slider.value = monster2.health;
        }
        else
        {
            _slider.maxValue = monster.Data.hp;
            _slider.value = monster.health;
        }
    }

    public void Update()
    {
        if (monster == null)
        {
            _slider.value = monster2.health;
        }
        else
        {
            _slider.value = monster.health;
        }
    }
}

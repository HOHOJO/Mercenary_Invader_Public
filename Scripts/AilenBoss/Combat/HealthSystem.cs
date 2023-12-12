using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 300;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private bool isPlayer = false;

    public int health;
    public event Action OnTakeDamage;
    public event Action OnDie;

    private HealthStateObserver healthObserver = new HealthStateObserver();

    public HealthState CurrentHealthState
    {
        get
        {
            if (health <= 0)
            {
                HandleDeath();
                OnDie?.Invoke();
                return HealthState.Critical;
            }
            else if (health <= 400)
            {
                return HealthState.Low;
            }
            else
            {
                return HealthState.Normal;
            }
        }
    }

    private void Start()
    {
        health = maxHealth;
        UpdateHealthBar();
        UpdateHealthText();
    }

    private void UpdateHealthBar()
    {
        if (healthSlider != null)
        {
            healthSlider.value = health;
        }
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = $"{health} / {maxHealth}";
        }
    }

    public void DealDamage(int damage)
    {
        if (health <= 0)
        {
            HandleDeath();
        }

        health = Mathf.Max(health - damage, 0);

        OnTakeDamage?.Invoke();
        healthObserver.Notify(CurrentHealthState);

        UpdateHealthBar();
        UpdateHealthText();
        Debug.Log(health);
    }

    private void HandleDeath()
    {
        if (isPlayer)
        {
            GameManager.Instance.GameOver(false);
        }
        else
        {
            Debug.Log("Win");
            GameManager.Instance.GameOver(true);
        }
    }

    public void SubscribeToHealthChanges(IHealthObserver observer)
    {
        healthObserver.Subscribe(observer);
    }

    public void UnsubscribeFromHealthChanges(IHealthObserver observer)
    {
        healthObserver.Unsubscribe(observer);
    }
}

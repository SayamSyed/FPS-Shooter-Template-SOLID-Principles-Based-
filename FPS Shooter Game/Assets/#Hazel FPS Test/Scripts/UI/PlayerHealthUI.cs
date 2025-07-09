using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image healthFillImage;
    private HealthComponent healthComponent;

    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
    }

    private void OnEnable()
    {
        healthComponent.OnDamaged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        healthComponent.OnDamaged -= UpdateHealthBar;
    }

    private void UpdateHealthBar()
    {
        float normalized = healthComponent.CurrentHealth / healthComponent.MaxHealth;
        healthFillImage.fillAmount = normalized;
    }
}
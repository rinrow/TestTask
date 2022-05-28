using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Slider _healthBar;

    private void OnEnable()
    {
        _enemy.OnHealthValueChanged += ChangeBarValue;
    }

    private void OnDisable()
    {
        _enemy.OnHealthValueChanged -= ChangeBarValue;
    }

    private void ChangeBarValue(float maxHealth, float currentHealth)
    {
        _healthBar.value = currentHealth / maxHealth;
    }
}

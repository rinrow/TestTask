using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private Rigidbody[] _rigidbodies;
    [SerializeField] private Animator _animator;

    private float _currentHealth;
    private BoxCollider _boxCollider;

    public event Action<Enemy> OnEnemyDieng;
    public event Action<float, float> OnHealthValueChanged;
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();

        foreach (var rigidbody in _rigidbodies)
            rigidbody.isKinematic = true;
        _animator.Play("Idle");
        _currentHealth = _maxHealth;
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        OnHealthValueChanged(_maxHealth, _currentHealth);

        if (_currentHealth <= 0)
        {
            Dying();
        }
    }

    [ContextMenu("Kill")]
    public void Kill()
    {
        Dying();
    }

    private void Dying()
    {
        _boxCollider.enabled = false;
        _animator.enabled = false;
        foreach (var rigidbody in _rigidbodies)
            rigidbody.isKinematic = false;
        StartCoroutine(DiedAfterSeconds(3));
    }

    private IEnumerator DiedAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        OnEnemyDieng(this);
        Destroy(gameObject);
    }
}

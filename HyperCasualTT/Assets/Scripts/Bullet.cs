using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int _damage;

    public void Init(int damage)
    {
        _damage = damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<Enemy>(out Enemy enemy))
            enemy.ApplyDamage(_damage);
        if (!collision.gameObject.CompareTag("IgnoreCollision"))
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
            enemy.ApplyDamage(_damage);
    }
}

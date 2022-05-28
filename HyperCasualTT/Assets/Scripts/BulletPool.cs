using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet _objectToPool;
    [SerializeField] private int _poolCount;

    private List<Bullet> _pulledObjects;

    public static BulletPool Instance { get; private set; }

    private void Start()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        _pulledObjects = new List<Bullet>();

        for (int i = 0; i < _poolCount; i++)
        {
            var temporary = Instantiate(_objectToPool, transform);
            temporary.gameObject.SetActive(false);
            _pulledObjects.Add(temporary);
        }
    }

    public bool TryGetBullet(out Bullet bullet, Vector3 position)
    {
        bullet = null;
        foreach (var item in _pulledObjects)
            if (!item.gameObject.activeInHierarchy)
            {
                bullet = item;
                bullet.transform.position = position;
                bullet.gameObject.SetActive(true);
                return true;
            }
        return false;
    }
}

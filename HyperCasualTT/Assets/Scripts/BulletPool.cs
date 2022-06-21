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
    }

    private void InitBullet()
    {
        var temporary = Instantiate(_objectToPool, transform);
        temporary.gameObject.SetActive(false);
        _pulledObjects.Add(temporary);
    }

    public Bullet GetBullet(Vector3 position)
    {
        foreach (var item in _pulledObjects)
            if (!item.gameObject.activeInHierarchy)
            {
                item.transform.position = position;
                item.gameObject.SetActive(true);
                return item;
            }

        _poolCount++;
        InitBullet();
        return _pulledObjects[_pulledObjects.Count - 1];
    }
}

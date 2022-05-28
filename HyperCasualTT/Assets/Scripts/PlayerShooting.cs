using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Range(0.5f, 15f)]
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private float _shootingGup;

    private float _time = 0;

    void Update()
    {
        _time += Time.deltaTime;
        if (Input.touchCount == 1 && _time > _shootingGup)
        {
            var touch = Input.GetTouch(0);
            var r = Camera.main.ScreenPointToRay(touch.position);
            if (Physics.Raycast(r, out RaycastHit hit))
            {
                _time = 0;

                Shoot(hit.point);
            }
        }
    }

    private void Shoot(Vector3 direction)
    {
        StartCoroutine(LaunchABullet(direction));
    }

    IEnumerator LaunchABullet(Vector3 direction)
    {
        print(direction);
        Bullet bullet;
        if (!BulletPool.Instance.TryGetBullet(out bullet, transform.position)) yield break;

        bullet.Init(_damage);
        var delta = 0f;
        while (delta < 1/_speed)
        {
            yield return null;

            if (bullet == null)
                yield break;

            delta += Time.deltaTime;
            bullet.transform.position = Vector3.Lerp(transform.position, direction, delta * _speed);
        }
    }
}

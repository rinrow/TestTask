using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Range(0.5f, 2.5f)]
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private float _shootingGup;

    float _time = 0;

    void Update()
    {
        _time += Time.deltaTime;
        if (Input.touchCount == 1 && _time > _shootingGup)
        {
            var touch = Input.GetTouch(0);
            var r = Camera.main.ScreenPointToRay(touch.position);
            Debug.DrawRay(r.origin, r.direction * 100, Color.red);
            if (Physics.Raycast(r, out RaycastHit hit))
            {
                _time = 0;

                Shoot(hit.point);
            }
        }

        if (Input.GetMouseButtonDown(0) && _time > _shootingGup)
        {
            var r = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(r.origin, r.direction * 100, Color.red);
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
        while (delta < 1)
        {
            yield return null;

            if (bullet == null)
                yield break;

            delta += Time.deltaTime;
            bullet.transform.position = Vector3.Lerp(transform.position, direction * _speed, delta);
            Debug.DrawRay(transform.position, direction * 100, Color.black);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Range(0.5f, 10f)]
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private float _shootingGup;

    private float _time = 0;

    void Update()
    {
        _time += Time.deltaTime;

        //Для мобилок
        if (Input.touchCount == 1 && _time > _shootingGup)
        {
            var touchPos = Input.GetTouch(0).position;
            var correctTouchPos = new Vector3(touchPos.x, touchPos.y, 25);
            var shootPoint = Camera.main.ScreenToWorldPoint(correctTouchPos);
            Shoot(shootPoint);
        }

        //Для пк
        if (Input.GetMouseButtonDown(0))
        {
            var correctMousePos = Input.mousePosition;
            correctMousePos.z = 25;
            var shootPoint = Camera.main.ScreenToWorldPoint(correctMousePos);
            Shoot(shootPoint);
        }
    }

    private void Shoot(Vector3 direction)
    {
        StartCoroutine(LaunchABullet(direction));
    }

    IEnumerator LaunchABullet(Vector3 direction)
    {
        print(direction);
        var bullet = BulletPool.Instance.GetBullet(transform.position); ;

        bullet.Init(_damage);
        var delta = 0f;
        while (delta < 1 / _speed)
        {
            yield return null;

            if (bullet == null)
                yield break;

            delta += Time.deltaTime;
            bullet.transform.position = Vector3.Lerp(transform.position, direction, delta * _speed);
        }
        bullet.gameObject.SetActive(false);
    }
}

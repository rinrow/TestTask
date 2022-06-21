using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            _playerMovement.GoTotTheNextPoint();
            Destroy(gameObject);
        }

        if (Input.GetMouseButtonDown(0))
        {
            _playerMovement.GoTotTheNextPoint();
            Destroy(gameObject);
        }
    }
}

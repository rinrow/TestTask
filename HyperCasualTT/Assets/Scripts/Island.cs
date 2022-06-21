using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    private List<Enemy> _enemies;
    private int diedsCount = 0;

    void Start()
    {
        _enemies = GetComponentsInChildren<Enemy>().ToList();

        foreach (var enemy in _enemies)
        {
            //Observer pattern
            enemy.OnEnemyDieng += EnemyDying;
        }
    }

    private void EnemyDying(Enemy enemy)
    {
        diedsCount++;
        if(diedsCount == _enemies.Count)
            _playerMovement.GoTotTheNextPoint();
        enemy.OnEnemyDieng -= EnemyDying;
    }
}

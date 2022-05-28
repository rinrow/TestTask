using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _player;
    [SerializeField] private List<Transform> _points;
    [SerializeField] private Animator _playerAnimator;

    private int _currentPoinNum = 0;

    public void GoTotTheNextPoint()
    {
        if (_currentPoinNum == _points.Count)
        {
            SceneReloader.Restart();
            return;
        }
        var target = _points[_currentPoinNum].position;
        _player.SetDestination(target);
        _playerAnimator.Play("Run");
        StartCoroutine(StopAnimationWhenInTheCurrentPoint(target));
        _currentPoinNum++;
    }

    private IEnumerator StopAnimationWhenInTheCurrentPoint(Vector3 targetPoint)
    {
        while (true)
        {
            yield return null;
            if (transform.position.x == targetPoint.x)
            {
                _playerAnimator.Play("Idle");
                yield break;
            }
        }
    }
}

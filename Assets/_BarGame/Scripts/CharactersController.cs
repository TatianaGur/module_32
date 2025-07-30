using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CharactersController : MonoBehaviour
{
    [SerializeField] private Transform _currentTarget;
    [SerializeField] private Transform[] _targets;

    [SerializeField] private NavMeshAgent _agent;

    private bool isWalking;


    private void Start()
    {
        if (_targets.Length == 0) Debug.LogError("No targets!");

        SelectNewTarget();
    }

    private void Update()
    {
        if (_agent.isActiveAndEnabled && _currentTarget != null && isWalking)
        {
            if (HasPathReady() && HasReachedDestination())
            {
                isWalking = false;

                StartCoroutine(WaitRandomSeconds());

                //SelectNewTarget();

                //isWalking = true;
            }
        }
    }

    private bool HasPathReady()
    {
        return !_agent.pathPending;
    }

    private bool HasReachedDestination()
    {
        return _agent.remainingDistance <= _agent.stoppingDistance;
    }

    private void SelectNewTarget()
    {
        _currentTarget = _targets[Random.Range(0, _targets.Length)];

        _agent.SetDestination(_currentTarget.position);
    }

    private IEnumerator WaitRandomSeconds()
    {
        float waitingTime = Random.Range(10, 31);

        Debug.Log($"waitingTime = {waitingTime}");

        yield return new WaitForSeconds(waitingTime);

        SelectNewTarget();

        isWalking = true;
    }
}

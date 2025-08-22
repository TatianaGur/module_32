using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using VContainer;

public class SecutityController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;

    private Transform _currentTarget;
    
    [SerializeField] private Transform[] _targets;

    private bool isWalking;
    private InDanceFloorTrigger _inDanceFloorTrigger;


    private void Start()
    {
        _targets = new Transform[0];

        if (_agent == null) Debug.LogError("No _agent!");

        isWalking = true;
    }

    [Inject]
    public void Construct(InDanceFloorTrigger inDanceFloorTrigger)
    {
        Debug.Log("Construct вызван");///??????не вызывается

        _inDanceFloorTrigger = inDanceFloorTrigger;

        if (_inDanceFloorTrigger == null) Debug.LogError("_inDanceFloorTrigger is null");

        TargetsArrayChanged();//можно ли здесь в Construct?
    }

    private void Update()
    {
        if (_agent.isActiveAndEnabled && _currentTarget != null && isWalking)
        {
            if (HasPathReady() && HasReachedDestination())
            {
                isWalking = false;

                StartCoroutine(WaitRandomSeconds());




                for (int i = 0; i < _targets.Length; i++)
                    Debug.Log(_targets[i].name);
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
        if (_targets == null || _targets.Length == 0) Debug.LogWarning("No targets available for security!");
        else
        {
            _currentTarget = _targets[Random.Range(0, _targets.Length)];

            _agent.SetDestination(_currentTarget.position);
        }
    }

    private IEnumerator WaitRandomSeconds()
    {
        float waitingTime = Random.Range(2, 9);

        yield return new WaitForSeconds(waitingTime);

        SelectNewTarget();

        isWalking = true;
    }

    private void OnEnable()
    {
        if (_inDanceFloorTrigger != null) _inDanceFloorTrigger.TargetsArrayChangedEvent += TargetsArrayChanged;
        else Debug.Log("InDanceTrigger = null");
    }

    private void OnDisable()
    {
        if (_inDanceFloorTrigger != null) _inDanceFloorTrigger.TargetsArrayChangedEvent -= TargetsArrayChanged;
    }

    private void TargetsArrayChanged()
    {
        if (_inDanceFloorTrigger != null) _targets = _inDanceFloorTrigger.SecurityTargets;

        if (_agent.isActiveAndEnabled && _currentTarget != null && isWalking && _targets.Length > 0)
        {
            if (HasPathReady() && HasReachedDestination())
            {
                isWalking = false;

                StartCoroutine(WaitRandomSeconds());
            }
        }
    }
}

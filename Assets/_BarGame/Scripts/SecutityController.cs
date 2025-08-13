using UnityEngine;
using UnityEngine.AI;
using System.Collections;
//using VContainer;
using Zenject;

public class SecutityController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;

    private Transform _currentTarget;
    private Transform[] _targets;

    private bool isWalking;
    private InDanceFloorTrigger _inDanceFloorTrigger;


    private SignalBus _signalBus;


    private void Start()
    {
        _targets = new Transform[_targets.Length];
        if (_targets.Length == 0) Debug.LogError("No targets!");

        SelectNewTarget();

        isWalking = true;
    }

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
        _signalBus.Subscribe<TargetsArrayChangedSignal>(TargetsArrayChanged);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<InDanceFloorTrigger>(out var inDanceFloorTrigger))
        {
            _targets = inDanceFloorTrigger.SecurityTargets;
            Debug.Log("InDanceFloorTrigger was found in SecurityController");
        }
    }

    /*[Inject]
    public void Construct(InDanceFloorTrigger inDanceFloorTrigger)
    {
        _inDanceFloorTrigger = inDanceFloorTrigger;
    }*/

    private void Update()
    {
        if (_agent.isActiveAndEnabled && _currentTarget != null && isWalking)
        {
            if (HasPathReady() && HasReachedDestination())
            {
                isWalking = false;

                StartCoroutine(WaitRandomSeconds());
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
        float waitingTime = Random.Range(2, 9);

        yield return new WaitForSeconds(waitingTime);

        SelectNewTarget();

        isWalking = true;
    }

    private void OnEnable()
    {
        //_inDanceFloorTrigger.TargetsArrayChangedEvent += TargetsArrayChanged;
    }

    private void OnDisable()
    {
        //_inDanceFloorTrigger.TargetsArrayChangedEvent -= TargetsArrayChanged;
    }

    private void TargetsArrayChanged()
    {
        _targets = _inDanceFloorTrigger.SecurityTargets;

        if (_agent.isActiveAndEnabled && _currentTarget != null && isWalking)
        {
            if (HasPathReady() && HasReachedDestination())
            {
                isWalking = false;

                StartCoroutine(WaitRandomSeconds());
            }
        }
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<TargetsArrayChangedSignal>(TargetsArrayChanged);
    }
}

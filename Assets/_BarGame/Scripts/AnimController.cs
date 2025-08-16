using UnityEngine;
using UnityEngine.AI;

public class AnimController : MonoBehaviour
{
    [SerializeField] private GameObject _cocktail;
    [SerializeField] private Transform _tableForLookingAt;

    private NavMeshAgent _agent;
    private Animator _anim;

    private float rotationSpeed;
    private bool isMoving;
    private bool readyToSit;

    private bool inDanceFloor;
    private bool inLoungeZone;
    private bool inBarCounterZone;
    private bool inHallZone;


    private void Start()
    {
        rotationSpeed = 10f;

        _anim = GetComponent<Animator>();
        if (_anim == null) Debug.Log("Anim = null");

        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null) Debug.Log("_agent == null");

        isMoving = false;
        readyToSit = true;

        inDanceFloor = false;
        inLoungeZone = false;
        inBarCounterZone = false;
        inHallZone = false;
    }

    private void FixedUpdate()
    {
        IsMovingBool();

        AnimateCharacter();
    }

    private void IsMovingBool()
    {
        if (_agent.velocity.magnitude >= 0.01f)
        {
            isMoving = true;
            ChangeDirection();
        }

        else isMoving = false;
    }

    private void AnimateCharacter()
    {
        if (isMoving && inLoungeZone)
        {
            readyToSit = true;
            _anim.SetBool("IsSitting", false);
            _anim.SetTrigger("StandUp");
        }


        if (isMoving) _anim.SetBool("IsMoving", true);

        

        else if (!isMoving)
        {
            _anim.SetBool("IsMoving", false);

            if (inDanceFloor) _anim.SetBool("InDanceFloor", true);
            else _anim.SetBool("InDanceFloor", false);
            if (inHallZone) _anim.SetBool("InHallZone", true);
            else _anim.SetBool("InHallZone", false);

            if (inBarCounterZone)
            {
                _anim.SetBool("InBarCounterZone", true);
                _cocktail.SetActive(true);
            }
            else
            {
                _anim.SetBool("InBarCounterZone", false);
                _cocktail.SetActive(false);
            }

            if (inLoungeZone)
            {
                _anim.SetBool("InLoungeZone", true);

                if (readyToSit)
                {
                    if (_tableForLookingAt != null) transform.LookAt(_tableForLookingAt.position);
                    _anim.SetTrigger("SitDown");
                    readyToSit = false;
                }

                if (!readyToSit) _anim.SetBool("IsSitting", true);
            }
        }
    }

    private void ChangeDirection()
    {
        Vector3 velocity = _agent.velocity;
        velocity.y = 0;

        Vector3 currentDirection = velocity.normalized;
        Quaternion currentRotation = transform.rotation;

        Quaternion targetRotation = Quaternion.LookRotation(currentDirection);

        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationSpeed);
    }

    public void InDanceFloor()
    {
        inDanceFloor = true;
        inLoungeZone = false;
        inBarCounterZone = false;
        inHallZone = false;
    }

    public void InHallZone()
    {
        inDanceFloor = false;
        inLoungeZone = false;
        inBarCounterZone = false;
        inHallZone = true;
    }

    public void InBarCounterZone()
    {
        inDanceFloor = false;
        inLoungeZone = false;
        inBarCounterZone = true;
        inHallZone = false;
    }

    public void InLoungeZone()
    {
        inDanceFloor = false;
        inLoungeZone = true;
        inBarCounterZone = false;
        inHallZone = false;
    }
}

using UnityEngine;
using UnityEngine.AI;

public class AnimController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private GameObject _cocktail;
    [SerializeField] private Transform _tableForLookingAt;

    private Animator anim;
    private InDanceFloorTrigger _inDanceFloorTrigger;

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

        anim = GetComponent<Animator>();
        if (anim == null) Debug.Log("Anim = null");

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
            anim.SetBool("IsSitting", false);
            anim.SetTrigger("StandUp");
        }


        if (isMoving) anim.SetBool("IsMoving", true);

        

        else if (!isMoving)
        {
            anim.SetBool("IsMoving", false);

            if (inDanceFloor) anim.SetBool("InDanceFloor", true);
            else anim.SetBool("InDanceFloor", false);
            if (inHallZone) anim.SetBool("InHallZone", true);
            else anim.SetBool("InHallZone", false);

            if (inBarCounterZone)
            {
                anim.SetBool("InBarCounterZone", true);
                _cocktail.SetActive(true);
            }
            else
            {
                anim.SetBool("InBarCounterZone", false);
                _cocktail.SetActive(false);
            }

            if (inLoungeZone)
            {
                anim.SetBool("InLoungeZone", true);

                if (readyToSit)
                {
                    if (_tableForLookingAt != null) transform.LookAt(_tableForLookingAt.position);
                    anim.SetTrigger("SitDown");
                    readyToSit = false;
                }

                if (!readyToSit) anim.SetBool("IsSitting", true);

                
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

using UnityEngine;
using UnityEngine.AI;

public class AnimController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;

    private Animator anim;
    private InDanceFloorTrigger _inDanceFloorTrigger;

    private float rotationSpeed;
    private bool isMoving;
    private bool isSitting;

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
        isSitting = false;

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

        //Debug.Log($"isMoving = {isMoving}");
    }

    private void AnimateCharacter()
    {
        if (isMoving) anim.SetBool("IsMoving", true);
        else anim.SetBool("IsMoving", false);

        if (!isMoving && inDanceFloor) anim.SetBool("InDanceFloor", true);
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

    public void InHallFloor()
    {
        inDanceFloor = false;
        inLoungeZone = false;
        inBarCounterZone = false;
        inHallZone = true;
    }
}

using UnityEngine;

public class InDanceFloorTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<AnimController>(out var animController))
        {
            animController.InDanceFloor();

            Debug.Log("inDanceFloor Trigger works!");
        }
    }
}

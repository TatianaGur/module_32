using UnityEngine;

public class InHallZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("In HallZoneTrigger");

        if (other.gameObject.TryGetComponent<AnimController>(out var animController))
        {
            animController.InHallZone();

            Debug.Log("inHallZoneTrigger works!");
        }
    }
}

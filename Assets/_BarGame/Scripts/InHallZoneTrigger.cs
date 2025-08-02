using UnityEngine;

public class InHallZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<AnimController>(out var animController))
        {
            animController.InHallZone();

            Debug.Log("inHallZoneTrigger works!");
        }
    }
}

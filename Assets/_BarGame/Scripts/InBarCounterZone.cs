using UnityEngine;

public class InBarCounterZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("In BarCounterZoneTrigger");

        if (other.gameObject.TryGetComponent<AnimController>(out var animController))
        {
            animController.InBarCounterZone();

            Debug.Log("BarCounterZoneTrigger works!");
        }
    }
}

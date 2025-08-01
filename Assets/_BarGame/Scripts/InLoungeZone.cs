using UnityEngine;

public class InLoungeZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("In LoungeZoneTrigger");

        if (other.gameObject.TryGetComponent<AnimController>(out var animController))
        {
            animController.InLoungeZone();

            Debug.Log("LoungeZoneTrigger works!");
        }
    }
}

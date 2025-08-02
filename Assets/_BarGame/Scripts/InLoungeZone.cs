using UnityEngine;

public class InLoungeZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<AnimController>(out var animController))
        {
            animController.InLoungeZone();
        }
    }
}

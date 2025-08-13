using System.Collections.Generic;
using UnityEngine;

public class InDanceFloorTrigger : MonoBehaviour
{
    public delegate void TargetsArrayChangedDelegate();
    public event TargetsArrayChangedDelegate TargetsArrayChangedEvent;



    public Transform[] SecurityTargets;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<AnimController>(out var animController))
        {
            animController.InDanceFloor();



            if (animController != null)
            {
                AddToSecurityTargets(other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<AnimController>(out var animController) && animController != null)
        {
            RemoveFromSecurityTargets(other.transform);
        }
    }

    private void AddToSecurityTargets(Transform securityTargets)
    {
        List<Transform> targetsList = new List<Transform>(SecurityTargets);

        if (!targetsList.Contains(securityTargets)) targetsList.Add(securityTargets);

        SecurityTargets = targetsList.ToArray();

        TargetsArrayChangedEvent?.Invoke();
    }

    private void RemoveFromSecurityTargets(Transform securityTargets)
    {
        List<Transform> targetsList = new List<Transform>(SecurityTargets);

        targetsList.Remove(securityTargets);

        SecurityTargets = targetsList.ToArray();

        TargetsArrayChangedEvent?.Invoke();
    }
}

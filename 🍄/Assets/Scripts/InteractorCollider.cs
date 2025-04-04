using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorCollider : MonoBehaviour
{
    List<Interactable> InteractablesInRange = new();
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Interactable interactable))
        {
            InteractablesInRange.Add(interactable);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Interactable interactable))
        {
            InteractablesInRange.Add(interactable);
        }
    }

    void ShowButtonPromptOnClosestInteractable()
    {
        Interactable closestInteractable;
        float closestSqrDistance = 0;
        foreach (Interactable interactable in InteractablesInRange)
        {
            float sqrDistance = Vector3.SqrMagnitude(interactable.transform.position - transform.position);
            if (sqrDistance < closestSqrDistance)
            {
                closestInteractable = interactable;
                closestSqrDistance = sqrDistance;
            }
        }
    }
}

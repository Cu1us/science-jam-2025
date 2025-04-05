using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorCollider : MonoBehaviour
{
    List<Interactable> InteractablesInRange = new();
    Interactable SelectedInteractable = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Interactable interactable))
        {
            InteractablesInRange.Add(interactable);
            SelectClosestInteractable();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Interactable interactable))
        {
            InteractablesInRange.Remove(interactable);
            SelectClosestInteractable();
        }
    }

    void SelectClosestInteractable()
    {
        Interactable closestInteractable = null;
        float closestSqrDistance = 0;
        foreach (Interactable interactable in InteractablesInRange)
        {
            interactable.SetButtonPromptVisible(false);
            float sqrDistance = Vector3.SqrMagnitude(interactable.transform.position - transform.position);
            if (sqrDistance < closestSqrDistance)
            {
                closestInteractable = interactable;
                closestSqrDistance = sqrDistance;
            }
        }
        if (closestInteractable)
        {
            closestInteractable.SetButtonPromptVisible(true);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact") && SelectedInteractable != null)
        {
            SelectedInteractable.InteractEnd();
        }
        if (Input.GetButtonUp("Interact") && SelectedInteractable != null)
        {
            SelectedInteractable.InteractStart();
            SelectClosestInteractable();
        }
    }
}

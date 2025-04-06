using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] GameObject ButtonPrompt;

    protected bool isInteractable;
    public abstract void InteractStart();
    public abstract void InteractEnd();

    public void SetButtonPromptVisible(bool visible)
    {
        if (!isInteractable)
            return;
        ButtonPrompt.SetActive(visible);
    }
}

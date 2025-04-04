using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] GameObject ButtonPrompt;

    public abstract void Interact();

    public void SetButtonPromptVisible(bool visible)
    {
        ButtonPrompt.SetActive(visible);
    }
}

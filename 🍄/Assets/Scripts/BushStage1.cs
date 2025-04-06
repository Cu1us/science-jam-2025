using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushStage1 : Interactable
{
    [SerializeField] float interactionTimer;
    [SerializeField] IntReference bushCounter;
    [SerializeField] GameEvent reverter;
    public override void InteractEnd()
    {
        Debug.Log("wow, the button ends");
        CancelInvoke(nameof(SpreadSpores));
    }

    public override void InteractStart()
    {
        if (!isInteractable)
            return;
        Debug.Log("Wow, the button works 1");
        Invoke("SpreadSpores", interactionTimer);
        //Play spore particle effect
    }

    private void SpreadSpores()
    {
        Debug.Log("Wow, the button works");
        GetComponent<AudioSource>().Play();
        bushCounter.Value++;
        GetComponentInParent<Bush>().growing = true;
        SetButtonPromptVisible(false);
        isInteractable = false;
    }
}

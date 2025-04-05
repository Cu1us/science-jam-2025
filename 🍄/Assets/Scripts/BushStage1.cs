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
        CancelInvoke();
    }

    public override void InteractStart()
    {
        Invoke("SpreadSpores", interactionTimer);
        //Play spore particle effect
    }

    private void SpreadSpores()
    {
        bushCounter.Value++;
        GetComponentInParent<Bush>().growing = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : Interactable
{
    [SerializeField] float interactionTimer;
    [SerializeField] IntReference bushCounter;
    public override void InteractEnd()
    {
        CancelInvoke();
    }

    public override void InteractStart()
    {
        Invoke("SpreadSpores", interactionTimer);
    }

    private void SpreadSpores()
    {
        //Write visual component
        bushCounter.Value++;
    }
}

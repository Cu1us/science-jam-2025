using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushStage2 : Interactable
{
    [SerializeField] IntReference steps;
    [SerializeField] int stepRegain = 10;
    public override void InteractEnd()
    {
        CancelInvoke();
    }

    public override void InteractStart()
    {
        if (!isInteractable)
            return;

        steps.Value += stepRegain;
        isInteractable = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp : Interactable
{
    public GameEvent onCampClicked;

    public override void InteractStart()
    {
        onCampClicked?.Invoke();
    }

    public override void InteractEnd()
    {
        
    }
}

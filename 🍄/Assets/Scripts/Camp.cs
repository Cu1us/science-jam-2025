using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp : Interactable
{
    public GameEvent startNextDay;

    public override void InteractStart()
    {
        startNextDay?.Invoke();
    }

    public override void InteractEnd()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp1 : Interactable
{
    public GameEvent startNextDay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void InteractStart()
    {
        startNextDay?.Invoke();
        throw new System.NotImplementedException();

    }

    public override void InteractEnd()
    {
        throw new System.NotImplementedException();
    }
}

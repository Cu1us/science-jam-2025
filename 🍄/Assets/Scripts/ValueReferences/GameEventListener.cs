using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] GameEvent Event;
    [SerializeField] UnityEvent OnInvoked;

    bool Listening = false;

    void Awake()
    {
        if (!Listening)
        {
            Event.Event += Invoked;
        }
    }

    void OnEnable()
    {
        if (!Listening)
        {
            Event.Event += Invoked;
        }
    }
    void OnDisable()
    {
        if (Listening)
        {
            Event.Event -= Invoked;
        }
    }

    void Invoked()
    {
        OnInvoked.Invoke();
    }
}

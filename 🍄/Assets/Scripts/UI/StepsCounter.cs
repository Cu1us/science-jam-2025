using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StepsCounter : MonoBehaviour
{
    [SerializeField] IntReference StepsRef;
    [SerializeField] TextMeshProUGUI Label;

    void Awake()
    {
        StepsRef.OnValueChanged += RemainingStepsUpdated;
    }

    void RemainingStepsUpdated()
    {
        Label.text = StepsRef.Value.ToString();
    }
}

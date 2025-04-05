using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    [SerializeField] private GameObject[] stages;
    private int stage = 0;
    public void UpdateBush()
    {
        if(stage >= 3)
        {
            return;
        }
        stages[stage].SetActive(false);
        stage++;
        stages[stage].SetActive(true);
    }
}

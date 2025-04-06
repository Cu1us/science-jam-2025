using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    [SerializeField] private GameObject[] stages;
    public bool growing = false;
    private int stage = 0;
    public void UpdateBush()
    {
        if(stage >= 2 || !growing)
        {
            return;
        }
        Debug.Log("afdasd");
        stages[stage].SetActive(false);
        stage++;
        stages[stage].SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    public void die()
    {
        canvas.SetActive(true);
        Invoke("LoadScene", 1);
    }
}

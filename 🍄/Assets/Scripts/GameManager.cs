using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] IntReference CurrentDay;
    [SerializeField] GameEvent OnDayProgressed;
    [SerializeField] IntReference RemainingSteps;
    [SerializeField] IntReference BushesGrown;
    [SerializeField] int BaseMaxSteps;
    [SerializeField] int MaxStepsPerBushGrown;

    [SerializeField] GameObject mushroomMask;

    public float zoomSpeed = 5;
    void Awake()
    {
        RefillSteps();
    }
    public void FadeToNextDay()
    {
        Debug.LogWarning("Starting new day!");
        StartCoroutine(BlackScreenZoom(new Vector3(0, 0, 0)));
        RefillSteps();
        CurrentDay.Value++;
        OnDayProgressed?.Invoke();
    }

    void RefillSteps()
    {
        RemainingSteps.Value = BaseMaxSteps + MaxStepsPerBushGrown * BushesGrown.Value;
    }


    IEnumerator BlackScreenZoom(Vector3 targetSize)
    {
        Vector3 initialChildScale = mushroomMask.transform.GetChild(0).localScale;
        const float epsilon = 0.0001f;
        float stepSpeed = zoomSpeed; // how many units per second

        while (Vector3.Distance(mushroomMask.transform.localScale, targetSize) > 0.01f)
        {
            Vector3 currentScale = mushroomMask.transform.localScale;

            // Move the scale consistently
            Vector3 newScale = Vector3.MoveTowards(currentScale, targetSize, stepSpeed * Time.deltaTime);
            mushroomMask.transform.localScale = newScale;

            // Prevent divide by zero
            float safeScaleX = Mathf.Max(newScale.x, epsilon);

            // Inverse scale for child
            Vector3 oppositeScale = initialChildScale * (1f / safeScaleX);
            mushroomMask.transform.GetChild(0).localScale = oppositeScale;

            yield return null;
        }

        // Snap to final size
        mushroomMask.transform.localScale = targetSize;
        float finalSafeScaleX = Mathf.Max(targetSize.x, epsilon);
        mushroomMask.transform.GetChild(0).localScale = initialChildScale * (1f / finalSafeScaleX);

        if (targetSize == new Vector3(0, 0, 0))
        {
            CurrentDay.Value++;
            OnDayProgressed.Invoke();
            RefillSteps();
            StartCoroutine(BlackScreenZoom(new Vector3(40, 40, 40)));
        }

        yield break;


    }





    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene(0);
        }
        /*if (Input.GetKeyDown("o"))
        {
            StartCoroutine(BlackScreenZoom(new Vector3(40, 40, 40)));
        }
        else if (Input.GetKeyDown("l"))
        {
            StartCoroutine(BlackScreenZoom(new Vector3(0, 0, 0)));
        }*/
    }
}

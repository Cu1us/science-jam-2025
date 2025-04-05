using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] IntReference CurrentDay;
    [SerializeField] GameEvent OnDayProgressed;
    [SerializeField] IntReference RemainingSteps;
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
        CurrentDay.Value++;
        OnDayProgressed.Invoke();
        RefillSteps();
    }

    void RefillSteps()
    {
        RemainingSteps.Value = BaseMaxSteps + MaxStepsPerBushGrown;
    }

    IEnumerator BlackScreenZoom(Vector3 targetSize)
    {
        while (Vector3.Distance(mushroomMask.transform.localScale, targetSize) > 0.01f)
        {
            mushroomMask.transform.localScale = Vector3.Lerp(
                mushroomMask.transform.localScale,
                targetSize,
                Time.deltaTime * zoomSpeed
            );
            yield return null;
        }

        // Optionally snap to exact target size at the end
        mushroomMask.transform.localScale = targetSize;
    }


    private void Update()
    {
        if (Input.GetKeyDown("o"))
        {
            StartCoroutine(BlackScreenZoom(new Vector3(1000000, 1000000, 1000000)));
        }
        else if (Input.GetKeyDown("l"))
        {
            StartCoroutine(BlackScreenZoom(new Vector3(0, 0, 0)));
        }
    }
}

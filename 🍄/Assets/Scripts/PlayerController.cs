using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;

public class PlayerController : MonoBehaviour
{
    bool isMoving;
    public float moveDistance;
    public float rotationSpeed;
    public IntReference steps;



    public float desiredFloorDistance = 0.5f;
    public float tolerance = 0.05f;



    public Exhaustion currentExhaustion;

    public float[] movementTimes;

    public Action stepTaken;

    Vector2 inputAxis;



    public enum Exhaustion
    {
        low,
        medium,
        high,
        severe
    }

    Dictionary<Exhaustion, float> movementSpeeds = new Dictionary<Exhaustion, float>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Enum.GetNames(typeof(Exhaustion)).Length; i++)
        {
            movementSpeeds.Add((Exhaustion)i, movementTimes[i]);
        }

    }

    // Update is called once per frame
    void Update()
    {
        inputAxis = new Vector2(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal"));

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + inputAxis.y * rotationSpeed * Time.deltaTime, transform.eulerAngles.z);
        SetDistanceRotation();

        if (isMoving == false && inputAxis.x != 0)
        {
            Debug.Log("calledMovePlayer");
            StartCoroutine(MovePlayer());
        }

    }

    void SetDistanceRotation()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
            {
                Debug.Log("Raycast hit: " + hit.collider.name + " | Distance: " + hit.distance);

                if (Mathf.Abs(hit.distance - desiredFloorDistance) > tolerance)
                {
                    float correction = desiredFloorDistance - hit.distance;
                    transform.position += new Vector3(0f, correction, 0f);
                    Debug.Log("Corrected Y by: " + correction);
                }

                transform.rotation = Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal));
            }
        }
    }


    public IEnumerator MovePlayer()
    {
        stepTaken();

        isMoving = true;
        float movementTime = movementSpeeds[currentExhaustion];
        float currentMovementTime = 0f;//The amount of time that has passed

        Vector3 origin = transform.position;
        Vector3 destination = transform.position + inputAxis.x * transform.forward * moveDistance;

        Debug.Log("Destination is:" + destination);

        while (Vector3.Distance(transform.position, destination) > 0)
        {
            currentMovementTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, destination, currentMovementTime / movementTime);
            yield return null;
        }
        Debug.Log("Done now");
        isMoving = false;
    }
}

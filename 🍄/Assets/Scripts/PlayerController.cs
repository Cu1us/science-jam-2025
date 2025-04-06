using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;

public class PlayerController : MonoBehaviour
{
    bool isMoving;
    public float moveDistance;
    public float rotationSpeed;
    public IntReference steps;

    [SerializeField] GameEvent onDeath;

    public float desiredFloorDistance = 0.5f;
    public float tolerance = 0.05f;

    public Exhaustion currentExhaustion;

    public float[] movementTimes;

    public Action stepTaken;

    Vector2 inputAxis;

    LayerMask terrain;

    bool dead = false;

    public float noMoveDistance = 2f;
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
        terrain = LayerMask.GetMask("Terrain");

    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
            return;

        inputAxis = new Vector2(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal"));

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + inputAxis.y * rotationSpeed * Time.deltaTime, transform.eulerAngles.z);
        SetDistanceRotation();

        if (isMoving == false && inputAxis.x != 0)
        {
            StartCoroutine(MovePlayer());
        }
        /*if (steps.Value <= 0)
        {
            onDeath.Invoke();
            dead = true;
        }*/



        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, terrain))
        {
            Vector3 normal = hit.normal;
            float slopeAngle = Vector3.Angle(normal, Vector3.up);

            Debug.Log("Slope angle: " + slopeAngle);

            if (slopeAngle > 30f)
            {
                // Calculate direction down the slope
                Vector3 downSlopeDirection = Vector3.Cross(Vector3.Cross(normal, Vector3.down), normal).normalized;

                // Move the player slightly down the slope
                float moveDistance = 1f; // Adjust as needed
                transform.position += downSlopeDirection * moveDistance;
            }
        }


    }

    void SetDistanceRotation()
    {
        RaycastHit hit;



        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, terrain))
        {
            if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
            {
                if (Mathf.Abs(hit.distance - desiredFloorDistance) > tolerance)
                {
                    float correction = desiredFloorDistance - hit.distance;
                    transform.position += new Vector3(0f, correction, 0f);
                }

                transform.rotation = Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal));
            }
        }
        else
        {
            Debug.Log("ray failed");
        }
    }


    public IEnumerator MovePlayer()
    {
        Vector3 raycastOrigin = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
        Vector3 yOnlyForward = Quaternion.Euler(0, transform.eulerAngles.y, 0) * Vector3.forward;
        RaycastHit hit;

        if (inputAxis.x > 0)
        {
            Debug.Log("Inputaxis above 0");
            if (Physics.Raycast(raycastOrigin, yOnlyForward, out hit, noMoveDistance, terrain))
            {
                Debug.DrawRay(transform.position, yOnlyForward, Color.red, 999);
                Debug.Log("raycast hit");
                Debug.Log(hit.distance + "distance from front object");
                yield break;
            }
        }
        else if (inputAxis.x < 0)
        {
            if (Physics.Raycast(raycastOrigin, yOnlyForward, out hit, -noMoveDistance, terrain))
            {
                yield break;
            }
        }

        stepTaken?.Invoke();
        steps.Value--;

        isMoving = true;
        float movementTime = movementSpeeds[currentExhaustion];
        float currentMovementTime = 0f;


        Vector3 origin = transform.position;
        Vector3 destination = transform.position + inputAxis.x * yOnlyForward * moveDistance;


        while (currentMovementTime < movementTime)
        {
            currentMovementTime += Time.deltaTime;
            float t = currentMovementTime / movementTime;
            transform.position = Vector3.Lerp(origin, destination, t);
            yield return null;
        }

        transform.position = destination; // Snap to destination at the end
        isMoving = false;
    }

}

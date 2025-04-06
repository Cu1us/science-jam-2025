using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
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
    public float wallCastHeight;
    public float floorCastHeight;

    [SerializeField] GameEvent onDeath;

    public float desiredFloorDistance = 0.5f;
    public float tolerance = 0.05f;

    public Exhaustion currentExhaustion;

    public float[] movementTimes;

    public Action stepTaken;

    Vector2 inputAxis;

    LayerMask terrain;

    bool dead = false;

    public float slideSpeed;

    [SerializeField] Vector3 rayOriginFrontOffset;
    [SerializeField] Vector3 rayOriginBackOffset;

    public float noMoveDistance = 5f;
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

        RaycastHit frontHit;
        RaycastHit backHit;

        Vector3 rayOriginFront = transform.position + rayOriginFrontOffset;
        Vector3 rayOriginBack = transform.position + rayOriginBackOffset;


        //RaycastHit hit;
        /*if (Physics.Raycast(transform.position, Vector3.down, out hit, 999))
        {


            // Only slide if it's steep enough
            float slopeAngle = Vector3.Angle(slopeNormal, Vector3.up);
        }*/

        if (Physics.Raycast(rayOriginFront, Vector3.down, out frontHit, Mathf.Infinity, terrain) &&
            Physics.Raycast(rayOriginBack, Vector3.down, out backHit, Mathf.Infinity, terrain))
        {
            Vector3 slopeNormal = frontHit.normal;
            Vector3 frontPoint = frontHit.point;
            Vector3 backPoint = backHit.point;

            Vector3 direction = frontPoint - backPoint;

            float verticalDifference = direction.y;
            Vector3 horizontalDirection = new Vector3(direction.x, 0f, direction.z);
            float horizontalDistance = horizontalDirection.magnitude;

            float slopeAngle = Mathf.Atan2(verticalDifference, horizontalDistance) * Mathf.Rad2Deg;

            Debug.Log("slope angle is" + slopeAngle);

            if (Mathf.Abs(slopeAngle) > 20f) // change threshold as needed
            {
                Vector3 downhillDirection = Vector3.Cross(Vector3.Cross(slopeNormal, Vector3.down), slopeNormal).normalized;

                if (slopeAngle < 0)
                {
                    downhillDirection *= -1;
                }

                transform.position += downhillDirection * slideSpeed * Time.deltaTime;
            }
        }
    }


    void SetDistanceRotation()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = new Vector3(transform.position.x, transform.position.y + floorCastHeight, transform.position.z);

        if (Physics.Raycast(raycastOrigin, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, terrain))
        {
            if (Physics.Raycast(raycastOrigin, Vector3.down, out hit, Mathf.Infinity, terrain))
            {
                if (Mathf.Abs(hit.distance - desiredFloorDistance) > tolerance)
                {
                    float correction = desiredFloorDistance - hit.distance;
                    transform.position += new Vector3(0f, correction, 0f);
                }

                /*
                Vector3 normal = hit.normal;
                Vector3 forward = Vector3.ProjectOnPlane(transform.forward, normal).normalized;

                if (forward.sqrMagnitude < 0.001f)
                {
                    forward = Vector3.ProjectOnPlane(transform.up, normal).normalized;
                }

                Quaternion targetRotation = Quaternion.LookRotation(forward, normal);

                // Convert to Euler and clamp x (pitch) and z (roll)
                Vector3 clampedEuler = targetRotation.eulerAngles;

                clampedEuler.x = ClampAngle(clampedEuler.x, -30f, 30f);
                clampedEuler.z = ClampAngle(clampedEuler.z, -30f, 30f);

                transform.rotation = Quaternion.Euler(clampedEuler);*/
            }
        }
        else
        {

        }
    }




    IEnumerator MovePlayer()
    {
        Vector3 raycastOrigin = new Vector3(transform.position.x, transform.position.y + wallCastHeight, transform.position.z);
        Vector3 yOnlyForward = Quaternion.Euler(0, transform.eulerAngles.y, 0) * Vector3.forward;
        RaycastHit hit;

        Debug.DrawRay(raycastOrigin, yOnlyForward, Color.red, 999);

        if (inputAxis.x > 0)
        {

            if (Physics.Raycast(raycastOrigin, yOnlyForward, out hit, noMoveDistance, terrain))
            {
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

        transform.position = destination;
        isMoving = false;
    }


}
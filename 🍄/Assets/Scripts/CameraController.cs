using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float cameraDistance;
    [SerializeField] private float cameraAngle;

    [SerializeField] private float horisontalBorder;
    [SerializeField] private float verticalBorder;
    [SerializeField] private float depthBorder;

    [SerializeField] private float cameraSpeed;

    private Vector3 cameraVector;

    private Transform target;
    private Vector3 targetPosition;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        var height = Mathf.Sin(cameraAngle * (Mathf.PI / 180)) * cameraDistance;
        var length = Mathf.Cos(cameraAngle * (Mathf.PI / 180)) * cameraDistance;
        cameraVector = new Vector3(0, height, length * (-1));
        transform.localPosition += cameraVector;
        transform.rotation = Quaternion.Euler(cameraAngle, 0, 0);
        cameraVector.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = gameObject.GetComponent<Camera>().WorldToViewportPoint(target.position);
        targetPosition.x -= .5f; targetPosition.y -= .5f; targetPosition.z -= cameraDistance;
        MoveCamera();
    }

    void MoveCamera()
    {
        if (Mathf.Abs(targetPosition.x) > horisontalBorder)
        {
            transform.position += MathF.Pow(targetPosition.x, 5) * transform.right * cameraSpeed * Time.deltaTime;
        }
        if (Mathf.Abs(targetPosition.y) > verticalBorder)
        {
            transform.position += MathF.Pow(targetPosition.y, 5) * transform.up * cameraSpeed * Time.deltaTime;
        }
        if (Mathf.Abs(targetPosition.z) > depthBorder)
        {
            transform.position += (MathF.Pow(targetPosition.z, 5) * transform.forward * Time.deltaTime);
        }
    }
}

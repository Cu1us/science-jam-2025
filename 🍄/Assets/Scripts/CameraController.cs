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

    [SerializeField] private float cameraSpeed;

    private Vector3 offset;
    private Vector3 yAxis = new Vector3();
    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        var height = Mathf.Sin(cameraAngle * (Mathf.PI / 180)) * cameraDistance;
        var length = Mathf.Cos(cameraAngle * (Mathf.PI / 180)) * cameraDistance;
        offset = new Vector3(0, height, length * (-1));
        transform.localPosition = target.transform.position;
        transform.localPosition += offset;
        offset *= -1;
        transform.rotation = Quaternion.Euler(cameraAngle, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        Vector3 tempVec = (transform.position + offset) - target.position;
        if (Mathf.Abs(tempVec.z) > horisontalBorder)
        {
            transform.position += -1 * MathF.Pow(tempVec.z, 3) * Vector3.forward * cameraSpeed * Time.deltaTime * 5;
        }
        if (Mathf.Abs(tempVec.x) > verticalBorder)
        {
            transform.position +=  -1 * MathF.Pow(tempVec.x, 3) * Vector3.right * cameraSpeed * Time.deltaTime;
        }
        yAxis.x = transform.position.x;
        yAxis.y = -1 * offset.y + target.position.y;
        yAxis.z = transform.position.z;

        transform.position = yAxis;
    }
}

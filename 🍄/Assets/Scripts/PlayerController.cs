using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isMoving;
    float moveDistance;
    public IntReference steps;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputAxis = new Vector2(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal"));

        //Debug.Log(inputAxis);

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            Debug.Log(hit.distance);
            Debug.Log("This is the math: " + transform.position.y + (0.5f - hit.distance));
            if (hit.distance < 0.5f)
            {
                transform.position.Set(transform.position.x, transform.position.y + (0.5f - hit.distance), transform.position.z);
            }
        }

    }


    void MovePlayer()
    {

    }
}

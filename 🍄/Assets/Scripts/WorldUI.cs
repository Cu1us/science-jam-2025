using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class WorldUI : MonoBehaviour
{
    public int damping = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = Camera.main.transform.position;

        var lookPos = cameraPos - transform.position;
        transform.forward = Vector3.Lerp(transform.forward, Quaternion.AngleAxis(90, transform.right) * lookPos.normalized, 0.3f);
    }
}

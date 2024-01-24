using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandCarpinchoMovement : MonoBehaviour
{
    public float speed = 2f;    

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + transform.right * speed, Time.fixedDeltaTime);
    }
}

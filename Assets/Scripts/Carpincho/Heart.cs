using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private Rigidbody2D rb;
    private float delay = 0.8f;
    private float droppingTime = 0f;
    void Update()
    {
        droppingTime += Time.deltaTime;
        if (droppingTime >= delay)
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
}

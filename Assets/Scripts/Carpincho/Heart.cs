using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private Martillo player;
    private Rigidbody rb;
    private BoxCollider bc;
    private float delay = 0.5f;
    private float droppingTime = 0f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Martillo>();
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
    }
    void Update()
    {
        droppingTime += Time.deltaTime;
        if (droppingTime >= delay)
        {
            rb.useGravity = true;

            bc.enabled = true;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            player.SendMessage("Heal");
            Destroy(this.gameObject);
        }
    }
}

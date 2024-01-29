using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private Martillo player;
    private Rigidbody rb;
    private BoxCollider bc;
    private float delay = 0.8f;
    private float droppingTime = 0f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Martillo>();
    }
    void Update()
    {
        droppingTime += Time.deltaTime;
        if (droppingTime >= delay)
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = true;
            
            bc = GetComponent<BoxCollider>();
            bc.enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            player.SendMessage("Heal");
            Destroy(this.gameObject);
        }
    }
}

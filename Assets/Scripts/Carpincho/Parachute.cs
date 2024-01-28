using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute : Enemy
{
    public GameObject bomber;
    public float power = 10.0f;
    public float radius = 5.0f;
    public float upForce = 1.0f;

    [SerializeField] public GameObject particleEffect;

    private void Awake()
    {

    }

    /* private void Update()
     {
         if (player != null && !this.isStunned)
         {
             transform.position = Vector3.MoveTowards(transform.position, );
         }
     }*/

    private void FixedUpdate()
    {
        if (bomber == enabled)
        {
            //Invoke("Attack", 5);
        }
    }

    private void Attack()
    {
        Vector3 explosionPos = bomber.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, upForce, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.tag == "Floor")
        {
            //Attack();
            Instantiate(particleEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute : Enemy
{
    public GameObject bomber;
    public float radius = 5.0f;

    [SerializeField] public GameObject particleEffect;
    [SerializeField] public float dmg;

    private void Attack()
    {
        Vector3 explosionPos = bomber.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        

        foreach (Collider hit in colliders)
        {
            if (hit.tag == "Player")
            {
                hit.gameObject.SendMessage("TakeDamage", dmg);
            }
        }
    }

    private void Explode()
    {
        Instantiate(particleEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.tag == "Floor" || other.gameObject.tag == "Player")
        {
            Attack();
            Explode();
        }
    }
}

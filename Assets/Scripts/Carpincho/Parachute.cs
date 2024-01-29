using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute : Enemy
{
    [SerializeField] private GameObject bomber;
    [SerializeField] private float radius;

    [SerializeField] public GameObject particleEffect;

    [SerializeField] AudioClip audioExplosion;

    private void Attack()
    {
        Vector3 explosionPos = bomber.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);


        foreach (Collider hit in colliders)
            if (hit.tag == "Player")
                hit.gameObject.SendMessage("TakeDamage", damage);
    }

    private new void Die()
    {
        AudioManager.instance.PlaySound(audioExplosion);
        Instantiate(particleEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Parachute")
        {
            Attack();
            Die();
        }
    }
}
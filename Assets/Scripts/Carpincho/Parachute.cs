using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute : Enemy
{
    [SerializeField] private GameObject bomber;
    [SerializeField] private float radius;

    [SerializeField] public GameObject particleEffect;
    [SerializeField] public float dmg;

    [SerializeField] AudioClip audioExplosion;

    private void Attack()
    {
        Vector3 explosionPos = bomber.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);


        foreach (Collider hit in colliders)
        {
            if (hit.tag == "Player")
            {
                Debug.Log("Oh oh, te estoy dañando");
                hit.gameObject.SendMessage("TakeDamage", dmg);
            }
        }
    }

    private void Die()
    {
        AudioManager.instance.PlaySound(audioExplosion);
        Instantiate(particleEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Player")
        {
            Attack();
            Die();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
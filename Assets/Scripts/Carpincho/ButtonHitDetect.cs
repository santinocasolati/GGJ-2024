using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHitDetect : MonoBehaviour
{
    public bool canDamage = false;
    private FinalBoss parent;

    private void Start()
    {
        parent = transform.parent.GetComponent<FinalBoss>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!canDamage) return;
        if (collision.gameObject.tag == "Player")
        {
            parent.TakeDamage(parent.damage);
        }
    }
}

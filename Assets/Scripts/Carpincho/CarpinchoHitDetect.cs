using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpinchoHitDetect : MonoBehaviour
{
    private Enemy parent;

    private void Start()
    {
        parent = transform.parent.gameObject.GetComponent<Enemy>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == parent.player.gameObject)
        {
            parent.Die();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpinchoTwerk : MonoBehaviour
{
    public float minSpeed = 3f;
    public float maxSpeed = 4f;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetFloat("TwerkSpeed", Random.Range(minSpeed, maxSpeed));
    }
}

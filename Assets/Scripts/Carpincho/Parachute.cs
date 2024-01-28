using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute : Enemy
{
    private Animator animator;


    private void Awake()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        if (player != null && !this.isStunned)
        {

        }
    }















}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Ranged : Enemy
{
    [SerializeField] Transform shootPosition;
    [SerializeField] GameObject projectile;
    private float speed = 1.5f;
    private float attackDelay = 3f;
    private float range = 5f;
    private float projectileSpeed = 2f;
    private Animator animator;

    private void Awake()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            bool shouldMoveToPlayer = true;

            this.KeepDistances(distanceToPlayer, shouldMoveToPlayer);

            if (distanceToPlayer > 3f)
            {
                Quaternion rot = Quaternion.LookRotation(player.transform.position - transform.position);
                rot.eulerAngles = new Vector3(0, rot.eulerAngles.y, 0);
                transform.rotation = rot;
            }

            bool isWalking = false;

            if (shouldMoveToPlayer && distanceToPlayer >= range)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                isWalking = true;
            }
            else if (shouldMoveToPlayer && attackTimer >= attackDelay)
            {
                this.Attack();
            }

            animator.SetBool("Walk", isWalking);

            attackTimer += Time.deltaTime;
        }
    }
    private bool KeepDistances(float distanceToPlayer, bool shouldMoveToPlayer) {

        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, range / 2, LayerMask.GetMask("Enemy", "Player"));

        foreach (Collider2D enemyCollider in nearbyEnemies)
        {
            float distanceToKeep;
            float distanceToEnemy = Vector3.Distance(transform.position, enemyCollider.transform.position);

            if (enemyCollider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                distanceToKeep = range / 4;
            else
                distanceToKeep = range / 2;

            if (distanceToEnemy < distanceToKeep)
            {
                shouldMoveToPlayer = false;
                Vector3 directionAwayFromEnemy = (transform.position - enemyCollider.transform.position).normalized;
                transform.Translate(directionAwayFromEnemy * speed * Time.deltaTime);

                if (attackTimer >= attackDelay && distanceToPlayer <= range)
                    this.Attack();

                return shouldMoveToPlayer;
            }
        }
        return shouldMoveToPlayer;
    }
    private void Attack()
    {
        Projectile.ThrowProjectile(this.projectile, this.shootPosition.position, this.transform.rotation, this.player.transform.position, this.projectileSpeed, this.damage);
        attackTimer = 0f;
    }
}
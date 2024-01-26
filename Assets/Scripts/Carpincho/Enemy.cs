using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected Martillo player;
    [SerializeField] protected GameObject corazon;
    protected int damage = 10;
    protected float attackTimer = 2f;
    protected int health = 100;
    protected bool isStunned = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Martillo>();
    }
    public void TakeDamage(int amount)
    {
        /*StartCoroutine(Stun());
        AudioSource enemyAudio = GetComponent<AudioSource>();
        enemyAudio.Play();
        health -= amount;
        if (health <= 0)*/
            Die();
    }
    private void Die()
    {
        ChanceToDropHeart();
        Destroy(this.gameObject);
    }

    protected void ChanceToDropHeart()
    {
        if (Random.value < 0.1f)
            Instantiate(corazon, transform.position + new Vector3(0,0.3f,0), Quaternion.identity);
    }

    protected IEnumerator Stun()
    {
        this.isStunned = true;
        //visual feedback de cuando esta stunned
        yield return new WaitForSeconds(0.1f);

        this.isStunned = false;
    }
}

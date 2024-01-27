using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Martillo player;
    [SerializeField] protected GameObject corazon;
    protected int damage = 10;
    protected float attackTimer = 2f;
    protected int health = 100;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Martillo>();
    }
    public void TakeDamage(int amount)
    {
        AudioSource enemyAudio = GetComponent<AudioSource>();
        enemyAudio.Play();
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
}

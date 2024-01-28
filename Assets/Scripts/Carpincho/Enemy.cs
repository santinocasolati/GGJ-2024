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

    private void Die()
    {
        GetComponent<CarpinchoDie>().PlaySound();
        ChanceToDropHeart();
        Destroy(this.gameObject);
    }

    protected void ChanceToDropHeart()
    {
        if (Random.value < 0.1f)
            Instantiate(corazon, transform.position + new Vector3(0,0.3f,0), Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player.gameObject)
            Die();
    }
}

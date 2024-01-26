using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Martillo : MonoBehaviour
{
    private int health = 100;
    [SerializeField] GameObject lifeBar;
    private Animator animator;
    GameObject panelController;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.y = 1f;
        this.transform.position = worldPos;
        
        if (Input.GetMouseButton(0))
        {
            this.Attack();
        }
    }
    public void Attack()
    { 

    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        lifeBar.GetComponent<LifeBar>().SetCurrentHealth(health);

        if (health <= 0)
            Die();
    }
    private void Die()
    {
        //panelController.SendMessage("showGameOverPanel", "Game Over");
        Time.timeScale = 0;
        Destroy(this.gameObject);
    }
}

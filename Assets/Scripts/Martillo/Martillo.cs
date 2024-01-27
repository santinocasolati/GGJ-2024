using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Martillo : MonoBehaviour
{
    private int health = 100;
    [SerializeField] GameObject lifeBar;
    [SerializeField] GameObject transition;
    [SerializeField] Vector2 offset;
    private Animator animator;
    GameObject panelController;
    private bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        CursorManager.instance.HideCursor();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.y = 1f;
        worldPos.x += offset.x;
        worldPos.z += offset.y;
        this.transform.position = worldPos;
        
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            this.Attack();
        }
    }
    public void Attack()
    {
        canAttack = false;
        animator.enabled = true;
    }

    public void ResetAttack()
    {
        canAttack = true;
        animator.enabled = false;
    }

    public void HammerDown()
    {
        StartCoroutine(MoveDown());
    }

    IEnumerator MoveDown()
    {
        float startY = transform.localPosition.y;
        float targetPosition = 0;

        float elapsedTime = 0f;

        while (elapsedTime < 0.16f)
        {
            float currentY = Mathf.Lerp(startY, targetPosition, elapsedTime / 0.16f);
            transform.localPosition = new Vector3(transform.localPosition.x, currentY, transform.localPosition.z);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = new Vector3(transform.localPosition.x, targetPosition, transform.localPosition.z);
    }

    public void HammerUp()
    {
        StartCoroutine(MoveUp());
    }

    IEnumerator MoveUp()
    {
        float startY = transform.localPosition.y;
        float targetPosition = 1;

        float elapsedTime = 0f;

        while (elapsedTime < 0.16f)
        {
            float currentY = Mathf.Lerp(startY, targetPosition, elapsedTime / 0.16f);
            transform.localPosition = new Vector3(transform.localPosition.x, currentY, transform.localPosition.z);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = new Vector3(transform.localPosition.x, targetPosition, transform.localPosition.z);
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
        //Time.timeScale = 0;

        transition.SetActive(true);
        MainMenuTransition tr = transition.GetComponent<MainMenuTransition>();
        tr.targetCameraPosition.x = Camera.main.transform.position.x;
        tr.StartTransition(2);

        Destroy(this.gameObject);
    }
}

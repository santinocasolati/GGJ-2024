using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Martillo : MonoBehaviour
{
    private int health = 100;
    [SerializeField] GameObject lifeBar;
    [SerializeField] GameObject transition;
    [SerializeField] Vector2 sens;
    private Animator animator;
    public float hammerHeight = 2.5f;
    public bool isBoss = false;

    public bool canAttack = true;

    void Start()
    {
        CursorManager.instance.HideCursor();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sens.x;
        float mouseY = Input.GetAxis("Mouse Y") * sens.y;

        Vector3 movement = new Vector3(mouseX, 0f, mouseY);

        Vector3 localPos = this.transform.localPosition;

        if (!isBoss)
        {
            localPos.x += movement.x;
        }

        this.transform.localPosition = localPos;

        Vector3 worldPos = this.transform.position;
        worldPos.y = hammerHeight;

        if (!isBoss)
        {
            worldPos.z += movement.z;
        } else
        {
            worldPos.x += movement.z;
            worldPos.z -= movement.x;
        }

        this.transform.position = worldPos;
        
        if (Input.GetMouseButtonDown(0) && canAttack)
            this.Attack();
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
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        StartCoroutine(MoveDown());
    }

    IEnumerator MoveDown()
    {
        float startY = transform.position.y;
        float targetPosition = 0;

        float elapsedTime = 0f;

        while (elapsedTime < 0.16f)
        {
            float currentY = Mathf.Lerp(startY, targetPosition, elapsedTime / 0.16f);
            transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = new Vector3(transform.position.x, targetPosition, transform.position.z);
    }

    public void HammerUp()
    {
        Destroy(GetComponent<Rigidbody>());

        StartCoroutine(MoveUp());
    }

    IEnumerator MoveUp()
    {
        float startY = transform.position.y;
        float targetPosition = hammerHeight;

        float elapsedTime = 0f;

        while (elapsedTime < 0.16f)
        {
            float currentY = Mathf.Lerp(startY, targetPosition, elapsedTime / 0.16f);
            transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = new Vector3(transform.position.x, targetPosition, transform.position.z);
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        lifeBar.GetComponent<LifeBar>().SetCurrentHealth(health);

        if (health <= 0)
            Die();
    }

    public void Heal()
    {
        lifeBar.GetComponent<LifeBar>().Heal(20);
    }
    private void Die()
    {
        transition.SetActive(true);
        MainMenuTransition tr = transition.GetComponent<MainMenuTransition>();
        tr.targetCameraPosition.x = Camera.main.transform.position.x;
        tr.StartTransition(3);

        Destroy(this.gameObject);
    }
}

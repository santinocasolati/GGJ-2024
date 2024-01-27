using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    private float maxHealth = 100f;
    protected float currentHealth = 100f;
    [SerializeField] public Image healthBarImage;

    private void Start()
    {
        transform.position = new Vector3(-4.33f, 1.78f, -9.63f);
        Camera mainCamera = Camera.main;
        transform.rotation = mainCamera.transform.rotation;
    }
    void Update()
    {
        float fillAmount = this.currentHealth / maxHealth;
        healthBarImage.fillAmount = fillAmount;
        transform.position = new Vector3(Camera.main.transform.position.x - 4.33f, transform.position.y, transform.position.z);
    }
    public void SetCurrentHealth(int health)
    { 
        this.currentHealth = (float)health;
    }
    public void Heal(int quantity)
    {
        this.currentHealth += (float)quantity;
    }
}

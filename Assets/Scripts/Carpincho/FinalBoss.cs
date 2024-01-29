using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    [SerializeField] private float hp;
    [SerializeField] private GameObject lifebar;
    
    public Material redMat;
    public Material greenMat;

    private float time = 0;
    [SerializeField] private float maxTimeChange;

    private int lastIndex = 0;
    private int randomChildIndex = 0;

    private void Start()
    {
        //Set default state
        foreach (Transform childTransform in transform)
        {
            if (childTransform.tag == "HittableBoss")
            {
                Renderer ch = childTransform.GetComponent<Renderer>();
                ch.material = redMat;
                Debug.Log("Estoy defaulteando");
            }
        }
    }

    private void Update()
    {
        if (time >= maxTimeChange)
        {  
            while (lastIndex == randomChildIndex)
            {
                Debug.Log("Estoy encendiendo");
                randomChildIndex = Random.Range(1, transform.childCount);
            }
            lastIndex = randomChildIndex;
            Transform randomChild = transform.GetChild(randomChildIndex);
            Debug.Log(randomChildIndex);
            StartCoroutine(lightButtonOn(randomChild));
            
            
            time = 0;
        }
        time += Time.deltaTime;
    }

    IEnumerator lightButtonOn(Transform randomChild)
    {
        randomChild.GetComponent<Renderer>().material = greenMat;
        yield return new WaitForSeconds(maxTimeChange);
        randomChild.GetComponent<Renderer>().material = redMat;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Collider myCollider = collision.GetContact(0).thisCollider;
        Debug.Log(collision);

        //if (collision.gameObject.tag == "Player")
            Debug.Log("Estoy golpeando correctamente");
            TakeDamage(1);
            time = 0;
        
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        //lifeBar.GetComponent<LifeBar>().SetCurrentHealth(hp);

        if (hp <= 0)
        {

        }
            
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

public class FinalBoss : MonoBehaviour
{
    public int damage = 1;

    [SerializeField] Transform carpinchoBoss;
    [SerializeField] float animTime;
    [SerializeField] float targetAnimation;
    [SerializeField] GameObject particles;

    [SerializeField] private float hp;
    [SerializeField] private GameObject lifebar;
    
    public Material redMat;
    public Material greenMat;

    private float time = 0;
    [SerializeField] private float maxTimeChange;

    private int lastIndex = 0;
    private int randomChildIndex = 0;

    private bool canButton = true;

    [SerializeField] private AudioClip deathSound;

    private void Start()
    {
        ResetStates();
    }

    private void ResetStates()
    {
        foreach (Transform childTransform in transform)
        {
            if (childTransform.tag == "HittableBoss")
            {
                Renderer ch = childTransform.GetComponent<Renderer>();
                ch.material = redMat;

                ButtonHitDetect bh = childTransform.GetComponent<ButtonHitDetect>();
                bh.canDamage = false;
            }
        }
    }

    private void Update()
    {
        if (time >= maxTimeChange && canButton)
        {  
            while (lastIndex == randomChildIndex)
            {
                randomChildIndex = Random.Range(1, transform.childCount);
            }
            lastIndex = randomChildIndex;
            Transform randomChild = transform.GetChild(randomChildIndex);
            StartCoroutine(lightButtonOn(randomChild));
            
            
            time = 0;
        }
        time += Time.deltaTime;
    }

    IEnumerator lightButtonOn(Transform randomChild)
    {
        Renderer rnd = randomChild.GetComponent<Renderer>();
        ButtonHitDetect bhd = randomChild.GetComponent<ButtonHitDetect>();

        rnd.material = greenMat;
        bhd.canDamage = true;

        yield return new WaitForSeconds(maxTimeChange);

        rnd.material = redMat;
        bhd.canDamage = false;
    }

    private void Die()
    {
        Destroy(GameObject.Find("Enemies"));
        Destroy(GameObject.Find("Spawner"));
        canButton = false;
        ResetStates();
        particles.SetActive(true);
        StartCoroutine(DieAnimation());

        AudioManager.instance.PlaySound(deathSound);
    }

    private IEnumerator DieAnimation()
    {
        float elapsedTime = 0;
        Vector3 originPos = carpinchoBoss.position;
        Vector3 targetPos = new Vector3(originPos.x, originPos.y - targetAnimation, originPos.z);

        while (elapsedTime < animTime)
        {
            carpinchoBoss.position = Vector3.Lerp(originPos, targetPos, elapsedTime / animTime);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        particles.SetActive(false);

        yield return new WaitForSeconds(1);

        GameObject transition = GameObject.Find("Transition");
        MainMenuTransition tr = transition.GetComponent<MainMenuTransition>();
        tr.targetCameraPosition.x = Camera.main.transform.position.x;
        tr.StartTransition(5);
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        //lifeBar.GetComponent<LifeBar>().SetCurrentHealth(hp);

        if (hp <= 0)
        {
            Die();
        }

        ResetStates();
    }
}
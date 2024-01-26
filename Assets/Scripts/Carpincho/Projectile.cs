using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Projectile : MonoBehaviour
{
    private Vector3 _end;
    private float _speed;
    private int _damage;

    public static void ThrowProjectile(GameObject gameObject, Vector3 startPoint, Quaternion rotation, Vector3 endPoint, float speed, int damage)
    {
        Vector3 direction = endPoint - startPoint;
        startPoint += (direction.normalized * 0.2f);

        //startPoint.y = 1f;
        GameObject projectileObj = Instantiate(gameObject, startPoint, rotation);
        projectileObj.AddComponent<Projectile>();

        Projectile projectile1 = projectileObj.GetComponent<Projectile>();
        projectile1._damage = damage;
        projectile1._end = endPoint;
        projectile1._speed = speed;
    }
    
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _end, _speed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation((transform.position - _end).normalized);

        if (transform.position.x == _end.x && transform.position.y == _end.y)
            Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
            collision.gameObject.SendMessage("TakeDamage", _damage);
        }
    }

}

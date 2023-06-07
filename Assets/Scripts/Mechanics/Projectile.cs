using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float lifetime;
    public int damage;

    //this is meant to be modified by the object creating this projectile -> the shoot class
    [HideInInspector]
    public float speed;

    void Start()
    {
        if (lifetime <= 0) lifetime = 2.0f;
        if (damage <= 0) damage = 5;

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject otherCollider = collision.gameObject;
        if (otherCollider.CompareTag("Enemy") && gameObject.CompareTag("Projectile")) otherCollider.GetComponent<Enemy>().TakeDamage(damage);
        if (otherCollider.CompareTag("Player") && gameObject.CompareTag("EnemyProjectile"))
        {
            GameManager.instance.lives--;
        }
        if ((GetComponent<Rigidbody2D>().velocity.x / speed) <= 0.95) Destroy(gameObject);
    }
}

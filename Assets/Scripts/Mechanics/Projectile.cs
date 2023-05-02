using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float lifetime;

    //this is meant to modified by the object creating this projectile -> the shoot class
    [HideInInspector]
    public float speed;

    void Start()
    {
        if (lifetime <= 0) lifetime = 2.0f;

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject otherCollider = collision.gameObject;
        if (otherCollider.name != "Player")
        {
            if ((GetComponent<Rigidbody2D>().velocity.x / speed) <= 0.95)
            {
                Destroy(gameObject);
            }
        }
    }
}

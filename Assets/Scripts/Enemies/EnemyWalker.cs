using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyWalker : Enemy
{
    Rigidbody2D rb;
    public float speed;
    public override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();

        if (speed <= 0) speed = 1.0f;
    }

    void Update()
    {
        AnimatorClipInfo[] curClips = anim.GetCurrentAnimatorClipInfo(0);

        if (curClips[0].clip.name == "Enemy_Walk")
        {
            rb.velocity = new Vector2((sr.flipX ? -1 : 1) * speed, rb.velocity.y)
;        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Barrier")) sr.flipX = !sr.flipX;
    }

    public override void Death()
    {
        base.Death();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    public void DestroySelf()
    {
        Destroy(transform.parent.gameObject);
    }

    public void Squish()
    {
        anim.SetTrigger("Squish");
    }
}

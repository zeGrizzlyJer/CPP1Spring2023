using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class Enemy : MonoBehaviour
{
    protected SpriteRenderer sr;
    protected Animator anim;
    public AudioClip deathClip;

    protected int _health;
    public int maxHealth;

    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            if (_health > maxHealth) _health = maxHealth;
            if (_health <= 0) Death();
        }
    }

    public virtual void Death()
    {
        GameManager.instance.playerInstance.GetComponent<AudioSourceManager>().PlayOneShot(deathClip, false);
        anim.SetTrigger("Death");
    }

    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        if (maxHealth <= 0) maxHealth = 5;

        Health = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
    }
}

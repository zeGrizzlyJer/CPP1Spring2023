using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shoot : MonoBehaviour
{
    SpriteRenderer sr;
    AudioSourceManager asm;

    public float projectileSpeed;
    public Transform spawnPointRight;
    public Transform spawnPointLeft;

    public Projectile projectilePrefab;
    public AudioClip fireSound;
    public UnityEvent OnProjectileSpawned;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        asm= GetComponent<AudioSourceManager>();

        if (projectileSpeed <= 0) projectileSpeed = 7.0f;

        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)
        {
            Debug.Log("Please set default values on " + gameObject.name);
        }
    }

    public void Fire()
    {
        if (!sr.flipX)
        {
            Projectile temp = Instantiate(projectilePrefab);
            temp.transform.position = spawnPointRight.position;
            temp.speed = projectileSpeed;
        }
        else
        {
            Projectile temp = Instantiate(projectilePrefab);
            temp.transform.position = spawnPointLeft.position;
            temp.speed = -projectileSpeed;
        }
        OnProjectileSpawned?.Invoke();
        asm.PlayOneShot(fireSound, false);
    }
}

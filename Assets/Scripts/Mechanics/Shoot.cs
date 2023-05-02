using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    SpriteRenderer sr;

    public float projectileSpeed;
    public Transform spawnPointRight;
    public Transform spawnPointLeft;

    public Projectile projectilePrefab;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

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
    }
}

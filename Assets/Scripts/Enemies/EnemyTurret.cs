using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shoot))]
public class EnemyTurret : Enemy
{
    public float projectileFireRate;
    Shoot shootScript;
    float timeSinceLastFire = 0f;
    float firingRange;

    public override void Start()
    {
        base.Start();

        shootScript = GetComponent<Shoot>();
        shootScript.OnProjectileSpawned.AddListener(UpdateTimeSinceLastFire);

        if (projectileFireRate <= 0f) projectileFireRate = 1f;
        if (firingRange <= 0f) firingRange = 10f;
    }

    private void Update()
    {
        if (GameManager.instance.playerInstance.transform.position.x <= transform.position.x) sr.flipX = true;
        else sr.flipX = false;
        float distanceApart = Mathf.Abs(transform.position.x - GameManager.instance.playerInstance.transform.position.x);
        AnimatorClipInfo[] curClips = anim.GetCurrentAnimatorClipInfo(0);



        if (curClips[0].clip.name != "Turret_Attack")
        {
            if (Time.time >= timeSinceLastFire + projectileFireRate && distanceApart <= firingRange)
            {
                anim.SetTrigger("Attack");
            }
        }
    }

    public override void Death()
    {
        Destroy(gameObject);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    void UpdateTimeSinceLastFire()
    {
        timeSinceLastFire = Time.time;
    }

    private void OnDisable()
    {
        shootScript.OnProjectileSpawned.RemoveListener(UpdateTimeSinceLastFire);
    }
}

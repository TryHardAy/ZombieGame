using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float damage;
    private event OnDeath ZombieOndDeath;

    private void Start()
    {
        Destroy(gameObject, 1);
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        var enemy = collision.collider.GetComponent<IEnemies>();
        var difBullet = collision.collider.GetComponent<Bullet>();

        if (enemy is not null)
        {
            enemy.GetDamaged(damage, ZombieOndDeath);
        }
        else if (difBullet is not null)
        {
            return;
        }

        Destroy(gameObject);
    }

    public void SetParams(float damage,  OnDeath onDeath)
    {
        this.damage = damage;
        ZombieOndDeath += onDeath;
    }
}

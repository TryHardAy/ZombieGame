using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics 
{
    private int damage = 0;
    private int damagePercent = 0;
    private int maxAmmo = 30;
    private float health;
    private int maxHealth = 100;
    private int healthRegen = 1;
    private int fireRatePercent = 0;
    private int armor = 0;
    private int speedPercent = 0;

    private int armorCap = 70;
    private int negArmorCap = 30;
    private int armorScalingFactor = 600;

    public int GetDamage => damage;
    public int GetDamagePercent => damagePercent;
    public int GetMaxAmmo => maxAmmo;
    public int GetMaxHealth => maxHealth;
    public float GetHealth => health;
    public int GetFireRatePercent => fireRatePercent;

    public Statistics() 
    {
        health = maxHealth;
    }

    public void AddDamage(int damage)
    {
        this.damage += damage;
    }

    public void AddDamagePercent(int damagePercent)
    {
        this.damagePercent += damagePercent;
    }

    public void TakeDamage(float damage)
    {
        damage -= damage * CalDmgReduction() / 100;
        health -= damage;
        Debug.Log("Player took damage: " + damage + " | Remaining health: " + health);  // Debug log to check damage
    }

    private int CalDmgReduction()
    {
        if (armor > 0)
        {
            /*
             -a/(x + a/b) + b
             */
            return -armorScalingFactor / (armor + (armorScalingFactor / armorCap)) + armorCap;
        }
        else if (armor < 0)
        {
            /*
             a/(-x + a/b) - b
             */
            return armorScalingFactor / (-armor + (armorScalingFactor / negArmorCap)) - negArmorCap;
        }
        else
        {
            return 0;
        }
    }

    public void AddMaxHealth(int maxHealth)
    {
        if (maxHealth <= 0)
            return;

        this.maxHealth += maxHealth;
        health += maxHealth;
    }

    public void RegenHealth()
    {
        health += healthRegen;

        if (health > maxHealth)
            health = maxHealth;
    }

    public void AddHealthRegen(int healthRegen)
    {
        this.healthRegen += healthRegen;

        if (this.healthRegen < 0)
            this.healthRegen = 0;
    }

    public void AddFireRatePercent(int fireRatePercent)
    {
        this.fireRatePercent += fireRatePercent;
    }

    public void AddArmor(int armor)
    {
        this.armor += armor; 
    }

    public void AddSpeedPercent(int speedPercent)
    {
        this.speedPercent += speedPercent;
    }

    public void AddMaxAmmo(int maxAmmo)
    {
        this.maxAmmo += maxAmmo;
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    // New method to apply zombie attack damage to the player
    public void ApplyZombieDamage(float damage)
    {
        TakeDamage(damage);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoost : MonoBehaviour, IPickAble
{
    public void PickUp(Statistics stats)
    {
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            stats.AddDamage(2);
        }
        else
        {
            stats.AddDamagePercent(2);
        }
        Debug.Log("DamageUp");
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxAmmoBoost : MonoBehaviour, IPickAble
{
    public void PickUp(Statistics stats)
    {
        stats.AddMaxAmmo(15);
        Debug.Log("AmmoUp");
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthBoost : MonoBehaviour, IPickAble
{
    public void PickUp(Statistics stats)
    {
        stats.AddMaxHealth(40);
        Debug.Log("MaxHealthUp");
        Destroy(gameObject);
    }
}

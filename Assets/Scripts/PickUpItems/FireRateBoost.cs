using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateBoost : MonoBehaviour, IPickAble
{
    public void PickUp(Statistics stats)
    {
        stats.AddFireRatePercent(5);
        Debug.Log("FireRateUp");
        Destroy(gameObject);
    }
}

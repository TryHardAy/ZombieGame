using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBoost : MonoBehaviour, IPickAble
{
    public void PickUp(Statistics stats)
    {
        stats.AddArmor(1);
        Debug.Log("Armorm up");
        Destroy(gameObject);
    }
}

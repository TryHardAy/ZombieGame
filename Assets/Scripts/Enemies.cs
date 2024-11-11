using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemies
{
    public void GetDamaged(float value, OnDeath onDeath);
}

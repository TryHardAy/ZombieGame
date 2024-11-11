using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float ZombieDmg = 30;
    public static float ZombieSpeed = 2;
    public static float ZombieHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameUpdate());
    }

    private IEnumerator GameUpdate()
    {
        yield return new WaitForSeconds(60);
        ZombieDmg = 60;
        ZombieSpeed = 3;
        ZombieHealth = 120;

        yield return new WaitForSeconds(240);
        ZombieDmg = 100;
        ZombieSpeed = 4;
        ZombieHealth = 200;

        while (true)
        {
            yield return new WaitForSeconds(240);
            ZombieDmg *= 1.1f;
            ZombieSpeed *= 1.1f;
            ZombieHealth *= 1.1f;
        }
    }
}

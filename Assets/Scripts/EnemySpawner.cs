using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // Prefab przeciwnika
    [SerializeField] private Transform[] spawnPoints; // Tablica punkt�w spawnu
    [SerializeField] private float spawnInterval = 5f; // Interwa� czasowy mi�dzy spawnami

    private bool spawning = true;

    void Start()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab is not assigned.");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Spawn points are not assigned or empty.");
            return;
        }

        // Rozpocznij korutyn� spawnu
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (spawning)
        {
            // Losowo wybierz punkt spawnu
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            // Stw�rz przeciwnika w punkcie spawnu
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            // Poczekaj okre�lony czas przed kolejnym spawnem
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}

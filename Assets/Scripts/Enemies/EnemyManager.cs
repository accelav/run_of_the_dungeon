using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs; // Puedes poner 1 o varios prefabs
    [SerializeField] float minSpawnInterval = 5f; // Tiempo mínimo de spawn (en segundos)
    [SerializeField] float maxSpawnInterval = 15f; // Tiempo máximo de spawn (en segundos)
    [SerializeField] Vector3 spawnPoint = new Vector3(0f, 0f, 0f); // Posición base de aparición

    private float timer = 0f;

    void Start()
    {
        // Llama a la función de spawn con un intervalo aleatorio inicial
        SetRandomSpawnInterval();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= maxSpawnInterval)  // Usamos el valor máximo para el ciclo
        {
            SpawnEnemy();
            SetRandomSpawnInterval(); // Configura un nuevo intervalo aleatorio
            timer = 0f;
        }
    }

    // Establece un nuevo intervalo aleatorio
    void SetRandomSpawnInterval()
    {
        maxSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval); // Establece un nuevo intervalo aleatorio
    }

    void SpawnEnemy()
    {
        int randomPrefab = Random.Range(0, enemyPrefabs.Length);

        Vector3 spawnPos = new Vector3(
            spawnPoint.x,
            spawnPoint.y,
            spawnPoint.z
        );

        GameObject enemy = Instantiate(enemyPrefabs[randomPrefab], spawnPos, Quaternion.identity);

        // Asigna la misma lista de carriles al enemigo
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.GetType()
                .GetField("positions", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        }
    }
}

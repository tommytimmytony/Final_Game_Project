using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlusSpawner : MonoBehaviour
{
    [SerializeField] private GameObject healthPlusPrefab;
    [SerializeField] private float spawnRange = 10f;
    [SerializeField] private float spawnRate = 10f;
    
    void Start()
    {
        SpawnPowerUp();
    }

    void SpawnPowerUp(){
        StartCoroutine(SpawnPowerUpsRoutine());

        IEnumerator SpawnPowerUpsRoutine() {
            while (true) {
                float randRate = Random.Range(0, spawnRate);
                yield return new WaitForSeconds(randRate);
                SpawnPowerUpRandom();
            }
        }
    }
    
    void SpawnPowerUpRandom() {
        float randX = Random.Range(-spawnRange, spawnRange);
        GameObject newObject = Instantiate(healthPlusPrefab, new Vector3(randX,5), Quaternion.identity);

        Destroy(newObject, 15);  
    }
}

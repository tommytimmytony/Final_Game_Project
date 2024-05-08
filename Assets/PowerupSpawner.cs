using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject powerupPrefab;
    [SerializeField] private float spawnRange = 6f;
    [SerializeField] private float spawnRate = 8f;
    
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
        GameObject newObject = Instantiate(powerupPrefab, new Vector3(randX,5), Quaternion.identity);

        Destroy(newObject, 15);  
    }
}

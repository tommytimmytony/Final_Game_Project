using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cloudPrefab;
    [SerializeField] private float spawnRange = 5f;
    [SerializeField] private float spawnRate = 2f;
   
    void Start()
    {
        SpawnClouds();
    }

    void Update() {}

    void SpawnClouds(){
        StartCoroutine(SpawnCloudsRoutine());
        IEnumerator SpawnCloudsRoutine(){
            while(true){
                float randRate = Random.Range(0, spawnRate);
                yield return new WaitForSeconds(randRate);
                SpawnCloudRandom();
            }
        }
    }

       void SpawnCloudRandom(){

       int randomX = 7;
       float randomY = Random.Range(-spawnRange,spawnRange);

       GameObject newCloud = Instantiate(cloudPrefab,new Vector3(randomX,randomY,0),Quaternion.identity);
       Rigidbody2D cloudRB = newCloud.GetComponent<Rigidbody2D>();
       cloudRB.velocity = new Vector2(-2, 0); 
       Destroy(newCloud,10);
    }
}

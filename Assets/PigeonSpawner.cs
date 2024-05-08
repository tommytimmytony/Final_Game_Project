using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pigeonPrefab;
    [SerializeField] private float spawnRange = 5f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float spawnRate = 10f;
    void Start()
    {
        SpawnBirds();
    }

    void SpawnBirds(){
        StartCoroutine(SpawnBirdsRoutine());
        IEnumerator SpawnBirdsRoutine(){
            while(true){
                SpawnBirdRight();
                float randRate = Random.Range(0, spawnRate);
                yield return new WaitForSeconds(randRate);
                SpawnBirdLeft();
            }
        }
    }

    void SpawnBirdRight(){
       float randomX = 10f;
       float randomY = Random.Range(-spawnRange,spawnRange);

       GameObject newBird = Instantiate(pigeonPrefab,new Vector3(randomX,randomY,0),Quaternion.identity);
       Rigidbody2D birdRB = newBird.GetComponent<Rigidbody2D>();
       birdRB.velocity = new Vector2(-speed, 0);
       newBird.transform.localScale = new Vector3(-2,2,2);
       Destroy(newBird,10);
    }

    void SpawnBirdLeft(){
        float randomX = -10f;
        float randomY = Random.Range(-spawnRange,spawnRange);
        GameObject newBird = Instantiate(pigeonPrefab,new Vector3(randomX,randomY,0),Quaternion.identity);
        Rigidbody2D birdRB = newBird.GetComponent<Rigidbody2D>();
        birdRB.velocity = new Vector2(speed, 0);
        newBird.transform.localScale = new Vector3(2,2,2);
        Destroy(newBird,10);
    }
}

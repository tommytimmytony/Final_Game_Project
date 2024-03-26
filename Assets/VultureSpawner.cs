using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VultureSpawner : MonoBehaviour
{
    [SerializeField] private GameObject vulturePrefab;
    [SerializeField] private float spawnRange = 5f;
    [SerializeField] private float spawnRate = 5f;
    [SerializeField] private float birdSpeed = 5f;
    void Start()
    {
        SpawnBirds();
    }

    void Update() {}

    void SpawnBirds(){
        StartCoroutine(SpawnBirdsRoutine());
        IEnumerator SpawnBirdsRoutine(){
            while(true){
                float randRate = Random.Range(0, spawnRate);
                yield return new WaitForSeconds(randRate);
                SpawnBirdRandom();
            }
        }
    }

       void SpawnBirdRandom(){

       float randomX = Random.Range(-spawnRange,spawnRange);
       float randomY = -5.5f;

       GameObject newBird = Instantiate(vulturePrefab,new Vector3(randomX,randomY,0),Quaternion.identity);
       Rigidbody2D birdRB = newBird.GetComponent<Rigidbody2D>();
       birdRB.velocity = new Vector2(0, birdSpeed);
       newBird.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
       newBird.transform.localScale = new Vector3(-3,3,3);
       Destroy(newBird,10);
    }
}

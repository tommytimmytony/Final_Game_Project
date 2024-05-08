using UnityEngine;
using System.Collections;

public class BirdSpawner : MonoBehaviour {
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private float spawnRange = 5f;
    [SerializeField] private float spawnRate = 10f;
    [SerializeField] private float birdSpeed = 5f;

    void Start() {
        SpawnBirds();
    }

    void Update() {}

    void SpawnBirds() {
        StartCoroutine(SpawnBirdsRoutine());
    }

    IEnumerator SpawnBirdsRoutine() {
        while (true) {
            float randRate = Random.Range(0, spawnRate);
            yield return new WaitForSeconds(randRate); 
            SpawnBirdRandom(); 
        }
    }

    void SpawnBirdRandom() {
        float randomX = Random.Range(-spawnRange, spawnRange);
        float randomY = -5.5f;


        GameObject newBird = objectPool.GetPooledObject();
        newBird.transform.position = new Vector3(randomX, randomY, 0); 
        newBird.transform.rotation = Quaternion.Euler(0f, 0f, 90f); 
        newBird.transform.localScale = new Vector3(-3, 3, 3);   
        Rigidbody2D birdRB = newBird.GetComponent<Rigidbody2D>();
        birdRB.velocity = new Vector2(0, birdSpeed); 

        StartCoroutine(ReturnBirdAfterTime(newBird, 10f));
    }

    private IEnumerator ReturnBirdAfterTime(GameObject bird, float delay) {
        yield return new WaitForSeconds(delay); 
        objectPool.ReturnObjectToPool(bird); 
    }
}

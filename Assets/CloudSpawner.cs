using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Video;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cloudPrefab;
    [SerializeField] private float spawnRange = 5f;
    [SerializeField] private float spawnRate = 0.5f;
   
    private Coroutine spawnCloudsCoroutine;

    public void InitSpawner(float duration) {
        StopAllCoroutines();
        Debug.Log(spawnCloudsCoroutine);
        spawnCloudsCoroutine = StartCoroutine(SpawnCloudsRoutine());
        StartCoroutine(StopAfterTime(duration));
    }

    IEnumerator StopAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (spawnCloudsCoroutine != null) 
        {
            StopCoroutine(spawnCloudsCoroutine);
            spawnCloudsCoroutine = null;
        }
    }

    IEnumerator SpawnCloudsRoutine(){
        while(true){
            float randRate = Random.Range(0, spawnRate);
            yield return new WaitForSeconds(randRate);
            SpawnCloudRandom();
        }
    }


       void SpawnCloudRandom(){

       int randomX = 12;
       float randomY = Random.Range(-spawnRange,spawnRange);

       GameObject newCloud = Instantiate(cloudPrefab,new Vector3(randomX,randomY,0),Quaternion.identity);
       Rigidbody2D cloudRB = newCloud.GetComponent<Rigidbody2D>();
       cloudRB.velocity = new Vector2(-4f, 0); 
       Destroy(newCloud,20);
    }
}

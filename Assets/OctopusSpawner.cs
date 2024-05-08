using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusSpawner : MonoBehaviour
{
    [SerializeField] private GameObject octopusPrefab;
    [SerializeField] private float spawnRange = 5f;
    [SerializeField] private float spawnRate = 7f;
    [SerializeField] private float speed = 7f;

    private Coroutine spawnOctopusCoroutine;

    public void InitOctopusSpawner(float duration) {
        spawnOctopusCoroutine = StartCoroutine(SpawnOctopusRoutine());
        // StartCoroutine(StopAfterTime(duration));
    }

    // IEnumerator SpawnOcotopusRoutine(){
    //     while(true){
    //         yield return new WaitForSeconds(4);
    //         SpawnOctopus();
    //     }
    // }
    
    IEnumerator StopAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (spawnOctopusCoroutine != null) 
        {
            StopCoroutine(spawnOctopusCoroutine);
            spawnOctopusCoroutine = null;
        }
    }

    IEnumerator SpawnOctopusRoutine(){
        while(true){
            SpawnOctopusLeft();
            float randRate = Random.Range(0, spawnRate);
            yield return new WaitForSeconds(randRate);
            SpawnOctopusRight();
        }
    }

    void SpawnOctopusRight(){
       float coinFlip = Random.value;
       float randomX;
       if (coinFlip < 0.5f) { randomX = 12f;}
       else {randomX = -14f;}
       float randomY = -3.2f;

       GameObject newOcto = Instantiate(octopusPrefab,new Vector3(randomX,randomY,0),Quaternion.identity);
       Rigidbody2D octoRb = newOcto.GetComponent<Rigidbody2D>();
       octoRb.velocity = new Vector2(-speed, 0);
       newOcto.transform.localScale = new Vector3(-2,2,2);
       Destroy(newOcto,10);
    }

    void SpawnOctopusLeft(){
       float coinFlip = Random.value;
       float randomX;
       if (coinFlip < 0.5f) { randomX = 12f;}
       else {randomX = -14f;}
       float randomY = -3.2f;

       GameObject newOcto = Instantiate(octopusPrefab,new Vector3(randomX,randomY,0),Quaternion.identity);
       Rigidbody2D octoRb = newOcto.GetComponent<Rigidbody2D>();
       octoRb.velocity = new Vector2(speed, 0);
       newOcto.transform.localScale = new Vector3(2,2,2);
       Destroy(newOcto,10);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRain : MonoBehaviour
{
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private float spawnRange = 8f;
    [SerializeField] private float spawnRate = 1.5f;
   
    private Coroutine spawnCloudsCoroutine;
    
    public void InitFireRain(float duration){
        spawnCloudsCoroutine = StartCoroutine(SpawnFireRoutine());
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

    IEnumerator SpawnFireRoutine(){
        while(true){
            float randRate = Random.Range(0, spawnRate);
            yield return new WaitForSeconds(randRate);
            SpawnFireRain();
        }
    }


       void SpawnFireRain(){

       float randomX = Random.Range(-spawnRange,spawnRange);
       float randomY = 6f;

       GameObject newObject = Instantiate(firePrefab,new Vector3(randomX,randomY,0),Quaternion.Euler(0f, 0f, 180f));
       Rigidbody2D cloudRB = newObject.GetComponent<Rigidbody2D>();
       cloudRB.velocity = new Vector2(0, -4); 
       Destroy(newObject,20);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCloud : MonoBehaviour
{
    [SerializeField] private GameObject cloudPrefab;
    private Coroutine spawnCloudsCoroutine;

    public void InitSmallCloudSpawner(float duration) {
        spawnCloudsCoroutine = StartCoroutine(SpawnCloudsRoutine());
        StartCoroutine(StopAfterTime(duration));
        StartCoroutine(MoveCloud());
    }

     public void RunSmallCloudSpawner(float duration) {
        spawnCloudsCoroutine = StartCoroutine(SpawnCloudsRoutine());
        StartCoroutine(StopAfterTime(duration));
    }
    
    IEnumerator MoveCloud()
    {
        yield return new WaitForSeconds(4);
        Rigidbody2D cloudRB = GetComponent<Rigidbody2D>();
        cloudRB.velocity = new Vector2(-4, 0);
        Destroy(cloudRB, 20);
    }

    IEnumerator SpawnCloudsRoutine(){
        while(true){
            yield return new WaitForSeconds(4);
            SpawnCloudRandom();
        }
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

     void SpawnCloudRandom(){
       int xPos = 14;
       float yPos = -4.27f;

       GameObject newCloud = Instantiate(cloudPrefab,new Vector3(xPos,yPos,0),Quaternion.identity);
       Rigidbody2D cloudRB = newCloud.GetComponent<Rigidbody2D>();
       cloudRB.velocity = new Vector2(-4f, 0); 
       Destroy(newCloud,20);
    }
}
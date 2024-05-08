using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBirdSpawner : MonoBehaviour
{
    [SerializeField] private GameObject blueBirdPrefab;
    [SerializeField] private float spawnRange = 5f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float spawnSpeed = 10f;
    [SerializeField] Creature player;

    void Start()
    {
        SpawnBirds();
    }

    void Update() {}

    void SpawnBirds(){
        StartCoroutine(SpawnBirdsRoutine());
        IEnumerator SpawnBirdsRoutine(){
            while(true){     
                float randRate = Random.Range(0, spawnSpeed);
                yield return new WaitForSeconds(randRate);
                SpawnBirdRandom();
            }
        }
    }

       void SpawnBirdRandom(){

       int randomX = 7;
       float randomY = Random.Range(-spawnRange,spawnRange);

       GameObject newBird = Instantiate(blueBirdPrefab,new Vector3(randomX,randomY,0),Quaternion.identity);
       newBird.transform.rotation = Quaternion.LookRotation(transform.forward, player.transform.position - new Vector3(randomX,randomY,0));
       Rigidbody2D birdRB = newBird.GetComponent<Rigidbody2D>();
       birdRB.velocity = newBird.transform.up * speed;
       newBird.transform.rotation = Quaternion.identity;
       newBird.transform.localScale = new Vector3(-2,2,2);
       Destroy(newBird,10);
    }

        //  Vector3 initPos = transform.position + new Vector3(-0.8f,2.5f,0);
        // GameObject newProjectile = Instantiate(projectilePrefab, initPos, Quaternion.identity);
        // newProjectile.transform.rotation = Quaternion.LookRotation(transform.forward, player.transform.position - initPos);
        // newProjectile.GetComponent<Rigidbody2D>().velocity = newProjectile.transform.up * speed;
        // Destroy(newProjectile,15);
}

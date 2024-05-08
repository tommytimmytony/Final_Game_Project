using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    [SerializeField] CloudSpawner cloudSpawner;
    [SerializeField] Boss boss;
    [SerializeField] FireRain fireRain;
    [SerializeField] SmallCloud smallCloud;
    [SerializeField] FinalClouds finalClouds;
    [SerializeField] BigClouds bigClouds;
    [SerializeField] OctopusSpawner octopusSpawner;
    [SerializeField] MusicBox musicBox;
    [SerializeField] GameObject spawner;
    void Start() {
        cloudSpawner.InitSpawner(60f);
        StartCoroutine(MoveBoss());
        finalClouds.InitFinalClouds();
        smallCloud.InitSmallCloudSpawner(60f); // Stop after this num sec 
    }

    void Update() {
        if (boss.health <= 0 && boss.finalBoss) {
            boss.BossDeath();
            spawner.SetActive(false);
        } else if (boss.health <= 0){
            Debug.Log("Boss Down");
            boss.health = 100;
            boss.BossDown();
            fireRain.InitFireRain(30f); // Stop after this num sec 
            octopusSpawner.InitOctopusSpawner(0f); // Stop after this num sec 
            StartCoroutine(ContinueLevel());
            boss.finalBoss = true;
        }
    }

    IEnumerator ContinueLevel()
    {
        yield return new WaitForSeconds(12f);
        cloudSpawner.InitSpawner(60f);
        finalClouds.MoveFinal();
        smallCloud.RunSmallCloudSpawner(60f);
        bigClouds.InitBigClouds(60f); // wait this num sec
        yield return new WaitForSeconds(10f);
        StartCoroutine(MoveBoss());
    }

    IEnumerator MoveBoss()
    {
        yield return new WaitForSeconds(65f);
        boss.InitBoss();
        musicBox.PlayBossFight();
    }
}

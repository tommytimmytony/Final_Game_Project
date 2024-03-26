using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileThrower : MonoBehaviour
{
    [SerializeField] GameObject normalShotPrefab;
    [SerializeField] GameObject powerShotPrefab;
    [SerializeField] bool powerShotActive = false;
    [SerializeField] float speed;
    public AnimationStateChanger asc;
    [SerializeField] float offsetShotX;
    [SerializeField] float offsetShotY;
    void Start(){
        speed = 20;
        offsetShotX = 0.7f;
        offsetShotY = -0.2f;
    }

    public void Launch(float xDirection){
        speed = Math.Abs(speed);
        offsetShotX = Math.Abs(offsetShotX);
        GameObject projectilePrefab = normalShotPrefab;
        
        if (powerShotActive) {
            projectilePrefab = powerShotPrefab;
            offsetShotX = 1.8f;
            offsetShotY = 0f;
        } 
        if (xDirection < 0) {
            speed = -speed;
            offsetShotX = -offsetShotX;
        }

        GameObject newProjectile = Instantiate(projectilePrefab, transform.position + new Vector3(offsetShotX,offsetShotY,0), Quaternion.identity);
        if (xDirection < 0){
            newProjectile.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        newProjectile.GetComponent<Rigidbody2D>().velocity = newProjectile.transform.right * speed;
        asc.ChangeAnimationState("Attack");
        Destroy(newProjectile,15);
    }

    public void ActivatePowerShot(){
        powerShotActive = true;
        StartCoroutine(DeactivatePowerShot(8f));
    }

     IEnumerator DeactivatePowerShot(float delay)
    {
        yield return new WaitForSeconds(delay);
        powerShotActive = false;
    }
}
